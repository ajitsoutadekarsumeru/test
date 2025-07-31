using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.HierarchyModule.AddGeoMasterHierarchyPlugins
{
    public partial class CheckForDuplicatesAtLevel : FlexiBusinessRuleBase, IFlexiBusinessRule<AddGeoMasterDataPacket>
    {
        public override string Id { get; set; } = "3a1afc5d02801ac38c2ef3b9c5c89bd9";
        public override string FriendlyName { get; set; } = "CheckForDuplicatesAtLevel";

        protected readonly ILogger<CheckForDuplicatesAtLevel> _logger;
        protected readonly RepoFactory _repoFactory;

        public CheckForDuplicatesAtLevel(ILogger<CheckForDuplicatesAtLevel> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(AddGeoMasterDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);

            //Verify whether a hierarchy master with the same name already exists to avoid duplicates
            var hierarchyMasterExists = await _repoFactory.GetRepo().FindAll<HierarchyMaster>()
                                            .Where(w => w.LevelId == packet.Dto.LevelId
                                                        && w.ParentId == packet.Dto.ParentId
                                                        && w.Item == packet.Dto.Item)
                                            .FirstOrDefaultAsync();
            if (hierarchyMasterExists != null)
            {
                packet.AddError("Error", "Geo master with the same name already exists. Use a different name.");
            }
        }
    }
}
