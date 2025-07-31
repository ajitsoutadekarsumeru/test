using ENCollect.Dyna.Cascading;
using ENTiger.ENCollect;

namespace ENCollect.Dyna.Workflows;

public interface IStepActorResolver<TContext> where TContext : IContextDataPacket
{
    Task<List<string>> GetActors(FlexAppContextBridge flexContext,
        DynaWorkflowStep<TContext> step,
        TContext ctx);
}