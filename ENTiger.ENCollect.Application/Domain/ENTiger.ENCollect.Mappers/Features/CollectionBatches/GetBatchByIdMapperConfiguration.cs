using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetBatchByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetBatchByIdMapperConfiguration() : base()
        {
            CreateMap<CollectionBatch, CollectionBatchOutputAPIModel>()
            .ForMember(vm => vm.BatchCode, Dm => Dm.MapFrom(dModel => dModel.CustomId))
            .ForMember(vm => vm.Amount, Dm => Dm.MapFrom(dModel => dModel.Amount))
            .ForMember(vm => vm.ModeOfPayment, Dm => Dm.MapFrom(dModel => dModel.ModeOfPayment))
            .ForMember(vm => vm.CreatedDate, Dm => Dm.MapFrom(dModel => dModel.CreatedDate))
            //.ForMember(vm => vm.AcknowledgedByName, Dm => Dm.MapFrom(dModel => dModel.AcknowledgedBy.FirstName + " "+dModel.AcknowledgedBy.LastName))
            ;

            CreateMap<Collection, CollectionDetailsOutputApiModel>()
            .ForMember(vm => vm.depositBranchName, Dm => Dm.MapFrom(dModel => dModel.Remarks))
            .ForMember(vm => vm.Amount, Dm => Dm.MapFrom(dModel => dModel.Amount))
            .ForMember(vm => vm.OverdueAmount, Dm => Dm.MapFrom(dModel => dModel.yOverdueAmount))
            .ForMember(vm => vm.OtherCharges, Dm => Dm.MapFrom(dModel => dModel.othercharges))
            .ForMember(vm => vm.TransactionNumber, Dm => Dm.MapFrom(dModel => dModel.TransactionNumber))
            .ForMember(vm => vm.Settlement, Dm => Dm.MapFrom(dModel => dModel.Settlement))
            .ForMember(vm => vm.PenalAmount, Dm => Dm.MapFrom(dModel => dModel.yPenalInterest))
            .ForMember(vm => vm.BounceCharges, Dm => Dm.MapFrom(dModel => dModel.yBounceCharges))
            .ForMember(vm => vm.ReceiptNo, Dm => Dm.MapFrom(dModel => dModel.CustomId))
            .ForMember(vm => vm.receiptDate, Dm => Dm.MapFrom(dModel => dModel.CollectionDate))
            .ForMember(vm => vm.AccountNo, Dm => Dm.MapFrom(dModel => dModel.Account.CustomId))
            .ForMember(vm => vm.PaymentMode, Dm => Dm.MapFrom(dModel => dModel.CollectionMode))

            //.ForMember(vm => vm.InstrumentDate, Dm => Dm.MapFrom(dModel => dModel.Cheque.InstrumentDate))
            //.ForMember(vm => vm.InstrumentNo, Dm => Dm.MapFrom(dModel => dModel.Cheque.InstrumentNo))
            //.ForMember(vm => vm.MircCode, Dm => Dm.MapFrom(dModel => dModel.Cheque.MICRCode))
            //.ForMember(vm => vm.IfSCCode, Dm => Dm.MapFrom(dModel => dModel.Cheque.IFSCCode))
            //.ForMember(vm => vm.DraweeBank, Dm => Dm.MapFrom(dModel => dModel.Cheque.BankName))
            //.ForMember(vm => vm.DraweeBranch, Dm => Dm.MapFrom(dModel => dModel.Cheque.BranchName))
            //.ForMember(vm => vm.InstrumentDate, Dm => Dm.MapFrom(dModel => dModel.Cheque.InstrumentDate))
            ;
        }
    }
}