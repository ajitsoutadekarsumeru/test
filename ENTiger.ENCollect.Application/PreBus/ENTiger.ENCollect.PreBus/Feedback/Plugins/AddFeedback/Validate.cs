﻿// <auto-generated>
//     This code was generated by Sumeru FlexGen.
//     Template Version: TemplateVersion
//
//     Do not rename the file
//     TODO:
//     Implement your validations in the Validate(..) method.
// </auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.FeedbackModule.AddFeedbackFeedbackPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ValidateFeedback : FlexiBusinessRuleBase, IFlexiBusinessRule<AddFeedbackDataPacket>
    {
        public override string Id { get; set; } = "3a1365b6a713832ab2fb9891923ce41e";
        public override string FriendlyName { get; set; } = "Validate";

        protected readonly ILogger<ValidateFeedback> _logger;
        protected readonly IRepoFactory _repoFactory;

        public ValidateFeedback(ILogger<ValidateFeedback> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(AddFeedbackDataPacket packet)
        {
            //Uncomment the below line if validating against a db data using your repo
            _repoFactory.Init(packet.Dto);
            var inputmodeldata = packet.Dto as AddFeedbackDto;

            var dispCode = _repoFactory.GetRepo()
                .FindAll<DispositionCodeMaster>()
                .Include(c => c.DispositionGroupMaster)
                .Where(c=>c.DispositionGroupMaster.Name.ToLower() == inputmodeldata.DispositionGroup.ToLower() 
                && c.DispositionCode.ToLower() == inputmodeldata.DispositionCode.ToLower() 
                && c.IsDeleted == false)
                .FirstOrDefault();

            if(dispCode == null) {
                packet.AddError("Error", "Disposition Code & Group Doesn't Match or Exists");
            }



            //If any validation fails, uncomment and use the below line of code to add error to the packet
            //packet.AddError("Error", "ErrorMessage");

            await Task.CompletedTask; //If you have any await in the validation, remove this line
        }
    }
}
