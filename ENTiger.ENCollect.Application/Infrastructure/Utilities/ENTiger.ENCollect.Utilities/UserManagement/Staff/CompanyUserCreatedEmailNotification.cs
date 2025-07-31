using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.UserManagement.Staff
{
    public class CompanyUserCreatedEmailNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(CompanyUserDtoWithId _dto)
        {
            string UserName = _dto?.FirstName + " " + _dto?.LastName;
            EmailMessage = "Your profile has been created with UserName :" + _dto?.PrimaryEMail + ". Kindly await approval for the same.";

            EmailSubject = "Company User - " + UserName + " Created";
        }
    }
}