using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.HierarchyModule.AddGeoMasterHierarchyPlugins
{
    public partial class CheckIfParentIdExists : FlexiBusinessRuleBase, IFlexiBusinessRule<AddGeoMasterDataPacket>
    {
        public override string Id { get; set; } = "3a1afc5d07d72a74303f1df9f08f6c52";
        public override string FriendlyName { get; set; } = "CheckIfParentIdExists";

        protected readonly ILogger<CheckIfParentIdExists> _logger;
        protected readonly RepoFactory _repoFactory;

        public CheckIfParentIdExists(ILogger<CheckIfParentIdExists> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(AddGeoMasterDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);

            //Verify whether the parent exists in the hierarchy master
            var parentHierarchyMaster = await _repoFactory.GetRepo().FindAll<HierarchyMaster>()
                                                .ByTFlexId(packet.Dto.ParentId)
                                                .FirstOrDefaultAsync();
            if (parentHierarchyMaster == null)
            {
                packet.AddError("Error", "A parent geo master doesn't exist.");
            }
        }
    }
}
