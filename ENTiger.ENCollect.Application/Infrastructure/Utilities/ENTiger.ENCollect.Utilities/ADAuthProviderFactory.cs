using Microsoft.Extensions.DependencyInjection;

namespace ENTiger.ENCollect
{
    public class ADAuthProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ADAuthProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IADAuthProvider GetADAuthProvider(string adAuthProvider)
        {
            return adAuthProvider switch
            {
                // FlexContainer.ServiceProvider.
                "ldap" => _serviceProvider.GetRequiredService<LDAPAuthProvider>(),
                _ => throw new InvalidOperationException("Invalid AD Auth provider type")
            };
        }
    }
}