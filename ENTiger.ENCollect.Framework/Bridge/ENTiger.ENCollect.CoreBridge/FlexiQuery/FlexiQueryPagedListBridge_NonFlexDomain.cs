using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public abstract class FlexiQueryPagedListBridge<TParam, TResult, TFlexHostContextInfo> : FlexiQueryPagedList<TParam, TResult, TFlexHostContextInfo> where TResult : class, new() where TParam : PagedQueryParamsDto<TFlexHostContextInfo> where TFlexHostContextInfo : FlexAppContextBridge
    {
    }

    public abstract class FlexiQueryPagedListBridgeAsync<TParam, TResult, TFlexHostContextInfo> : FlexiQueryPagedListAsync<TParam, TResult, TFlexHostContextInfo> where TResult : class, new() where TParam : PagedQueryParamsDto<TFlexHostContextInfo> where TFlexHostContextInfo : FlexAppContextBridge
    {
    }
}