﻿// <auto-generated>
//     This code was generated by FlexGen.
//     
//
//     Do not implement any changes in this file
//     This file will be replaced with existing plugin list when the code will be generated next time.
// </auto-generated>

using ENTiger.ENCollect.CommunicationModule.AddTriggerCommunicationPlugins;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateTriggerStatusSequence : FlexiBusinessRuleSequenceBase<UpdateTriggerStatusDataPacket>
    {
        /// <summary>
        /// 
        /// </summary>
        public UpdateTriggerStatusSequence()
        {
            this.Add<CheckForDuplicateActiveTrigger>();
            this.Add<CheckTriggerTemplateStatus>();
        }
    }
}
