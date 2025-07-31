using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetPayInSlipsForAckMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetPayInSlipsForAckMapperConfiguration() : base()
        {
            CreateMap<PayInSlip, GetPayInSlipsForAckDto>()
                .ForMember(d => d.CMSPayInSlipId, s => s.MapFrom(s => s.CMSPayInSlipNo))
                .ForMember(d => d.ENCollectPayInSlipNumber, s => s.MapFrom(s => s.CustomId))
                .ForMember(d => d.PayinSlipType, s => s.MapFrom(s => s.PayinslipType))
                .ForMember(d => d.BatchIds, s => s.MapFrom(s => s.CollectionBatches.Select(x => new PayInSlipBatchIdDto { CustomId = x.CustomId })));
        }
    }
}