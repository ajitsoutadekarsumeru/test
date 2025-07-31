using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using ENCollect.Dyna.Cascading;
using ENCollect.Dyna.Filters;
using ENCollect.Dyna.Workflows;
using ENTiger.ENCollect;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace ENCollect.Dyna.Workflows
{
    /// <summary>
    /// A "thin" base saga for dynamic approval workflows.
    /// 
    /// Each derived saga provides:
    ///   - A specific <see cref="DynaWorkflowDefinition{TContext}"/> 
    ///   - Implementation of the abstract methods 
    ///     (IsUserAuthorizedForStep, CreateContext, IdentifyActorsForStep, 
    ///      OnTransitionAuthorized).
    /// 
    /// This saga correlates on a single "WorkflowInstanceId" (string) 
    /// plus stores a separate "DomainId" for aggregator usage.
    /// </summary>
    /// <typeparam name="TData">
    ///   The saga data type (must inherit <see cref="DynaWorkflowSagaData"/>).
    /// </typeparam>
    /// <typeparam name="TContext">
    ///   A context implementing <see cref="IContextDataPacket"/> 
    ///   used for step .IsNeeded(...) logic and aggregator usage.
    /// </typeparam>
    public abstract class DynaWorkflowSaga<TData, TContext>
        : Saga<TData>,
          IAmStartedByMessages<StartDynaWorkflowCommand>,
          IHandleMessages<DynaWorkflowTransitionCommand>           
         

        where TData : DynaWorkflowSagaData, IContainSagaData, new()
        where TContext : IContextDataPacket
    {
              

        #region ===  Fields (DI singletons)  ===
        /// <summary>
        /// The step definitions specifying .IsNeeded(...) 
        /// and .ActorFlow(...) logic for the entire workflow.
        /// </summary>
        protected readonly DynaWorkflowDefinition<TContext> _definition;
        protected readonly IWorkflowNavigator<TContext> _navigator;
        protected readonly IStepActorResolver<TContext> _actorResolver;

        //Read model for storing possible actors on the workflow item
        private readonly ISettlementRepository _readModel;
        // For aggregator-based user lookups at the DB level
        private readonly IUserRepository _userRepository;       
        /// <summary>
        /// Logger for all normal, warning, and error logs.
        /// </summary>
        protected readonly ILogger<DynaWorkflowSaga<TData, TContext>> _logger;

        private readonly IUserRepository _userRepo;
        private readonly IParameterContextBuilder<TContext>? _paramCtxBuilder;
        protected FlexAppContextBridge? _flexAppContext;

        #endregion

        /// <summary>
        /// The derived saga must provide:
        ///   - A domain-specific workflow definition
        ///   - A typed logger
        ///   - A read model for user checks
        /// 
        /// We then store them, so the base saga can do both 
        /// step skip logic + user auth checks.
        /// </summary>
        protected DynaWorkflowSaga(
            DynaWorkflowDefinition<TContext> definition,
            ILogger<DynaWorkflowSaga<TData, TContext>> logger,
            ISettlementRepository readModel,
            IUserRepository userRepository,
             IWorkflowNavigator<TContext> navigator,
            IStepActorResolver<TContext> actorResolver,
            IParameterContextBuilder<TContext>? paramCtxBuilder = null
        )
        {
            _definition = definition;
            _logger = logger;
            _readModel = readModel;
            _userRepository = userRepository;
            _paramCtxBuilder = paramCtxBuilder;
            _navigator = navigator;
            _actorResolver = actorResolver;
        }

        // --------------------------------------------------------------------
        // (A) SAGA CORRELATION
        // --------------------------------------------------------------------

        /// <summary>
        /// Configures how to correlate the start command, transitions, 
        /// and domain events by 'WorkflowInstanceId'.
        /// Derived saga can override if it wants custom correlation logic.
        /// </summary>
        protected virtual void ConfigureCorrelationDefault(SagaPropertyMapper<TData> mapper)
        {
            mapper.ConfigureMapping<StartDynaWorkflowCommand>(c => c.WorkflowInstanceId)
                  .ToSaga(d => d.WorkflowInstanceId);

            mapper.ConfigureMapping<DynaWorkflowTransitionCommand>(c => c.WorkflowInstanceId)
                  .ToSaga(d => d.WorkflowInstanceId);
            
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TData> mapper)
        {
            ConfigureCorrelationDefault(mapper);
        }

        // --------------------------------------------------------------------
        // (B) HANDLE START
        // --------------------------------------------------------------------

        #region ===  Start-command handler  ===

        /// <summary>
        /// Receives the start command (inheriting from <see cref="StartDynaWorkflowCommand"/>).
        /// We set up saga data, attempt skip logic, and if we remain active, 
        /// call <see cref="OnWorkflowInitiated"/>.
        /// </summary>
        public virtual async Task Handle(StartDynaWorkflowCommand cmd, IMessageHandlerContext context)
        {
            _flexAppContext = cmd.FlexAppContext;

            Data.WorkflowInstanceId = cmd.WorkflowInstanceId;
            Data.DomainId = cmd.DomainId;
            Data.IsCompleted = false;
            Data.CurrentStepName = null; // “not started”

            _logger.LogInformation(
                "[BaseSaga] Start command => WorkflowInstanceId={WorkflowInstanceId}, DomainId={DomainId}",
                cmd.WorkflowInstanceId, 
                cmd.DomainId);

            await AdvanceOrCompleteAsync(_flexAppContext,context); // pick first step
            if (!Data.IsCompleted) await OnWorkflowInitiated(context);
        }

        /// <summary>Domain sagas may send a “workflow-initiated” event, email, etc.</summary>
        protected virtual Task OnWorkflowInitiated(IMessageHandlerContext ctx) => Task.CompletedTask;

        #endregion

        // --------------------------------------------------------------------
        // (C) HANDLE TRANSITIONS
        // --------------------------------------------------------------------

        #region ===  Transition command (user click)  ===
        /// <summary>
        /// Receives transitions like (Recommend, Deny, Approve, Reject).
        /// We do a pre-check => either <see cref="OnTransitionAuthorized"/> 
        /// or <see cref="OnTransitionFailed"/>.
        /// </summary>
        public virtual async Task Handle(DynaWorkflowTransitionCommand cmd, 
            IMessageHandlerContext context)
        {
            _logger.LogInformation(
                "[BaseSaga] Transition command => WorkflowInstanceId={WorkflowInstanceId}, StepIndex={StepIndex}, ActionType={ActionType}, UserId={UserId}",
                cmd.WorkflowInstanceId,
                cmd.StepName,
                cmd.ActionType,
                cmd.UserId);

            if (!PreCheckTransition(cmd, out var reason))
                await OnTransitionFailed(cmd, reason, context);
            else
                await OnTransitionAuthorized(cmd, context);
        }

        /// <summary>
        /// Private method verifying step index alignment, 
        /// step type vs. action type, user authorization, 
        /// and saga completion status. Logs warnings if failing.
        /// </summary>
        private bool PreCheckTransition(DynaWorkflowTransitionCommand cmd, out string reason)
        {
            reason = string.Empty;

            if (Data.IsCompleted)
            {
                reason = "Completed";
                return false;
            }

            if (!string.Equals(cmd.StepName, Data.CurrentStepName, StringComparison.OrdinalIgnoreCase))
            {
                reason = "Step mismatch";
                return false;
            }

            //if (!_readModel.IsUserAuthorized(Data.DomainId, _definition.WorkflowName, cmd.StepName!, cmd.UserId))
            //{
            //    reason = "User not authorised";
            //    return false;
            //}

            return true;
        }
    
        /// <summary>
        /// Default => logs a warning about the failure. 
        /// Derived saga can override to publish an event or do more.
        /// </summary>
        protected virtual Task OnTransitionFailed(DynaWorkflowTransitionCommand cmd, string reason,
            IMessageHandlerContext context)
        {
            _logger.LogWarning(
                "[BaseSaga] OnTransitionFailed => WorkflowInstanceId={WorkflowInstanceId}, StepIndex={StepIndex}, Reason={Reason}",
                cmd.WorkflowInstanceId,
                cmd.StepName,
                reason);

            return Task.CompletedTask;
        }
        
        /// <summary>
        /// Called once the pre-check passes for a transition (Recommend, Deny, Approve, Reject).
        /// By default, this uses reflection to create and populate a domain command
        /// as specified by <c>ActionCommandMap</c> in the workflow definition.
        /// Then it sends that command to the bus.
        /// 
        /// If your domain requires extra logic or you have specialized commands,
        /// you can override this method in the derived saga for full control.
        /// </summary>
        /// <param name="cmd">
        /// The <see cref="DynaWorkflowTransitionCommand"/> containing step index,
        /// action type, user ID, and workflow instance ID.
        /// </param>
        /// <param name="ctx">
        /// The NServiceBus message handler context for sending commands or publishing events.
        /// </param>
        //protected virtual async Task OnTransitionAuthorized_Old(DynaWorkflowTransitionCommand cmd, IMessageHandlerContext ctx)
        //{
        //    //check the commandType
        //    var step = _definition.Steps[Data.CurrentStepIndex];
        //    // 2) Map (step, incoming action) → resulting ActionType
        //    ActionType actionType = step.UIActionContext switch
        //    {
        //        UIActionContext.Recommendation => cmd.ActionType switch
        //        {
        //            ActionEnum.Reject => ActionType.Deny,
        //            ActionEnum.Approve => ActionType.Recommend,
        //            ActionEnum.Negotiate => ActionType.Negotiate,
        //            _ => throw new InvalidOperationException($"Unhandled action type: {cmd.ActionType}")
        //        },

        //        UIActionContext.Approval => cmd.ActionType switch
        //        {
        //            ActionEnum.Reject => ActionType.Reject,
        //            ActionEnum.Approve => ActionType.Approve,
        //            ActionEnum.Negotiate => ActionType.Negotiate,
        //            _ => throw new InvalidOperationException($"Unhandled action type: {cmd.ActionType}")
        //        },

        //        UIActionContext.Acceptance => cmd.ActionType switch
        //        {
        //            ActionEnum.Reject => ActionType.CustomerReject,
        //            ActionEnum.Approve => ActionType.CustomerAcceptance,
        //            ActionEnum.Negotiate => ActionType.Negotiate,
        //            _ => throw new InvalidOperationException($"Unhandled action type: {cmd.ActionType}")
        //        },

        //        _ => throw new InvalidOperationException(
        //            $"Unhandled step type: {step.UIActionContext}")
        //    };

        //    if (!_definition.ActionCommandMap.TryGetValue(actionType, out var commandType))
        //    {
        //        _logger.LogError("[BaseSaga] No command type found for ActionType={ActionType}", cmd.ActionType);
        //        return;
        //    }

        //    // Use our reflection cache => create + populate the 3 properties
        //    var domainCommand = CommandReflectionCache.CreateAndPopulate(
        //            commandType,
        //            Data.DomainId,
        //            cmd.UserId,
        //            cmd.Dto                
        //        );

        //    _logger.LogInformation(
        //        "[BaseSaga] Sending domain command {CommandClass} for Action={ActionType}",
        //        commandType.Name,
        //        cmd.ActionType
        //    );

        //    var command = (FlexCommandBridge<FlexAppContextBridge>)domainCommand;
        //    command.FlexAppContext = cmd.FlexAppContext;
        //    await ctx.Send(domainCommand).ConfigureAwait(false);
        //}

        /// <summary>
        /// By default, maps <see cref="ActionType"/> to a domain command via
        /// <c>_def.ActionCommandMap</c>.  Override for custom logic.
        /// </summary>
        // inside CoreWorkflowSaga
        protected async Task OnTransitionAuthorized(
            DynaWorkflowTransitionCommand cmd,            
            IMessageHandlerContext ctx)
        {
            if (!_definition.ActionCommandMap.TryGetValue(cmd.ActionType, out var cmdType))
            {
                _logger.LogError("[CoreSaga] No command mapped for {0}", cmd.ActionType);
                return;
            }

            var domainCmd = CommandReflectionCache.CreateAndPopulate(
                cmdType,
                Data.DomainId,
                cmd.UserId,
                Data.WorkflowInstanceId,
                Data.CurrentStepName!,              
                _definition.StepLookup[Data.CurrentStepName!].UIActionContext.ToString(),
                cmd.Dto);          // pass packet (may be null)

            await ctx.Send(domainCmd);
        }

        #endregion

        // --------------------------------------------------------------------
        // (D) STEP SKIP LOGIC & ACTOR IDENTIFICATION
        // --------------------------------------------------------------------

        #region ===  Navigation & actor helpers  ===
        /// <summary>
        /// Called after we start or handle domain events. 
        /// It attempts to find the next needed step by checking 
        /// step.IsNeeded(...) on subsequent steps. 
        /// If found, we set Data.CurrentStepIndex and call IdentifyActorsForStep(...). 
        /// If none remain, we end the workflow.
        /// </summary>
        protected virtual async Task<(bool continueWorkflow, string reason)> MoveToNextNeededStep(
            FlexAppContextBridge flexContext, IMessageHandlerContext context)
        {
            var currentIdx = Data.CurrentStepIndex;
            var domainCtx = await CreateContextAsync(flexContext);

            // Ask navigator for the next required node
            var next = _navigator.GetNext(_definition, Data.CurrentStepName, domainCtx);
            if (next is null) return (false, "No further steps");

            // Persist pointer
            Data.CurrentStepName = next;

            // Grab the full step object once, we need it twice
            var step = _definition.StepLookup[next];

            // Resolve eligible users for the step
            var users = await _actorResolver.GetActors(flexContext,step, domainCtx);

            // Notify read-model / UI
            if (users.Count > 0)
            {
                await context.Send(new RecordPotentialActorsCommand
                {
                    DomainId = Data.DomainId,
                    WorkflowName = _definition.WorkflowName,
                    StepName = next,                   
                    EligibleUserIds = users,
                    UIActionContext = step.UIActionContext.ToString(),
                    WorkflowInstanceId = Data.WorkflowInstanceId,
                    FlexAppContext = flexContext
                });
            }

            return (true, "");
        }

        /// <summary>
        /// Common pattern for domain events: move forward; otherwise finish.
        /// </summary>
        protected async Task AdvanceOrCompleteAsync(FlexAppContextBridge flexContext,IMessageHandlerContext ctx)
        {
            var (cont, _) = await MoveToNextNeededStep(flexContext,ctx);
            if (!cont)
            {
                Data.IsCompleted = true;
                MarkAsComplete();
            }
        }

        /// <summary>
        /// Jump to an explicitly-named step, then either<br/>
        /// • publish the actor list (if the step has outgoing transitions)<br/>
        /// • or mark the saga complete (if it is a terminal node).<br/>
        /// All reachability checks are performed by the navigator.
        /// </summary>
        protected async Task MoveToStepAsync(
            string targetStep, 
            IMessageHandlerContext ctx,
             FlexAppContextBridge flexContext)
        {
            // 1. Navigator validates and returns canonical name
            Data.CurrentStepName = _navigator.MoveToStep(
                Data.CurrentStepName!, 
                targetStep, 
                _definition);

            // 2. Fetch the step once
            var step = _definition.StepLookup[targetStep];

            // 3. If the step has no outgoing transitions, the workflow is finished
            if (step.AllowedTransitions.Count == 0)
            {
                Data.IsCompleted = true;
                MarkAsComplete();
                return;
            }

            // 4. Non-terminal: resolve actors and notify read-model
            var domainCtx = await CreateContextAsync(flexContext);
            var users = await _actorResolver.GetActors(flexContext, step, domainCtx);

            if (users.Count > 0)
            {
                await ctx.Send(new RecordPotentialActorsCommand
                {
                    DomainId = Data.DomainId,
                    WorkflowName = _definition.WorkflowName,
                    StepName = targetStep,
                    EligibleUserIds = users,
                    UIActionContext = step.UIActionContext.ToString(),
                    WorkflowInstanceId = Data.WorkflowInstanceId,
                    FlexAppContext = flexContext
                });
            }
        }
        #endregion

        /// <summary>
        /// Derived saga constructs a TContext (by e.g. loading domain data from a repo 
        /// using Data.DomainId) for step .IsNeeded(...) checks and aggregator usage.
        /// </summary>
        protected abstract Task<TContext> CreateContextAsync(FlexAppContextBridge _flexAppContext);


    }
}