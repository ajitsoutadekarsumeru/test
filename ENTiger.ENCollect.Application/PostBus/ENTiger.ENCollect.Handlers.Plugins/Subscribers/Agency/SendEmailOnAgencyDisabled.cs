using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class SendEmailOnAgencyDisabled : ISendEmailOnAgencyDisabled
    {
        protected readonly ILogger<SendEmailOnAgencyDisabled> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public SendEmailOnAgencyDisabled(ILogger<SendEmailOnAgencyDisabled> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AgencyDisabled @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            await this.Fire<SendEmailOnAgencyDisabled>(EventCondition, serviceBusContext);
        }
    }
}