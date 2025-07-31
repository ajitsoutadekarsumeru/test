using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class BulkTrailUploadFileMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public BulkTrailUploadFileMapperConfiguration() : base()
        {
            CreateMap<BulkTrailUploadFileDto, BulkTrailUploadFile>();
            CreateMap<BulkTrailUploadFile, BulkTrailUploadFileDto>();
            CreateMap<BulkTrailUploadFileDtoWithId, BulkTrailUploadFile>();
            CreateMap<BulkTrailUploadFile, BulkTrailUploadFileDtoWithId>();
        }
    }
}