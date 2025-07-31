using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ForgotPasswordNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;
        private string baseUrl = string.Empty;
        private ILogger<ForgotPasswordNotification> _logger = FlexContainer.ServiceProvider.GetService<ILogger<ForgotPasswordNotification>>();
        private readonly AuthSettings _authSettings;

        public ForgotPasswordNotification(IOptions<AuthSettings> authSettings)
        {
            _authSettings = authSettings.Value;
        }

        public virtual void ConstructData(SendForgotPasswordDto dto)
        {
            baseUrl = _authSettings.BaseUrl;
            _logger.LogInformation("ForgotPasswordNotification : Start");
            var callbackUrl = baseUrl + "#/ResetPassword?Code=" + dto.Code + "&Email=" + dto.Email + "&TenantId=" + dto.GetAppContext().TenantId;

            EmailMessage = "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a><br/>";

            EmailSubject = "Reset Password";
            _logger.LogInformation("ForgotPasswordNotification : End");
        }
    }
}