using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetBucketListMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetBucketListMapperConfiguration() : base()
        {
            CreateMap<Bucket, GetBucketListDto>();
        }
    }
}