using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class SendEmailOnAgencyRejected : ISendEmailOnAgencyRejected
    {
        protected readonly ILogger<SendEmailOnAgencyRejected> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public SendEmailOnAgencyRejected(ILogger<SendEmailOnAgencyRejected> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AgencyRejected @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; 

            await this.Fire<SendEmailOnAgencyRejected>(EventCondition, serviceBusContext);
        }
    }
}