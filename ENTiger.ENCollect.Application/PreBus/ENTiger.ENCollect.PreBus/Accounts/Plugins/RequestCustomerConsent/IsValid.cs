using DocumentFormat.OpenXml.Office.PowerPoint.Y2023.M02.Main;
using DocumentFormat.OpenXml.Wordprocessing;
using ENTiger.ENCollect.DomainModels.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule.RequestCustomerConsentAccountsPlugins
{
    public partial class IsValid : FlexiBusinessRuleBase, IFlexiBusinessRule<RequestCustomerConsentDataPacket>
    {
        public override string Id { get; set; } = "3a18827c6553b13321f97598445d629e";
        public override string FriendlyName { get; set; } = "IsValid";

        protected readonly ILogger<IsValid> _logger;
        protected readonly IRepoFactory _repoFactory;

        public IsValid(ILogger<IsValid> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(RequestCustomerConsentDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);

            //Is account valid & active
            if (!String.IsNullOrEmpty(packet.Dto.AccountId))
            {
                var AccountExists = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                    .ByAccountId(packet.Dto.AccountId)
                    .ByLoanAccountStatus(LoanAccountStatusEnum.Live.Value)
                    .FirstOrDefaultAsync();
                
                if (AccountExists == null || AccountExists.IsDeleted)
                {
                    string message = AccountExists?.AGREEMENTID == null ? "not found. Account status not Live: id " + packet.Dto.AccountId : AccountExists?.AGREEMENTID + " number is marked as deleted";
                    packet.AddError("Error", $"Account {message}.");
                }
                else
                {
                    packet.MobileNumber = AccountExists.MAILINGMOBILE ?? "";
                    packet.EmailId = AccountExists.EMAIL_ID ?? "";
                }
            }
            else
            {
                packet.AddError("Error", "The AccountId field is required.");
            }
            //is requested date valid
            if (packet.Dto.RequestedVisitTime != null && packet.Dto.RequestedVisitTime.Value != DateTime.MinValue)
            {
                DateTime appointmentDate = packet.Dto.RequestedVisitTime.Value;
                DateTime minTime = new DateTime(DateTime.Today.Year,DateTime.Today.Month,DateTime.Today.Day,19,0,0);
                DateTime tomorrow = DateTime.Today.AddDays(1);
                DateTime maxTime = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 7, 0, 0);
                if (appointmentDate < minTime || appointmentDate > maxTime)
                {
                    packet.AddError("Error", $"The Requested Visit Time is outside allowable appointment times {minTime} to {maxTime}.");
                }
            }
            else
            {
                packet.AddError("Error", $"The Requested Visit Time {packet.Dto.RequestedVisitTime.Value} is incorrect.");
            }

            //check dup
            var AppointmentExists = await _repoFactory.GetRepo().FindAll<CustomerConsent>()
                    .ByConsentAccountId(packet.Dto.AccountId)
                    .ByAppointmentDate(packet.Dto.RequestedVisitTime)
                    .FirstOrDefaultAsync();

            if (AppointmentExists != null)
            {
                packet.AddError("Error", "An appointment for this date and time already exists.");
            }

        }

    }
}
