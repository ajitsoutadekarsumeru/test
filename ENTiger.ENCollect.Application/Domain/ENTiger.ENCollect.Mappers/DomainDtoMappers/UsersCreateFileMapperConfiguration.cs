using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UsersCreateFileMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UsersCreateFileMapperConfiguration() : base()
        {
            CreateMap<UsersCreateFileDto, UsersCreateFile>();
            CreateMap<UsersCreateFile, UsersCreateFileDto>();
            CreateMap<UsersCreateFileDtoWithId, UsersCreateFile>();
            CreateMap<UsersCreateFile, UsersCreateFileDtoWithId>();
        }
    }
}