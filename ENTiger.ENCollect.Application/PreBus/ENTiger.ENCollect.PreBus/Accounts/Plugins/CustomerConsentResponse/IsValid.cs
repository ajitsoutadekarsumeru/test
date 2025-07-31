using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule.CustomerConsentResponseAccountsPlugins
{
    public partial class IsValid : FlexiBusinessRuleBase, IFlexiBusinessRule<CustomerConsentResponseDataPacket>
    {
        public override string Id { get; set; } = "3a18827d44b907a02723ddcfecc71fe3";
        public override string FriendlyName { get; set; } = "IsValid";

        protected readonly ILogger<IsValid> _logger;
        protected readonly IRepoFactory _repoFactory;

        public IsValid(ILogger<IsValid> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(CustomerConsentResponseDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);

            var AppointmentExists = await _repoFactory.GetRepo().FindAll<CustomerConsent>()
                   .BySecureToken(packet.Dto.ConsentId)
                   .ByConsentStatus(CustomerConsentStatusEnum.Pending.Value)
                   .FirstOrDefaultAsync();

            if (AppointmentExists == null)
            {
                packet.AddError("Error", "An appointment for this date and time does not exist, has expired, has been rejected or has already been accepted.");
            }

            await Task.CompletedTask; //If you have any await in the validation, remove this line

        }

    }
}
