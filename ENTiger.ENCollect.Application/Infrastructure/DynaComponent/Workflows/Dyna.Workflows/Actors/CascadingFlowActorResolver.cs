using ENCollect.Dyna.Filters;
using ENTiger.ENCollect;

namespace ENCollect.Dyna.Workflows;

/// Resolves actors for a workflow step that uses CascadingFlow<TContext>.
public sealed class CascadingFlowActorResolver<TContext> : IStepActorResolver<TContext>
    where TContext : IContextDataPacket
{
    private readonly IUserRepository                    _userRepository;
    private readonly IParameterContextBuilder<TContext>? _paramCtxBuilder;

    public CascadingFlowActorResolver(
        IUserRepository                    userRepository,
        IParameterContextBuilder<TContext>? paramCtxBuilder = null)
    {
        _userRepository  = userRepository;
        _paramCtxBuilder = paramCtxBuilder;
    }
    public async Task<List<string>> GetActors(FlexAppContextBridge flexContext,
        DynaWorkflowStep<TContext> step, 
        TContext ctx)
    {
        // 1️⃣ Evaluate the cascading flow → final search-criteria object
        var criteria = step.ActorFlow.Evaluate(ctx);

        // 2️⃣ Optional parameter-context (place-holders, values, etc.)
        //IParameterContext? paramCtx = _paramCtxBuilder?.BuildParameterContext(ctx);

        // 3️⃣ Ask the repository, now passing the domain context as well
        var userIds = await _userRepository.FindEligibleUserIds(
            flexContext,criteria, null, ctx);

        // 4️⃣ Return a defensive copy
        return  userIds;
    }
    
}