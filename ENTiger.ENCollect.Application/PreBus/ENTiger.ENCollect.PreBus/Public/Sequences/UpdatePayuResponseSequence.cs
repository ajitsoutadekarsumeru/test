﻿// <auto-generated>
//     This code was generated by FlexGen.
//     
//
//     Do not implement any changes in this file
//     This file will be replaced with existing plugin list when the code will be generated next time.
// </auto-generated>

using Sumeru.Flex;
using ENTiger.ENCollect.PublicModule.UpdatePayuResponsePublicPlugins;

namespace ENTiger.ENCollect.PublicModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdatePayuResponseSequence : FlexiBusinessRuleSequenceBase<UpdatePayuResponseDataPacket>
    {
        /// <summary>
        /// 
        /// </summary>
        public UpdatePayuResponseSequence()
        {
            
             this.Add<ValidatePayment>(); this.Add<UpdatePayment>();
        }
    }
}
