using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDuePDCDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetDuePDCDetailsMapperConfiguration()
        {
            CreateMap<Collection, GetDuePDCDetailsDto>()
                .ForMember(o => o.ReceiptNo, opt => opt.MapFrom(o => o.CustomId))
                .ForMember(o => o.InstrumentDate, opt => opt.MapFrom(o => o.Cheque.InstrumentDate))
                .ForMember(o => o.CustomerAccountNo, opt => opt.MapFrom(o => o.Account.CustomId))
                .ForMember(o => o.PaymentStatus, opt => opt.MapFrom(p =>
                   //(p.CollectionBatch != null) ?
                   //     ((p.CollectionBatch.PayInSlip != null) ?
                   //         p.CollectionBatch.PayInSlip.PayInSlipWorkflowState.Name :
                   //         p.CollectionWorkflowState.Name) :
                   //     p.CollectionWorkflowState.Name));

                   (p.CollectionBatch != null && p.CollectionBatch.PayInSlip != null) ?
                        p.CollectionWorkflowState.Name :
                        p.CollectionWorkflowState.Name));
        }
    }
}