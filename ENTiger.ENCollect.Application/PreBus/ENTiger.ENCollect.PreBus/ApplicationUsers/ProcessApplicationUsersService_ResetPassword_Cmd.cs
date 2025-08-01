﻿// <auto-generated>
//     This code was generated by FlexGen.
//     
//
//     Do not implement any changes in this file
//     This file will be replaced with chosen option when the code will be generated next time.
// </auto-generated>

using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        protected virtual async Task ProcessCommand(ResetPasswordCommand cmd)
        {

            ResetPasswordPostBusDataPacket packet = _flexHost.GetFlexiFlowDataPacket<ResetPasswordPostBusDataPacket>();

            //Fill your data to datapacket here
            packet.Cmd = cmd;

            FlexiPluginSequenceBase<ResetPasswordPostBusDataPacket> sequence = _flexHost.GetFlexiPluginSequence<ResetPasswordPostBusSequence, ResetPasswordPostBusDataPacket>();

            await FlexiFlow.Run(sequence, packet, new FlexServiceBusContextBridge(_bus));
        }

    }
}
