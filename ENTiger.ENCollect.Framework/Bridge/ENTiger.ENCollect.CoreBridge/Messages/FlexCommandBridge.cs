using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public abstract class FlexCommandBridge : FlexCommand, IFlexCommandBridge
    {
    }

    public abstract class FlexCommandBridge<TDto, TFlexAppContex> : FlexCommand<TDto, TFlexAppContex>, IFlexCommandBridge where TDto : FlexDto<TFlexAppContex> where TFlexAppContex : FlexAppContextBridge
    {
    }

    public abstract class FlexCommandBridge<TFlexAppContex> : FlexCommand<TFlexAppContex>, IFlexCommandBridge where TFlexAppContex : FlexAppContextBridge
    {
    }
}