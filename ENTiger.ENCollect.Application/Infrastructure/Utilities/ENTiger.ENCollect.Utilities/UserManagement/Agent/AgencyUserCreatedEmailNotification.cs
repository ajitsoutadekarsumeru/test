using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.UserManagement.Agent
{
    public class AgencyUserCreatedEmailNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(AgencyUserDtoWithId _dto)
        {
            string emailId = _dto.PrimaryEMail;
            string companyUserName = _dto.FirstName;
            string userId = _dto.UserId;
            EmailMessage = "Your profile has been created with UserName :" + emailId + ". Kindly await approval for the same.";

            EmailSubject = "Agent: - " + companyUserName + " Created.";
        }
    }
}