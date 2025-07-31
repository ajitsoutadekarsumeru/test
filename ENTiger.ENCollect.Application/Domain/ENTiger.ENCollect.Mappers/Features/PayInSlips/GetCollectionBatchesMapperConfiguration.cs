using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCollectionBatchesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetCollectionBatchesMapperConfiguration() : base()
        {
            CreateMap<CollectionBatch, GetCollectionBatchesDto>();

            CreateMap<CollectionBatch, PayInSlipCollectionBatchDto>()
                .ForMember(d => d.BatchCode, s => s.MapFrom(s => s.CustomId))
                .ForMember(d => d.Amount, s => s.MapFrom(s => s.Amount))
                .ForMember(d => d.ModeOfPayment, s => s.MapFrom(s => s.ModeOfPayment))
                .ForMember(d => d.CreatedDate, s => s.MapFrom(s => s.CreatedDate));

            CreateMap<Collection, PayInSlipCollectionDetailsDto>()
                .ForMember(vm => vm.ReceiptNo, Dm => Dm.MapFrom(dModel => dModel.CustomId))
                .ForMember(vm => vm.PaymentMode, Dm => Dm.MapFrom(dModel => dModel.CollectionMode))
                .ForMember(vm => vm.CollecterCode, Dm => Dm.MapFrom(dModel => dModel.Collector.CustomId))
                .ForMember(vm => vm.CollecterName, Dm => Dm.MapFrom(dModel => dModel.Collector.FirstName));

            CreateMap<Cheque, ChequeDetailsDto>();
        }
    }
}