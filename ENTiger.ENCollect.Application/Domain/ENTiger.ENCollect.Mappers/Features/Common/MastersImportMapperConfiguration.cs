using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class MastersImportMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public MastersImportMapperConfiguration() : base()
        {
            CreateMap<MastersImportDto, MasterFileStatus>();
        }
    }
}