using Sumeru.Flex;

namespace ENTiger.ENCollect.PublicModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ImportAccountsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ImportAccountsMapperConfiguration() : base()
        {
            CreateMap<ImportAccountsDto, MasterFileStatus>();
        }
    }
}