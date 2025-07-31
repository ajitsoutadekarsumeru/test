using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCollectionsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetCollectionsMapperConfiguration() : base()
        {
            CreateMap<Collection, GetCollectionsDto>()
                .ForMember(vm => vm.ReceiptNo, Dm => Dm.MapFrom(dModel => dModel.CustomId))
                .ForMember(vm => vm.ReceiptNo, Dm => Dm.MapFrom(dModel => dModel.CustomId))
                .ForMember(vm => vm.ReceiptDate, DM => DM.MapFrom(dmodel => dmodel.CollectionDate))
                .ForMember(vm => vm.CollectorCustomId, DM => DM.MapFrom(dmodel => dmodel.Collector.CustomId))
                .ForMember(vm => vm.CustomerName, DM => DM.MapFrom(dmodel => dmodel.Account.CUSTOMERNAME))
                .ForMember(vm => vm.CustomerAccountNo, DM => DM.MapFrom(dmodel => dmodel.Account.CustomId))
                .ForMember(vm => vm.MobileNo, DM => DM.MapFrom(dmodel => dmodel.MobileNo))
                .ForMember(vm => vm.EmailId, DM => DM.MapFrom(dmodel => dmodel.EMailId))
                .ForMember(vm => vm.CollectorName, Dm => Dm.MapFrom(dModel => dModel.Collector.FirstName + "" + dModel.Collector.LastName))
                .ForMember(vm => vm.Total, DM => DM.MapFrom(dmodel => dmodel.Amount))
                .ForMember(vm => vm.CustomerName, DM => DM.MapFrom(dmodel => dmodel.Account.CUSTOMERNAME))
                .ForMember(vm => vm.Product, DM => DM.MapFrom(dmodel => dmodel.Account.PRODUCT))
            ;
        }
    }
}