using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDepositSlipDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetDepositSlipDetailsMapperConfiguration() : base()
        {
            CreateMap<Collection, GetDepositSlipDetailsDto>()
                .ForMember(d => d.Id, s => s.MapFrom(s => s.ReceiptId))
                .ForMember(d => d.ReceiptNo, s => s.MapFrom(s => s.CustomId))
                .ForMember(d => d.ReceiptDate, s => s.MapFrom(s => s.CollectionDate))
                .ForMember(d => d.CustomerAccount, s => s.MapFrom(s => s.Account.CustomId))
                .ForMember(d => d.ModeOfPayment, s => s.MapFrom(s => s.CollectionMode))
                .ForMember(d => d.DraweeBankName, s => s.MapFrom(s => s.Cheque != null ? s.Cheque.BankName : string.Empty))
                .ForMember(d => d.DraweeBranchName, s => s.MapFrom(s => s.Cheque != null ? s.Cheque.BranchName : string.Empty))
                .ForMember(d => d.InstrumentNo, s => s.MapFrom(s => s.Cheque != null ? s.Cheque.InstrumentNo : string.Empty))
                .ForMember(d => d.MICR, s => s.MapFrom(s => s.Cheque != null ? s.Cheque.MICRCode : string.Empty))
                .ForMember(d => d.IFSC, s => s.MapFrom(s => s.Cheque != null ? s.Cheque.IFSCCode : string.Empty));
        }
    }
}