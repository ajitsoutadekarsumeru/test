using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetInstallmentsByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetInstallmentsByIdMapperConfiguration() : base()
        {
            CreateMap<List<InstallmentDetail>, GetInstallmentsByIdDto>()
                    .ForMember(dest => dest.Installments, opt => opt.MapFrom(src => src));

            CreateMap<InstallmentDetail, InstallmentDetailDto>()
                .ForMember(dest => dest.InstallmentDueDate, opt => opt.MapFrom(src => src.InstallmentDueDate.UtcDateTime));
        }
    }
}
