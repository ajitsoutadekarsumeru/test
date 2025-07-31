using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class BulkTrailUploadMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public BulkTrailUploadMapperConfiguration() : base()
        {
            CreateMap<BulkTrailUploadDto, BulkTrailUploadFile>();
        }
    }
}