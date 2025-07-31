using ENTiger.ENCollect.AllocationModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PreBus.Allocation.Plugins.PrimaryValidateUnAllocationAccess
{
    public partial class ValidateUnAllocationAccess : FlexiBusinessRuleBase, IFlexiBusinessRule<PrimaryUnAllocationByBatchDataPacket>
    {
        public override string Id { get; set; } = "3a12e79d05084cc8e16392c61adc1cdd";
        public override string FriendlyName { get; set; } = "ValidateUnAllocationAccess";

        protected readonly ILogger<ValidateUnAllocationAccess> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        public ValidateUnAllocationAccess(ILogger<ValidateUnAllocationAccess> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(PrimaryUnAllocationByBatchDataPacket packet)
        {
            //Uncomment the below line if validating against a db data using your repo
            _flexAppContext = packet.Dto.GetAppContext();
            _repoFactory.Init(packet.Dto);

            string loggedInUserId = _flexAppContext.UserId;
            CompanyUser users;
            string unallocationType = packet.Dto.UnAllocationType;

            if (unallocationType?.ToLower() == "customerid level")
            {
                users = await _repoFactory.GetRepo().FindAll<CompanyUser>().ByCompanyUserId(loggedInUserId).FirstOrDefaultAsync();
                if(users == null)
                {
                    packet.AddError("Error", "You do not have permission for unallocation at the customer ID level");
                }
            }
        }
    }
}
