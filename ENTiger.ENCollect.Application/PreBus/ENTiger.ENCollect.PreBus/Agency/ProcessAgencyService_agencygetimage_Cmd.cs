﻿// <auto-generated>
//     This code was generated by FlexGen.
//     
//
//     Do not implement any changes in this file
//     This file will be replaced with chosen option when the code will be generated next time.
// </auto-generated>

using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class ProcessAgencyService : ProcessFlexServiceBridge
    {
        protected virtual async Task ProcessCommand(agencygetimageCommand cmd)
        {

            agencygetimagePostBusDataPacket packet = _flexHost.GetFlexiFlowDataPacket<agencygetimagePostBusDataPacket>();

            //Fill your data to datapacket here
            packet.Cmd = cmd;

            FlexiPluginSequenceBase<agencygetimagePostBusDataPacket> sequence = _flexHost.GetFlexiPluginSequence<agencygetimagePostBusSequence, agencygetimagePostBusDataPacket>();

            await FlexiFlow.Run(sequence, packet, new FlexServiceBusContextBridge(_bus));
        }

    }
}
