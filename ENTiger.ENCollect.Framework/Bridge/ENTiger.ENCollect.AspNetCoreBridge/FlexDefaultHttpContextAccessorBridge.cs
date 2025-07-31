using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;
using System;
using System.Linq;

namespace ENTiger.ENCollect
{
    public class FlexDefaultHttpContextAccessorBridge : FlexDefaultHttpContextAccessor, IFlexHostHttpContextAccesorBridge
    {
        private IApplicationUserUtility _applicationUser;
        public string TenantId { get; protected set; }
        public string RequestSource { get; protected set; }
        public string ClientIP { get; protected set; }

        public FlexDefaultHttpContextAccessorBridge(IHttpContextAccessor httpContextAccessor, IApplicationUserUtility applicationUser) : base(httpContextAccessor)
        {
            _applicationUser = applicationUser;
            //Uncomment the below lines for which you want to enable the code:
            SetCorrelationId();
            SetRequestSource();
            SetTenantId();
            SetUserId();
            SetHostName();
            SetClientIP();
            //SetUserName();
        }

        public override void SetCorrelationId()
        {
            base.SetCorrelationId();
        }

        public void SetTenantId()
        {
            this.TenantId = _httpContextAccessor.HttpContext.Request.Headers["tenantId"].FirstOrDefault();
        }

        public void SetRequestSource()
        {
            this.RequestSource = _httpContextAccessor.HttpContext.Request.Headers["X-Request-Source"].FirstOrDefault() ?? "unknown";
        }

        public override void SetHostName()
        {
            IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory = FlexContainer.ServiceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();

            string hostName = _repoTenantFactory.FindAll<FlexTenantBridge>().Where(x => x.Id == TenantId).Select(x => x.HostName).FirstOrDefault();

            this.HostName = hostName;
            //this.HostName = _httpContextAccessor.HttpContext.Request.Host.Value;
        }

        public override void SetUserId()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                string authUserId = _httpContextAccessor.HttpContext.User.Claims.Where(a => string.Equals(a.Type, "sub", StringComparison.OrdinalIgnoreCase)).FirstOrDefault()?.Value;

                FlexAppContextBridge context = new FlexAppContextBridge()
                {
                    TenantId = this.TenantId
                };
                this.UserId = _applicationUser.GetApplicationUserId(authUserId, context);
            }
        }

        public override void SetUserName()
        {
            this.UserName = _httpContextAccessor.HttpContext.User.Claims.Where(a => string.Equals(a.Type, "email", StringComparison.OrdinalIgnoreCase)).FirstOrDefault()?.Value;

        }
        public void SetClientIP()
        {
            this.ClientIP = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        }
    }
}