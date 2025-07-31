using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAckBatchesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetAckBatchesMapperConfiguration() : base()
        {
            CreateMap<CollectionBatch, GetAckBatchesDto>()
                .ForMember(vm => vm.BatchCode, Dm => Dm.MapFrom(dModel => dModel.CustomId))
                .ForMember(vm => vm.Receiptcount, Dm => Dm.MapFrom(dModel => dModel.Collections.Count()))
                .ForMember(vm => vm.ReceiptIssuedBy, Dm => Dm.MapFrom(dModel => dModel.CreatedBy))
                .ForMember(vm => vm.CreatedDate, Dm => Dm.MapFrom(dModel => dModel.CreatedDate));
        }
    }
}