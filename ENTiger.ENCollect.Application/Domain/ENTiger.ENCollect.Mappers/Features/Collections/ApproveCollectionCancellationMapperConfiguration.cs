using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ApproveCollectionCancellationMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ApproveCollectionCancellationMapperConfiguration() : base()
        {
            CreateMap<ApproveCollectionCancellationDto, Collection>();
        }
    }
}