using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateSettlementMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public UpdateSettlementMapperConfiguration() : base()
        {
            CreateMap<UpdateSettlementDto, Settlement>()
                 .ForMember(dest => dest.TOS, opt => opt.MapFrom(src => src.PrincipalOutstanding))
                  .ForMember(dest => dest.RenegotiationAmount, opt => opt.MapFrom(src => src.RenegotiateAmount))
                .ForMember(dest => dest.CURRENT_BUCKET, opt => opt.MapFrom(src => src.CurrentBucket));
            CreateMap<WaiverDetailDto, WaiverDetail>();
            CreateMap<InstallmentDetailDto, InstallmentDetail>();
            CreateMap<DocumentsDto, SettlementDocument>();
        }
    }
}
