using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public abstract class FlexiQueryPagedListBridge<T, TParam, TResult, TFlexHostContextInfo> : FlexiQueryPagedList<T, TParam, TResult, TFlexHostContextInfo> where T : IObjectWithState where TResult : class, new() where TParam : PagedQueryParamsDto<TFlexHostContextInfo> where TFlexHostContextInfo : FlexAppContextBridge
    {
    }

    public abstract class FlexiQueryPagedListBridgeAsync<T, TParam, TResult, TFlexHostContextInfo> : FlexiQueryPagedListAsync<T, TParam, TResult, TFlexHostContextInfo> where T : IObjectWithState where TResult : class, new() where TParam : PagedQueryParamsDto<TFlexHostContextInfo> where TFlexHostContextInfo : FlexAppContextBridge
    {
    }
}