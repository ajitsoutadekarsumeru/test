using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public abstract class FlexiQueryEnumerableBridge<TResult> : FlexiQueryEnumerable<TResult> where TResult : class, new()
    {
    }

    public abstract class FlexiQueryEnumerableBridgeAsync<TResult> : FlexiQueryEnumerableAsync<TResult> where TResult : class, new()
    {
    }
}