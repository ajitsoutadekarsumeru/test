using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.UserManagement.Staff
{
    public class CompanyUserRejectedEmailNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(CompanyUserDtoWithId _dto)
        {
            string UserName = _dto.FirstName + " " + _dto.LastName;
            EmailMessage = "Request for profile creation for your ID has been rejected. Contact your supervisor for details.";

            EmailSubject = "RejectedStaff";
        }
    }
}