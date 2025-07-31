using ENTiger.ENCollect.DomainModels.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule.GetGeneratedFileAllocationPlugins
{
    public partial class CheckFileAccess : FlexiBusinessRuleBase, IFlexiBusinessRule<GetGeneratedFileDataPacket>
    {
        public override string Id { get; set; } = "4a1816dc3d48c6088f4e735b9197780f";
        public override string FriendlyName { get; set; } = "CheckFileAccess";

        protected readonly ILogger<CheckFileAccess> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        public CheckFileAccess(ILogger<CheckFileAccess> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(GetGeneratedFileDataPacket packet)
        {
            if (!packet.HasError)
            {
                _repoFactory.Init(packet.Dto);
                _flexAppContext = packet.Dto.GetAppContext();  //do not remove this line

                string userId = _flexAppContext.UserId;
                List<string> filesList = new List<string>();

                var generatedFiles = await _repoFactory.GetRepo().FindAll<AllocationDownload>()
                                                .Where(x => x.CreatedBy == userId && (x.FileName == packet.Dto.FileName))
                                                .ToListAsync();

                if (generatedFiles == null || generatedFiles.Count() == 0)
                {                    
                    _logger.LogError("CheckFileAccess : Access Denied");
                    packet.AddError("Error", "Access Denied");
                }
            }
        }
    }
}
