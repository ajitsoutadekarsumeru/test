using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UsersUpdateFileMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UsersUpdateFileMapperConfiguration() : base()
        {
            CreateMap<UsersUpdateFileDto, UsersUpdateFile>();
            CreateMap<UsersUpdateFile, UsersUpdateFileDto>();
            CreateMap<UsersUpdateFileDtoWithId, UsersUpdateFile>();
            CreateMap<UsersUpdateFile, UsersUpdateFileDtoWithId>();
        }
    }
}