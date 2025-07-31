using Sumeru.Flex;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    public partial class CreatePermissionSchemeChangeLogMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public CreatePermissionSchemeChangeLogMapperConfiguration() : base()
        {
            CreateMap<PermissionSchemeChangeLogDto, PermissionSchemeChangeLog>();

        }
    }
}
