using ENTiger.ENCollect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.Core.Utilities
{
    public class CheckUserActivity : Attribute, IResourceFilter
    {
        protected readonly IUserUtility _userUtility;
        protected ILogger<CheckUserActivity> _logger = FlexContainer.ServiceProvider.GetService<ILogger<CheckUserActivity>>();

        public CheckUserActivity(ILogger<CheckUserActivity> logger, IUserUtility userUtility)
        {
            // Initialize any required values
            _logger = logger;
            _userUtility = userUtility;
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            double? lat = 0;
            double? lng = 0;
            string _UserID = context.HttpContext.User.Claims.FirstOrDefault(a => a.Type.Equals("sub", StringComparison.OrdinalIgnoreCase))?.Value;


            var headers = context.HttpContext.Request.Headers;
            string TenantId = string.Empty;
            if (headers == null)
            {
                context.Result = new ContentResult
                {
                    Content = "{\"Error\":[\"Unauthorized user.\"]}",
                    StatusCode = 401
                };
            }
            else
            {
                TenantId = headers["TenantId"];

                if (string.IsNullOrEmpty(TenantId))
                {
                    TenantId = headers["tenantid"];
                }
            }

            _userUtility.InsertUserActivityDetails(_UserID, "Others", lat, lng, "");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //throw new NotImplementedException();
        }
    }
}