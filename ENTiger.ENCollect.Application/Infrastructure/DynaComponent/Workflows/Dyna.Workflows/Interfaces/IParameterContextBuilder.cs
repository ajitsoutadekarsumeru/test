using ENCollect.Dyna.Filters;

public interface IParameterContextBuilder<TContext>
{
    IParameterContext BuildParameterContext(TContext domainCtx);
}