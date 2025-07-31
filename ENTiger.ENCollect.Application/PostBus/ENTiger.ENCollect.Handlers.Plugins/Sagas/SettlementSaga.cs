
using Elastic.Clients.Elasticsearch.Security;
using ENCollect.Dyna.Cascading;
using ENCollect.Dyna.Workflows;
using ENTiger.ENCollect.SegmentationModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace ENTiger.ENCollect
{
    public class SettlementSaga 
        : DynaWorkflowSaga<DynaWorkflowSagaData, SettlementContext>,
            IAmStartedByMessages<ProcessSettlementRequestCommand>,
         IHandleMessages<SettlementRecommendedEvent>,
          IHandleMessages<SettlementDeniedEvent>,
          IHandleMessages<SettlementApprovedEvent>,
          IHandleMessages<SettlementRejectedEvent>,        
          IHandleMessages<CustomerRejectedEvent>,
          IHandleMessages<CustomerAcceptedEvent>,
          IHandleMessages<RenegotiationProcessedEvent>,
        IHandleMessages<SettlementUpdatedEvent>,
        IHandleMessages<SettlementCancelledEvent>
    {
        private readonly ISettlementRepository _repoSettlement;
        private readonly IPotentialActorsReadModel _readModel;
        private readonly IRepoFactory _repoFactory;
        protected readonly IWorkflowNavigator<IContextDataPacket> _navigator;
        protected readonly IStepActorResolver<IContextDataPacket> _actorResolver;

        public SettlementSaga(
            DynaWorkflowDefinition<SettlementContext> definition,
            ILogger<SettlementSaga> logger,            
            IUserRepository userRepository,
             //IParameterContextBuilder<SettlementContext>? paramCtxBuilder,
             ISettlementRepository readModel,

            IWorkflowNavigator<SettlementContext> navigator,
            IStepActorResolver<SettlementContext> actorResolver,
            ISettlementRepository repoSettlement, IRepoFactory repoFactory
        )
            : base(definition, logger,readModel, userRepository,navigator,actorResolver, null)
        {
            _repoSettlement = repoSettlement;
            _repoFactory = repoFactory;
        }

        protected override void ConfigureCorrelationDefault(SagaPropertyMapper<DynaWorkflowSagaData> mapper)
        {
            base.ConfigureCorrelationDefault(mapper);
            mapper.ConfigureMapping<ProcessSettlementRequestCommand>(m => m.WorkflowInstanceId)
                    .ToSaga(sagaData => sagaData.WorkflowInstanceId);

            //Recommendation
            mapper.ConfigureMapping<SettlementRecommendedEvent>(e => e.WorkflowInstanceId)
                  .ToSaga(d => d.WorkflowInstanceId);
            mapper.ConfigureMapping<SettlementDeniedEvent>(e => e.WorkflowInstanceId)
                      .ToSaga(d => d.WorkflowInstanceId);

            //Approval
            mapper.ConfigureMapping<SettlementApprovedEvent>(e => e.WorkflowInstanceId)
                      .ToSaga(d => d.WorkflowInstanceId);
            mapper.ConfigureMapping<SettlementRejectedEvent>(e => e.WorkflowInstanceId)
                      .ToSaga(d => d.WorkflowInstanceId);

            // Customer Acceptance
            mapper.ConfigureMapping<CustomerAcceptedEvent>(e => e.WorkflowInstanceId)
                      .ToSaga(d => d.WorkflowInstanceId);
            mapper.ConfigureMapping<CustomerRejectedEvent>(e => e.WorkflowInstanceId)
                      .ToSaga(d => d.WorkflowInstanceId);

            //Renegotiation
            mapper.ConfigureMapping<RenegotiationProcessedEvent>(m => m.WorkflowInstanceId)
                    .ToSaga(sagaData => sagaData.WorkflowInstanceId);
            mapper.ConfigureMapping<SettlementUpdatedEvent>(m => m.WorkflowInstanceId)
                    .ToSaga(sagaData => sagaData.WorkflowInstanceId);

            //Cancell
            mapper.ConfigureMapping<SettlementCancelledEvent>(m => m.WorkflowInstanceId)
                   .ToSaga(sagaData => sagaData.WorkflowInstanceId);


        }

        public async virtual Task Handle(ProcessSettlementRequestCommand message, 
            IMessageHandlerContext context)
        {
            // This domain command extends StartDynaSagaCommand 
            // => so we can pass it directly to the base logic or adapt if needed.
            StartDynaWorkflowCommand cmd = new StartDynaWorkflowCommand
            {
                DomainId = message.DomainId,
                WorkflowInstanceId = message.WorkflowInstanceId,
                FlexAppContext = message.FlexAppContext
            };
            await Handle(cmd, context);
        }

        protected override async Task<SettlementContext> CreateContextAsync(FlexAppContextBridge _flexAppContext)
        {
            //1) Fetch Waiver data
            var settlement = await _repoSettlement.GetWaiverDetailsByIdAsync(_flexAppContext, Data.DomainId);

            var waivers = settlement.WaiverDetails ?? Enumerable.Empty<WaiverDetail>();
            var principal = waivers
                            .FirstOrDefault(w =>
                            w.ChargeType
                            .Equals(WaiverCargeTypeEnum.Principal.Value,
                            StringComparison.OrdinalIgnoreCase));
                              
            
            decimal principalWaiverPercentage = principal?.WaiverPercent ?? 0m;

            //3) sum up everything else (interest + charges + any extra charge lines)
            // Safely handle a null or empty WaiverDetails collection and case‐insensitive comparison
            decimal interestAndChargesWaiver = (settlement.WaiverDetails ?? Enumerable.Empty<WaiverDetail>())
                .Where(w => !w.ChargeType.Equals(
                    WaiverCargeTypeEnum.Principal.Value,
                    StringComparison.OrdinalIgnoreCase))
                .Sum(w => w.WaiverAmount);

            //4) get the requestor level
            _repoFactory.Init(_flexAppContext);
             // Fetch the user once and check their type
            var user = await _repoFactory.GetRepo().FindAll<ApplicationUser>()                        
                        .FirstOrDefaultAsync(a => a.Id == settlement.CreatedBy);

           int level = 0;
            switch (user)
            {
                case AgencyUser agencyUser:
                    var userWithDesignation = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                        .Include(a => a.Designation)
                        .ThenInclude(d => d.Designation)
                        .FirstOrDefaultAsync(a => a.Id == settlement.CreatedBy);

                    level = userWithDesignation.Designation
                            .Select(d => d.Designation.Level)
                            .DefaultIfEmpty(0)
                            .Max();
                    break;

                case CompanyUser companyUser:
                    var companyUserWithDesignation = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                       .Include(a => a.Designation)
                       .ThenInclude(d => d.Designation)
                       .FirstOrDefaultAsync(a => a.Id == settlement.CreatedBy);
                    
                    level = companyUserWithDesignation.Designation
                            .Select(d => d.Designation.Level)
                            .DefaultIfEmpty(0)
                            .Max();
                    break;

                default:
                    level = 0; 
                    break;
            }


            return new SettlementContext
            {
                PrincipalWaiverPercentage = principalWaiverPercentage,
                InterestAndChargesWaiver  = interestAndChargesWaiver,
                RequestorLevel           = level,
                RequestorId = settlement.CreatedBy
                // Potentially store more domain info 
                // or userPerformingCurrentStep from saga data
            };
        }

        //Recommendation
        public async virtual Task Handle(SettlementRecommendedEvent _event, 
            IMessageHandlerContext ctx)
        => await AdvanceOrCompleteAsync(_event.AppContext, ctx);

        public async Task Handle(SettlementDeniedEvent _event, 
            IMessageHandlerContext ctx)
      => await MoveToStepAsync("Denied", ctx, _event.AppContext);

      
        //Approval
        public async Task Handle(SettlementApprovedEvent _event, 
            IMessageHandlerContext ctx)
       => await MoveToStepAsync("Pending Customer Acceptance", ctx, _event.AppContext);
       
        public async Task Handle(SettlementRejectedEvent _event, 
            IMessageHandlerContext ctx)
     => await MoveToStepAsync("Rejected", ctx, _event.AppContext);


        // Renegotiation
        public async Task Handle(RenegotiationProcessedEvent _event, 
            IMessageHandlerContext ctx)
        => await MoveToStepAsync("Pending Renegotiation", ctx, _event.AppContext);          // explicit jump
        
        public async Task Handle(SettlementUpdatedEvent _event, 
            IMessageHandlerContext ctx)
       => await MoveToStepAsync("Pending Recommendation1", ctx, _event.AppContext);          // explicit jump


        //Customer Settlement Rejection and Acceptance
        public async Task Handle(CustomerAcceptedEvent _event, IMessageHandlerContext ctx)
        => await MoveToStepAsync("Closed", ctx, _event.AppContext);          // explicit jump
        public async Task Handle(CustomerRejectedEvent _event, IMessageHandlerContext ctx)
        => await MoveToStepAsync("Closed", ctx, _event.AppContext);          // explicit jump

        //Cancel
        public async Task Handle(SettlementCancelledEvent _event, IMessageHandlerContext ctx)
       => await MoveToStepAsync("Cancelled", ctx, _event.AppContext);                        // explicit jump + terminal


    }
}