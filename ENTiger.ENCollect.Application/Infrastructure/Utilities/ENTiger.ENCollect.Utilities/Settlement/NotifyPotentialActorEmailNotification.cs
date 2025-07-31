using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.UserManagement.Agent
{
    public class NotifyPotentialActorEmailNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(SettlementDtoWithId _dto)
        {
            string UserName = _dto.UserName;
            EmailMessage = $"Dear {UserName}, Settlement ID {_dto.CustomId} pending at your queue.";

            EmailSubject = $"Settlement - {_dto.CustomId} : Action Pending";
        }
    }
}