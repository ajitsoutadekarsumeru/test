using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchAccountImportFileStatusMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchAccountImportFileStatusMapperConfiguration() : base()
        {
            CreateMap<MasterFileStatus, SearchAccountImportFileStatusDto>()
                .ForMember(d => d.TransactionId, s => s.MapFrom(s => s.CustomId));
        }
    }
}