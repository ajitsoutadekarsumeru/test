using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCollectionByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetCollectionByIdMapperConfiguration() : base()
        {
            CreateMap<Collection, GetCollectionByIdDto>()
            .ForMember(vm => vm.BatchId, Dm => Dm.MapFrom(dModel => dModel.CollectionBatchId))
            .ForMember(vm => vm.CustomerAccountNo, Dm => Dm.MapFrom(dModel => dModel.Account.CustomId))
            .ForMember(vm => vm.EmiAmount, Dm => Dm.MapFrom(dModel => dModel.Account.EMIAMT))
            .ForMember(vm => vm.Product, Dm => Dm.MapFrom(dModel => dModel.Account.PRODUCT))
            .ForMember(vm => vm.ReceiptDate, Dm => Dm.MapFrom(dModel => dModel.CollectionDate))
            .ForMember(vm => vm.CollectorName, Dm => Dm.MapFrom(dModel => dModel.Collector.FirstName + " " + dModel.Collector.FirstName))
            .ForMember(vm => vm.CollectorId, Dm => Dm.MapFrom(dModel => dModel.Collector.Id))
            .ForMember(vm => vm.CollectorCode, Dm => Dm.MapFrom(dModel => dModel.Collector.CustomId))
            .ForMember(vm => vm.ReceivedAtAgency, Dm => Dm.MapFrom(dModel => dModel.AcknowledgedDate))
            .ForMember(vm => vm.ReceiptNo, Dm => Dm.MapFrom(dModel => dModel.CustomId))
            .ForMember(vm => vm.BankName, Dm => Dm.MapFrom(dModel => dModel.Cheque.BankName))
            .ForMember(vm => vm.BranchName, Dm => Dm.MapFrom(dModel => dModel.Cheque.BranchName))
            .ForMember(vm => vm.InstrumentNo, Dm => Dm.MapFrom(dModel => dModel.Cheque.InstrumentNo))
            .ForMember(vm => vm.InstrumentDate, Dm => Dm.MapFrom(dModel => dModel.Cheque.InstrumentDate))
            .ForMember(vm => vm.MicrCode, Dm => Dm.MapFrom(dModel => dModel.Cheque.MICRCode))
            .ForMember(vm => vm.BankCity, Dm => Dm.MapFrom(dModel => dModel.Cheque.BankCity))
            .ForMember(vm => vm.IfscCode, Dm => Dm.MapFrom(dModel => dModel.Cheque.IFSCCode))
            //.ForMember(vm => vm.IfscCode, Dm => Dm.MapFrom(p => (p.CollectionBatch != null) ? (p.CollectionBatch.PayInSlip != null ? p.CollectionBatch.PayInSlip.PayInSlipWorkflowState.Name : p.CollectionWorkflowState.Name) : p.CollectionWorkflowState.Name))
            ;
        }
    }
}