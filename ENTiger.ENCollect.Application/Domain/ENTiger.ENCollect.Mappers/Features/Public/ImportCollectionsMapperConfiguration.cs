using Sumeru.Flex;

namespace ENTiger.ENCollect.PublicModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ImportCollectionsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public ImportCollectionsMapperConfiguration() : base()
        {
            CreateMap<ImportCollectionsDto, Collection>();

        }
    }
}
