﻿// <auto-generated>
//     This code was generated by FlexGen.
//     
//
//     Do not implement any changes in this file
//     This file will be replaced with existing plugin list when the code will be generated next time.
// </auto-generated>

using Sumeru.Flex;
using ENTiger.ENCollect.SettlementModule.UpdateStatusSettlementPlugins;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateStatusSequence : FlexiBusinessRuleSequenceBase<UpdateStatusDataPacket>
    {
        /// <summary>
        /// 
        /// </summary>
        public UpdateStatusSequence()
        {
            
            this.Add<DynaApprovalWorkflow>(); 
        }
    }
}
