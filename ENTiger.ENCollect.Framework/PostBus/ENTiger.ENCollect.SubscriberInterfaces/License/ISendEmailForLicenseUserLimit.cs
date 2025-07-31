using ENTiger.ENCollect.Messages.Events.License;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public interface ISendEmailForLicenseUserLimit : IAmFlexSubscriber<UserLicenseLimitReachedEvent>
    {
    }
}
