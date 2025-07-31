using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class DbUtilityFactory : IFlexUtilityService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<DBTypeEnum, Type> _dbTypeMappings;

        public DbUtilityFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            //Map the Enum strings witht he Concrete class types
            _dbTypeMappings = new Dictionary<DBTypeEnum, Type>
            {
                { DBTypeEnum.MySQL, typeof(MySqlUtility) },
                { DBTypeEnum.MsSQL, typeof(MsSqlUtility) }
            };
        }

        public virtual IDbUtility GetUtility(DBTypeEnum dbType)
        {
            //fetch the service type from the mapping dict.
            if (_dbTypeMappings.TryGetValue(dbType, out var serviceType))
            {
                //TODO: review change to get scope - was causing error
                var scope = _serviceProvider.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService(serviceType);
                return (IDbUtility)service;
                //return (IDbUtility)_serviceProvider.GetRequiredService(serviceType);              
            }

            throw new InvalidOperationException("Invalid Db Type");
        }

    }
}