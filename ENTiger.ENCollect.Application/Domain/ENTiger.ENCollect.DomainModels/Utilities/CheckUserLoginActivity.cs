using ENTiger.ENCollect;
using ENTiger.ENCollect.ApplicationUsersModule;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.Core.Utilities
{
    public class CheckUserLoginActivity : Attribute, IActionFilter
    {
        protected readonly IUserUtility _userUtility;
        protected ILogger<CheckUserLoginActivity> _logger = FlexContainer.ServiceProvider.GetService<ILogger<CheckUserLoginActivity>>();
        public CheckUserLoginActivity(ILogger<CheckUserLoginActivity> logger, IUserUtility userUtility)
        {
            // Initialize any required values
            _logger = logger;
            _userUtility = userUtility;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
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

                if (context.ActionArguments.Count > 0)
                {
                    foreach (var arg in context.ActionArguments)
                    {
                        var model = arg.Value;

                        if (model != null)
                        {
                            Type modeltype = model.GetType();
                            _logger.LogInformation("CheckUserLoginActivity inside IF Loop");
                            if (modeltype.Name == "AddUserAttendanceDto")
                            {
                                _logger.LogInformation("CheckUserLoginActivity inside AddUserAttendanceAPIModel IF Loop");

                                FlexAppContextBridge hostContextInfo = new FlexAppContextBridge()
                                {
                                    TenantId = TenantId,
                                    UserId = _UserID
                                };

                                AddUserAttendanceDto input = (model as AddUserAttendanceDto);
                                input.SetAppContext(hostContextInfo);
                                _logger.LogInformation("Inside AddUserAttendanceAPIModel input.LogInLatitude input.LogInLongitude " + lat + lng);
                                lat = input.LogInLatitude;
                                lng = input.LogInLongitude;
                                _logger.LogInformation("Inside AddUserAttendanceAPIModel input.LogInLatitude input.LogInLongitude " + lat + lng);

                                _userUtility.InsertUserActivityDetails(_UserID, "Login", lat, lng, input);
                            }
                            if (modeltype.Name == "UpdateUserAttendanceDto")
                            {
                                FlexAppContextBridge hostContextInfo = new FlexAppContextBridge()
                                {
                                    TenantId = TenantId,
                                    UserId = _UserID
                                };
                                UpdateUserAttendanceDto input = (model as UpdateUserAttendanceDto);
                                input.SetAppContext(hostContextInfo);
                                lat = input.LogOutLatitude;
                                lng = input.LogOutLongitude;

                                _userUtility.InsertUserActivityDetails(_UserID, "Logout", lat, lng, input);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception in CheckUserLoginActivity " + ex);
            }
        }
    }
}