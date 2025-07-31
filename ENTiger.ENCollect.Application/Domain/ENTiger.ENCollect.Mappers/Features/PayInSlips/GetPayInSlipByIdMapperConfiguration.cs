using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetPayInSlipByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetPayInSlipByIdMapperConfiguration() : base()
        {
            CreateMap<PayInSlip, GetPayInSlipByIdDto>()
                .ForMember(d => d.PayInSlipNo, s => s.MapFrom(s => s.CustomId))
                .ForMember(d => d.Latitude, s => s.MapFrom(s => s.Lattitude))
                .ForMember(d => d.CreatedDate, s => s.MapFrom(s => s.CreatedDate.DateTime))
                .ForMember(d => d.DepositSlipBranchName, s => s.MapFrom(s => s.BranchName))
                .ForMember(d => d.BatchIds, s => s.MapFrom(s => s.CollectionBatches.Select(x => new PayInSlipBatchDto { CustomId = x.CustomId }).ToList()));
        }
    }
}