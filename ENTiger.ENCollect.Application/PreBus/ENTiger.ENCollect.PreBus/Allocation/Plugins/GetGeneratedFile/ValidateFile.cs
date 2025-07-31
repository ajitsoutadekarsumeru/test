using ENTiger.ENCollect.DomainModels;
using ENTiger.ENCollect.DomainModels.Reports;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.IO.Abstractions;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AllocationModule.GetGeneratedFileAllocationPlugins
{
    public partial class ValidateFile : FlexiBusinessRuleBase, IFlexiBusinessRule<GetGeneratedFileDataPacket>
    {
        public override string Id { get; set; } = "3a13ffd4599b1c4435c4c6cafe0c7b08";
        public override string FriendlyName { get; set; } = "ValidateFile";

        protected readonly ILogger<ValidateFile> _logger;
        protected readonly IRepoFactory _repoFactory;

        public ValidateFile(ILogger<ValidateFile> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(GetGeneratedFileDataPacket packet)
        {
            //Uncomment the below line if validating against a db data using your repo
            //_repoFactory.Init(packet.Dto);

            //If any validation fails, uncomment and use the below line of code to add error to the packet
            //packet.AddError("Error", "ErrorMessage");

            await Task.CompletedTask; //If you have any await in the validation, remove this line
        }
    }
}
