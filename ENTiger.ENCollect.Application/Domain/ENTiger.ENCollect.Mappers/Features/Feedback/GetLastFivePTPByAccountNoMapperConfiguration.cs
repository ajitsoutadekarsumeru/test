using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetLastFivePTPByAccountNoMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetLastFivePTPByAccountNoMapperConfiguration() : base()
        {
            CreateMap<Feedback, GetLastFivePTPByAccountNoDto>()
            .ForMember(cm => cm.ContactNumber, Dm => Dm.MapFrom(dModel => dModel.NewContactNo))
            .ForMember(cm => cm.NewContactNumber, Dm => Dm.MapFrom(dModel => dModel.NewContactNo))
            ;
        }
    }
}