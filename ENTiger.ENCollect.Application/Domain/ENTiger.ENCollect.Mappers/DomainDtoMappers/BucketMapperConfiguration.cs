using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class BucketMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public BucketMapperConfiguration() : base()
        {
            CreateMap<BucketDto, Bucket>();
            CreateMap<Bucket, BucketDto>();
            CreateMap<BucketDtoWithId, Bucket>();
            CreateMap<Bucket, BucketDtoWithId>();
        }
    }
}