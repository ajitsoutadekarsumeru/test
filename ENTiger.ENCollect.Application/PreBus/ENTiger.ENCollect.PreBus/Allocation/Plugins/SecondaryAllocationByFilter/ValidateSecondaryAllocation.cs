﻿// <auto-generated>
//     This code was generated by Sumeru FlexGen.
//     Template Version: TemplateVersion
//
//     Do not rename the file
//     TODO:
//     Implement your validations in the Validate(..) method.
// </auto-generated>

using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AllocationModule.SecondaryAllocationByFilterAllocationPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ValidateSecondaryAllocation : FlexiBusinessRuleBase, IFlexiBusinessRule<SecondaryAllocationByFilterDataPacket>
    {
        public override string Id { get; set; } = "3a139de59506a1dd7faed68e1d6d33c9";
        public override string FriendlyName { get; set; } = "ValidateSecondaryAllocation";

        protected readonly ILogger<ValidateSecondaryAllocation> _logger;
        protected readonly IRepoFactory _repoFactory;

        public ValidateSecondaryAllocation(ILogger<ValidateSecondaryAllocation> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(SecondaryAllocationByFilterDataPacket packet)
        {
            //Uncomment the below line if validating against a db data using your repo
            //_repoFactory.Init(packet.Dto);

            //If any validation fails, uncomment and use the below line of code to add error to the packet
            //packet.AddError("Error", "ErrorMessage");


            await Task.CompletedTask; //If you have any await in the validation, remove this line

        }

    }
}
