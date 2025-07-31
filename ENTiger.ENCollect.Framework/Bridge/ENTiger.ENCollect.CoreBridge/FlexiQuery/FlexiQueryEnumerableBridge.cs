using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public abstract class FlexiQueryEnumerableBridge<T, TResult> : FlexiQueryEnumerable<T, TResult> where T : IObjectWithState where TResult : class, new()
    {
    }

    public abstract class FlexiQueryEnumerableBridgeAsync<T, TResult> : FlexiQueryEnumerableAsync<T, TResult> where T : IObjectWithState where TResult : class, new()
    {
    }
}