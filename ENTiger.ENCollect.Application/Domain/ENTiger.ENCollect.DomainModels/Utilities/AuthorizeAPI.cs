using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ENTiger.ENCollect
{
    public class AuthorizeAPI : Attribute, IResourceFilter
    {
        private string configApikey = AppConfigManager.GetSection("AccountImportSettings")["APIKey"] ?? "";

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var headers = context.HttpContext.Request.Headers;

            if (headers == null || !headers.TryGetValue("apikey", out var apikey) || configApikey != apikey)
            {
                context.Result = new ContentResult
                {
                    Content = "{resultcode = \"01\", message = \"failed\", reason = \"API Key MisMatch\"}",
                    StatusCode = 200
                };
                return;
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
    }
}