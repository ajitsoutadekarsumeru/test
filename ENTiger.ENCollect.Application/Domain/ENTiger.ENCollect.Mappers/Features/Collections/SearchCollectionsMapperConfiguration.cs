using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchCollectionsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchCollectionsMapperConfiguration() : base()
        {
            CreateMap<Collection, SearchCollectionsDto>()

                   .ForMember(o => o.Account, opt => opt.MapFrom(o => new
                   {
                       o.Account.CustomId,
                       o.Account.PRODUCT,
                       o.Account.ProductGroup
                   }))
                .ForMember(o => o.Cheque, opt => opt.MapFrom(o => new
                {
                    o.Cheque.BankName,
                    o.Cheque.BranchName,
                    o.Cheque.InstrumentDate,
                    o.Cheque.InstrumentNo,
                    o.Cheque.IFSCCode,
                    o.Cheque.BankCity
                }))
               .ForMember(vm => vm.ReceiptNo, opt => opt.MapFrom(dModel => dModel.CustomId))
               .ForMember(vm => vm.AccountNo, Dm => Dm.MapFrom(dModel => dModel.Account.CustomId))
               .ForMember(vm => vm.ProductName, Dm => Dm.MapFrom(dModel => dModel.Account.PRODUCT))
               .ForMember(vm => vm.ProductGroup, Dm => Dm.MapFrom(dModel => dModel.Account.ProductGroup))
               .ForMember(vm => vm.ProductName, Dm => Dm.MapFrom(dModel => dModel.Account.PRODUCT))
               .ForMember(vm => vm.PaymentDate, Dm => Dm.MapFrom(dModel => dModel.CollectionDate))
               .ForMember(vm => vm.PaymentMode, Dm => Dm.MapFrom(dModel => dModel.CollectionMode))
                .ForMember(vm => vm.InstrumentDate, Dm => Dm.MapFrom(dModel => dModel.Cheque.InstrumentDate))
               .ForMember(vm => vm.InstrumentNo, Dm => Dm.MapFrom(dModel => dModel.Cheque.InstrumentNo))
               .ForMember(vm => vm.BankName, Dm => Dm.MapFrom(dModel => dModel.Cheque.BankName))
               .ForMember(vm => vm.IfscCode, Dm => Dm.MapFrom(dModel => dModel.Cheque.IFSCCode))
               .ForMember(vm => vm.BranchName, Dm => Dm.MapFrom(dModel => dModel.Cheque.BranchName))
               .ForMember(vm => vm.Bankcity, Dm => Dm.MapFrom(dModel => dModel.Cheque.BankCity));
        }
    }
}