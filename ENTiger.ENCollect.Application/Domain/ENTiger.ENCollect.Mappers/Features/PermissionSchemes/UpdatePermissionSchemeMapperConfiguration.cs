using Sumeru.Flex;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdatePermissionSchemeMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public UpdatePermissionSchemeMapperConfiguration() : base()
        {
            CreateMap<UpdatePermissionSchemeDto, PermissionSchemes>();

        }
    }
}
