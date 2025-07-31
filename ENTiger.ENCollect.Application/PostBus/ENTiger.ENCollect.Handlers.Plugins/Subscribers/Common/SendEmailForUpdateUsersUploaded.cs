using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class SendEmailForUpdateUsersUploaded : ISendEmailForUpdateUsersUploaded
    {
        protected readonly ILogger<SendEmailForUpdateUsersUploaded> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEmailForUpdateUsersUploaded(ILogger<SendEmailForUpdateUsersUploaded> logger,
            IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(UpdateUsersUploadedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation("SendEmailForUpdateUsersUploaded : Start");
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);
            //TODO: Write your business logic here:
            var userUpdateFile = await repo.GetRepo().FindAll<UsersUpdateFile>()
                                    .Where(u => u.Id == @event.Id)
                                    .SelectTo<UsersUpdateFileDtoWithId>()
                                    .FirstOrDefaultAsync();

            var PrimaryEMail = await repo.GetRepo().FindAll<ApplicationUser>()
                                    .Where(a => a.Id == userUpdateFile.CreatedBy)
                                    .Select(b => b.PrimaryEMail)
                                    .FirstOrDefaultAsync();

            //ServiceLayerTemplate
            var messageTemplate = _messageTemplateFactory.UploadUsersUploadedTemplate(userUpdateFile.CustomId, userUpdateFile.FileName, userUpdateFile.UploadType, @event.AppContext.HostName);

            _logger.LogInformation("SendEmailForUpdateUsersUploaded : SendMail : Email - " + PrimaryEMail);
            await _emailUtility.SendEmailAsync(PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);
            _logger.LogInformation("SendEmailForUpdateUsersUploaded : SendMail : Sent Email - " + PrimaryEMail);

            _logger.LogInformation("SendEmailForUpdateUsersUploaded : End");
        }
    }
}