﻿// <auto-generated>
//     This code was generated by FlexGen.
//     
//
//     Do not implement any changes in this file
//     This file will be replaced with existing plugin list when the code will be generated next time.
// </auto-generated>

using Sumeru.Flex;
using ENTiger.ENCollect.CommonModule.UploadFileCommonPlugins;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UploadFileSequence : FlexiBusinessRuleSequenceBase<UploadFileDataPacket>
    {
        /// <summary>
        /// 
        /// </summary>
        public UploadFileSequence()
        {            
            this.Add<ValidateFile>(); this.Add<UploadFile>();
        }
    }
}
