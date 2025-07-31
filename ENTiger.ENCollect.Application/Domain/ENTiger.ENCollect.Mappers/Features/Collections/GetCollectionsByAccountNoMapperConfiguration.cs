using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCollectionsByAccountNoMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetCollectionsByAccountNoMapperConfiguration() : base()
        {
            CreateMap<Collection, GetCollectionsByAccountNoDto>()
            .ForMember(vm => vm.ReceiptNo, Dm => Dm.MapFrom(dModel => dModel.CustomId))
            .ForMember(vm => vm.InstrumentNo, Dm => Dm.MapFrom(dModel => dModel.Cheque.InstrumentNo))
            .ForMember(vm => vm.BankName, Dm => Dm.MapFrom(dModel => dModel.Cheque.BankName))
            .ForMember(vm => vm.BranchName, Dm => Dm.MapFrom(dModel => dModel.Cheque.BranchName))
            .ForMember(vm => vm.BranchCity, Dm => Dm.MapFrom(dModel => dModel.Cheque.BankCity))
            .ForMember(vm => vm.IFSCCode, Dm => Dm.MapFrom(dModel => dModel.Cheque.IFSCCode))
            .ForMember(vm => vm.PaymentDate, Dm => Dm.MapFrom(dModel => dModel.CollectionDate))
            .ForMember(vm => vm.CollectorName, Dm => Dm.MapFrom(dModel => dModel.Collector.FirstName + " " + dModel.Collector.LastName));
        }
    }
}