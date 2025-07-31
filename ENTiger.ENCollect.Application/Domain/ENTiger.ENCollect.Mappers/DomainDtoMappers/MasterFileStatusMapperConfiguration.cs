using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class MasterFileStatusMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public MasterFileStatusMapperConfiguration() : base()
        {
            CreateMap<MasterFileStatusDto, MasterFileStatus>();
            CreateMap<MasterFileStatus, MasterFileStatusDto>();
            CreateMap<MasterFileStatusDtoWithId, MasterFileStatus>();
            CreateMap<MasterFileStatus, MasterFileStatusDtoWithId>();
        }
    }
}