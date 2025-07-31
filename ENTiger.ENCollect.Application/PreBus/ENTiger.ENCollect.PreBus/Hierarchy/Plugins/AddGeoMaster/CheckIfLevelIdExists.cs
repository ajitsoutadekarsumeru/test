using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.HierarchyModule.AddGeoMasterHierarchyPlugins
{
    public partial class CheckIfLevelIdExists : FlexiBusinessRuleBase, IFlexiBusinessRule<AddGeoMasterDataPacket>
    {
        public override string Id { get; set; } = "3a1afc5d07d72a74303f1df9f08f6c53";
        public override string FriendlyName { get; set; } = "CheckIfLevelIdExists";

        protected readonly ILogger<CheckIfLevelIdExists> _logger;
        protected readonly RepoFactory _repoFactory;

        public CheckIfLevelIdExists(ILogger<CheckIfLevelIdExists> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(AddGeoMasterDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);

            //Verify whether the level id exists in the hierarchy level
            var hierarchyLevel = await _repoFactory.GetRepo().FindAll<HierarchyLevel>()
                                                .ByTFlexId(packet.Dto.LevelId)
                                                .FirstOrDefaultAsync();
            if (hierarchyLevel == null)
            {
                packet.AddError("Error", "A level id doesn't exist.");
            }
        }
    }
}
