using ENCollect.Dyna.Filters;

namespace ENTiger.ENCollect;

public interface IUserRepository
{
    /// <summary>
    /// Finds user IDs that match the final aggregator + optional param context
    /// (converted to an Expression or used in DB queries).
    /// </summary>
    Task<List<string>> FindEligibleUserIds(
        FlexAppContextBridge flexContext,
        ISearchCriteria aggregator, 
        IParameterContext? paramCtx,
        IContextDataPacket domainCtx);
}