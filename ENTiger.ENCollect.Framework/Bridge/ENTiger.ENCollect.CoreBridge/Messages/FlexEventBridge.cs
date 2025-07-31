using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public abstract class FlexEventBridge : FlexEvent, IFlexEventBridge
    {
    }

    public abstract class FlexEventBridge<TDto, TFlexAppContex> : FlexEvent<TDto, TFlexAppContex>, IFlexEventBridge where TDto : FlexDto<TFlexAppContex> where TFlexAppContex : FlexAppContextBridge
    {
    }

    public abstract class FlexEventBridge<TFlexAppContex> : FlexEvent<TFlexAppContex>, IFlexEventBridge where TFlexAppContex : FlexAppContextBridge
    {
    }
}