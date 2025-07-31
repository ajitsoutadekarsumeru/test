using ENCollect.Dyna.Workflows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.IO.Abstractions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class NotifyPotentialActor : INotifyPotentialActor
    {
        protected readonly ILogger<NotifyPotentialActor> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected readonly ISettlementRepository _settlementRepository;
        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        protected readonly IEmailUtility _emailUtility;

        public NotifyPotentialActor(
            ILogger<NotifyPotentialActor> logger, 
            IRepoFactory repoFactory,
             IFileSystem fileSystem,
           IOptions<FilePathSettings> fileSettings,
            ISettlementRepository settlementRepository,
            MessageTemplateFactory messageTemplateFactory,
            IEmailUtility emailUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _settlementRepository = settlementRepository;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
            _messageTemplateFactory = messageTemplateFactory;
            _emailUtility = emailUtility;
        }

        public virtual async Task Execute(ActorIndentifiedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            _repoFactory.Init(@event);

            _logger.LogInformation("NotifyPotentialActor : Start");

            var userData = _repoFactory.GetRepo().FindAll<ApplicationUser>()
                .Where(x => x.Id == @event.ApplicationUserId)
                .Select(a => new
                {
                    a.Id,
                    a.PrimaryEMail,
                    a.FirstName,
                    a.LastName
                })
                .FirstOrDefault();

            var result = _repoFactory.GetRepo().FindAll<SettlementQueueProjection>()                
                .Where(x => x.SettlementId == @event.DomainId
                && x.ApplicationUserId == @event.ApplicationUserId
                && x.WorkflowInstanceId == @event.WorkflowInstanceId
                && x.IsDeleted == false
                )
                .Select(a => new SettlementDtoWithId
                {
                    Id = a.Id,
                    LoanAccountId = a.Settlement.LoanAccountId,
                    CustomId = a.Settlement.CustomId,
                    SettlementAmount = a.Settlement.SettlementAmount,
                    AgreementId = a.Settlement.LoanAccount.AGREEMENTID,
                    CustomerName = a.Settlement.LoanAccount.CUSTOMERNAME,
                    UserName = userData.FirstName + " " + userData.LastName,
                    UserEmail = userData.PrimaryEMail 
                })

                .FirstOrDefault();

            if (result != null)
            {
                var messageTemplate = _messageTemplateFactory.NotifyPotentialActorEmailTemplate(result);
                await _emailUtility.SendEmailAsync(result.UserEmail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);
            }


            _logger.LogInformation("NotifyPotentialActor : End");
            await Task.CompletedTask;
        }
       
    }
}