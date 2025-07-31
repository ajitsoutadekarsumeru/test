using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public static class SwaggerConfig
    {        
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SchemaFilter<FlexSwaggerSchemaFilter>();
                c.OperationFilter<FlexSwaggerStandardOperationFilter>();

                // Define the custom header
                c.AddSecurityDefinition("TenantId", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "TenantId",
                    Type = SecuritySchemeType.ApiKey,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "TenantId"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                c.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Authorization"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        public static void ConfigureAPIs(this IServiceCollection services)
        {
            List<string> mobileApiPatterns = new List<string>
            {
                "api/account/GetKey",
                "api/mvp/addupdate/device",
                "api/mvp/device/validateotp",
                "api/Account/SendOTP/Login",
                "api/mvp/device/verify",
                "api/Account/VerifyOTP/Login",
                "api/mvp/subscription/getSubscription",
                "api/mvp/getFeatures",
                "api/mvp/getlogo",
                "api/mvp/um/loggedinuser/details",
                "api/mvp/add/userattendance",
                "api/mvp/CommunicationTask/broadcastDeviceDetail/Add",
                "api/mvp/banklist",
                "api/mvp/dispositiongroupmaster",
                "api/mvp/receipts/myreceiptallocations",
                "api/mvp/account/feedback/today/ptp/list",
                "api/mvp/dispositionCodemaster",
                "api/mvp/account/offline/myaccounts",
                "api/mvp/add/collection",
                "api/mvp/account/feedback/add",
                "api/mvp/payment/myreceipts",
                "api/mvp/account/CreditAccount",
                "api/mvp/payment/duplicate_send_email?paymentId=",
                "api/mvp/payment/duplicate_send_sms?paymentId=",
                "api/mvp/myaccount/list/0/10",
                "api/mvp/myaccount/attempted/list/0/10",
                "api/mvp/myaccount/unattempted/list/0/10",
                "api/mvp/myaccount/paid/list/0/10",
                "api/mvp/get/publicPrimaryCategoryItems?categoryMasterId=ProductGroup",
                "api/mvp/Master/All/City",
                "api/mvp/basebranch/list",
                "api/mvp/agencylist",
                "api/mvp/Staff/List",
                "api/mvp/Supervisormyaccount/list",
                "api/mvp/account/top/ten/list",
                "api/mvp/settlement/validate/loanaccount",
                "api/mvp/account/search",
                "api/mvp/dispositiongroupmaster",
                "api/mvp/account/",
                "api/mvp/account/feedback/add",
                "api/mvp/receipts/account/last/ABCD123456/3",
                "api/mvp/account/feedback/last/list",
                "api/mvp/account/search",
                "api/mvp/get/supervisorgeoTagDetails",
                "api/mvp/get/supervisorgeoTagDetails",
                "api/mvp/payment/mycollection",
                "api/mvp/getimage/?filename=PNG2_20200421082103000.jpeg",
                "api/mvp/account/mobilenolist",
                "api/Account/ChangePassword"
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mobile API's", Version = "v1" });
                c.AddServer(new OpenApiServer
                {
                    Url = "https://mtqa.sumeruentiger.com:9117/webapi",
                    Description = "Base API path"
                });
                c.DocInclusionPredicate((documentName, apiDescription) =>
                {
                    var actionDescriptor = apiDescription.ActionDescriptor as ControllerActionDescriptor;
                    if (actionDescriptor != null)
                    {
                        var controllerName = actionDescriptor.ControllerName;
                        var routeTemplate = apiDescription.RelativePath;

                        // Check if the route matches any pattern
                        bool routeMatch = true;//mobileApiPatterns.Any(a => a.StartsWith(routeTemplate));

                        return routeMatch;
                    }
                    return false;
                });
            });
        }
    }
}
