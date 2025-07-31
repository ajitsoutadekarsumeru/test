using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class RequestSettlementMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public RequestSettlementMapperConfiguration() : base()
        {
            CreateMap<RequestSettlementDto, Settlement>()
                .ForMember(dest => dest.WaiverDetails, opt => opt.MapFrom(src => src.WaiverDetails.ToList() as IReadOnlyCollection<string>))
                .ForMember(dest => dest.Installments, opt => opt.MapFrom(src => src.Installments.ToList() as IReadOnlyCollection<string>))
                .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.Documents.ToList() as IReadOnlyCollection<string>))
                 .ForMember(dest => dest.TOS, opt => opt.MapFrom(src => src.PrincipalOutstanding))
                .ForMember(dest => dest.CURRENT_BUCKET, opt => opt.MapFrom(src => src.CurrentBucket));

        }
    }
}
