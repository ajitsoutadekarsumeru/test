using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetBatchCollectionsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetBatchCollectionsMapperConfiguration() : base()
        {
            CreateMap<Collection, GetBatchCollectionsDto>()
            .ForMember(vm => vm.AccountNumber, Dm => Dm.MapFrom(dModel => dModel.Account.AGREEMENTID))
            .ForMember(vm => vm.ReceiptNumber, Dm => Dm.MapFrom(dModel => dModel.CustomId))
            .ForMember(vm => vm.ReceiptAmount, Dm => Dm.MapFrom(dModel => dModel.Amount))
            .ForMember(vm => vm.ReceiptDate, Dm => Dm.MapFrom(dModel => dModel.CreatedDate))
            .ForMember(vm => vm.CustomerName, Dm => Dm.MapFrom(dModel => dModel.CustomerName))

                ;
        }
    }
}