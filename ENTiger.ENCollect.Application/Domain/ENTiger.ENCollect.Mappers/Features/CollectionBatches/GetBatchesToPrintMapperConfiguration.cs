using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetBatchesToPrintMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetBatchesToPrintMapperConfiguration() : base()
        {
            CreateMap<CollectionBatch, GetBatchesToPrintDto>()
            .ForMember(vm => vm.BatchCode, Dm => Dm.MapFrom(dModel => dModel.CustomId))
            .ForMember(vm => vm.Amount, Dm => Dm.MapFrom(dModel => dModel.Amount))
            .ForMember(vm => vm.ModeOfPayment, Dm => Dm.MapFrom(dModel => dModel.ModeOfPayment));
            // .ForMember(vm => vm.CreatedDate, Dm => Dm.MapFrom(dModel => dModel.CreatedDate))
            //.ForMember(vm => vm.status, Dm => Dm.MapFrom(dModel => dModel.CollectionBatchWorkflowState.Name))

            ;
        }
    }
}