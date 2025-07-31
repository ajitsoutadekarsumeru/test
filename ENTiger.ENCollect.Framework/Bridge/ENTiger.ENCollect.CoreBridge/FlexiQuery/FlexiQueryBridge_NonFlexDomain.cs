using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public abstract class FlexiQueryBridge<TResult> : FlexiQuery<TResult> where TResult : class, new()
    {
    }

    public abstract class FlexiQueryBridgeAsync<TResult> : FlexiQueryAsync<TResult> where TResult : class, new()
    {
    }
}