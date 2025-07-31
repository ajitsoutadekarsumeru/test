using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.UserManagement.Staff
{
    public class CompanyUserApprovedEmailNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(CompanyUserDtoWithId _dto, string callbackUrl)
        {
            string UserName = _dto.FirstName + " " + _dto.LastName;
            EmailMessage = "Your id has been approved by Encollect team. Your profile User Name : " + _dto.PrimaryEMail
                  + ". To create your password, " + "<a href=\"" + callbackUrl + "\" > CLICK HERE</a> " + "<br/><br/>";

            EmailSubject = "ApprovedStaff";
        }
    }
}