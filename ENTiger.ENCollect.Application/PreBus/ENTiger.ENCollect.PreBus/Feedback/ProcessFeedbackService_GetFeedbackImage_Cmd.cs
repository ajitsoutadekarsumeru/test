﻿// <auto-generated>
//     This code was generated by FlexGen.
//     
//
//     Do not implement any changes in this file
//     This file will be replaced with chosen option when the code will be generated next time.
// </auto-generated>

using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class ProcessFeedbackService : ProcessFlexServiceBridge
    {
        protected virtual async Task ProcessCommand(GetFeedbackImageCommand cmd)
        {

            GetFeedbackImagePostBusDataPacket packet = _flexHost.GetFlexiFlowDataPacket<GetFeedbackImagePostBusDataPacket>();

            //Fill your data to datapacket here
            packet.Cmd = cmd;

            FlexiPluginSequenceBase<GetFeedbackImagePostBusDataPacket> sequence = _flexHost.GetFlexiPluginSequence<GetFeedbackImagePostBusSequence, GetFeedbackImagePostBusDataPacket>();

            await FlexiFlow.Run(sequence, packet, new FlexServiceBusContextBridge(_bus));
        }

    }
}
