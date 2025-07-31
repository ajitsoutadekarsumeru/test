using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public abstract class FlexiQueryBridge<T, TResult> : FlexiQuery<T, TResult> where T : IObjectWithState where TResult : class, new()
    {
    }

    public abstract class FlexiQueryBridgeAsync<T, TResult> : FlexiQueryAsync<T, TResult> where T : IObjectWithState where TResult : class, new()
    {
    }
}