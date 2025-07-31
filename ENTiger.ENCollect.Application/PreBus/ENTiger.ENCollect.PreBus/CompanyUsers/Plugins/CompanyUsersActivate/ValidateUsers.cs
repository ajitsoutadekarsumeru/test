using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule.CompanyUsersActivateCompanyUsersPlugins
{
    public partial class ValidateUsers : FlexiBusinessRuleBase, IFlexiBusinessRule<CompanyUsersActivateDataPacket>
    {
        public override string Id { get; set; } = "3a19940da272d57e5f06ae584f0cd97b";
        public override string FriendlyName { get; set; } = "ValidateUsers";

        protected readonly ILogger<ValidateUsers> _logger;
        protected readonly RepoFactory _repoFactory;

        public ValidateUsers(ILogger<ValidateUsers> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(CompanyUsersActivateDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);
            CompanyUser? user;
            foreach (var userId in packet.Dto.Ids)
            {
                user = await _repoFactory.GetRepo().FindAll<CompanyUser>().Where(m => m.Id == userId).FirstOrDefaultAsync();
                if (user == null)
                {
                    packet.AddError("Error", $"User with id {userId} does not exist");
                }
                if (user != null && user.IsBlackListed)
                {
                    packet.AddError("Error", $"{user?.FirstName + " " + user?.LastName} is blacklisted");
                }
                if (user != null && user.IsDeactivated)
                {
                    packet.AddError("Error", $"{user?.FirstName + " " + user?.LastName} is deactivated");
                }
                if (user != null && user.IsDeleted)
                {
                    packet.AddError("Error", $"{user?.FirstName + " " + user?.LastName} has been deleted");
                }
            }

        }

    }
}
