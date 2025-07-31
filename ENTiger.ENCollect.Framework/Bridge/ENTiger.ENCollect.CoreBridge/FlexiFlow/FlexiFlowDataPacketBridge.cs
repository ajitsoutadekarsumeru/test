using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public abstract class FlexiFlowDataPacketBridge : FlexiFlowDataPacket
    {
    }

    public abstract class FlexiFlowDataPacketWithDtoBridge<TDto, TFlexAppContex> : FlexiFlowDataPacketWithDto<TDto, TFlexAppContex> where TDto : FlexDto<TFlexAppContex> where TFlexAppContex : FlexAppContextBridge
    {
    }

    public abstract class FlexiFlowDataPacketWithCommandBridge<TCmd> : FlexiFlowDataPacketWithCommand<TCmd> where TCmd : FlexCommand
    {
    }
}