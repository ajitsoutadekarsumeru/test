﻿// <auto-generated>
//     This code was generated by FlexGen.
//     
//
//     Do not implement any changes in this file
//     This file will be replaced with chosen option when the code will be generated next time.
// </auto-generated>

using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        protected virtual async Task ProcessCommand(UploadFileCommand cmd)
        {

            UploadFilePostBusDataPacket packet = _flexHost.GetFlexiFlowDataPacket<UploadFilePostBusDataPacket>();

            //Fill your data to datapacket here
            packet.Cmd = cmd;

            FlexiPluginSequenceBase<UploadFilePostBusDataPacket> sequence = _flexHost.GetFlexiPluginSequence<UploadFilePostBusSequence, UploadFilePostBusDataPacket>();

            await FlexiFlow.Run(sequence, packet, new FlexServiceBusContextBridge(_bus));
        }

    }
}
