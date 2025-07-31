using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public static class LicenseExtensions
    {
        // Middleware to block requests if license invalid
        public static IApplicationBuilder UseLicenseValidation(this IApplicationBuilder app)
        {
            return app.Use(async (context, next) =>
            {
                var licenseService = FlexContainer.ServiceProvider.GetRequiredService<ILicenseService>();
                if (!licenseService.IsLicenseValid())
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Application license invalid or expired.");
                    return;
                }

                await next();
            });
        }
    }
}
