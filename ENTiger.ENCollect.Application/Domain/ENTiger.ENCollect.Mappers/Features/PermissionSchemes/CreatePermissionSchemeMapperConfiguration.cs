using Sumeru.Flex;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CreatePermissionSchemeMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public CreatePermissionSchemeMapperConfiguration() : base()
        {
            CreateMap<CreatePermissionSchemeDto, PermissionSchemes>();

        }
    }
}
