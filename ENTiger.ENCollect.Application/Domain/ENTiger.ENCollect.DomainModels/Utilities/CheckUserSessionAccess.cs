using ENTiger.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CheckUserSessionAccess : Attribute, IResourceFilter
    {
        private readonly SessionSettings _sessionSettings;
        public CheckUserSessionAccess(IOptions<SessionSettings> sessionSettings)
        {
            _sessionSettings = sessionSettings.Value;
        }
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            // Check if AllowAnonymous is applied to this action
            var isAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                .OfType<AllowAnonymousAttribute>()
                .Any();

            if (isAllowAnonymous)
            {
                // Skip filter logic
                return;
            }

            var headers = context.HttpContext.Request.Headers;

            if (headers == null)
            {
                context.Result = new ContentResult
                {
                    Content = "{\"errors\":{\"Error\":[\"Unauthorized\"]}}",
                    StatusCode = 401
                };
                return;
            }

            string authorizeToken = headers["Authorization"].FirstOrDefault() ?? headers["authorization"].FirstOrDefault();
            string tenantId = headers["TenantId"].FirstOrDefault() ?? headers["tenantid"].FirstOrDefault();
            string userId = context.HttpContext.User?.Claims?.FirstOrDefault(a => a.Type.Equals("sub", StringComparison.OrdinalIgnoreCase))?.Value;

            if (string.IsNullOrEmpty(authorizeToken) || string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(userId))
            {
                context.Result = new ContentResult
                {
                    Content = "{\"errors\":{\"Error\":[\"Unauthorized\"]}}",
                    StatusCode = 401
                };
                return;
            }

            IRepoFactory _repoFactory = FlexContainer.ServiceProvider.GetRequiredService<IRepoFactory>();
            var appContext = new FlexAppContextBridge { TenantId = tenantId };
            _repoFactory.Init(appContext);
            string applicationUserId = _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.UserId == userId).Select(x => x.Id).FirstOrDefault();
            if (string.IsNullOrEmpty(applicationUserId))
            {
                context.Result = new ContentResult
                {
                    Content = "{\"errors\":{\"Error\":[\"Unauthorized\"]}}",
                    StatusCode = 401
                };
                return;
            }

            if (_sessionSettings.EnableSessionCheck == true)
            {
                var userAttendanceLog = _repoFactory.GetRepo().FindAll<UserAttendanceLog>().Where(x => x.ApplicationUserId == applicationUserId
                                            && x.SessionId == authorizeToken && x.IsSessionValid == true).Select(x => x.Id).FirstOrDefault() ?? "";

                if (string.IsNullOrEmpty(userAttendanceLog))
                {
                    context.Result = new ContentResult
                    {
                        Content = "{\"errors\":{\"Error\":[\"Invalid session, please login again\"]}}",
                        StatusCode = 401
                    };
                    return;
                }
            }

        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
    }
}