using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddCollectionCancellationMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AddCollectionCancellationMapperConfiguration() : base()
        {
            CreateMap<AddCollectionCancellationDto, Collection>();
        }
    }
}