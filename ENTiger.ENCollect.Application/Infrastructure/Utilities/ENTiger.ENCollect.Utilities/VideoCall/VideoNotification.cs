using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class VideoNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        private readonly NotificationSettings _notificationSettings;

        public VideoNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(string name, string link, string tinylink)
        {
            string SMSSignature = _notificationSettings.SmsSignature;
            EmailSubject = "Join video call with MyBank";

            EmailMessage = "Dear " + name + ",<br/>" +
                             "You can join the video call with our executive by clicking on the below link.<br/>" +
                             "<p>" +
                            "<a href=\"" + link + "\"> Click Here</a><br/>";

            SMSMessage = "Dear " + name + ", You can join the video call with our executive by clicking on the below link : " + tinylink + ". Thanks " + SMSSignature + "";
        }
    }
}