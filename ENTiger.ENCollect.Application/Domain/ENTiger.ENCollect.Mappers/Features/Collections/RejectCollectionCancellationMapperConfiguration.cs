using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class RejectCollectionCancellationMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public RejectCollectionCancellationMapperConfiguration() : base()
        {
            CreateMap<RejectCollectionCancellationDto, Collection>();
        }
    }
}