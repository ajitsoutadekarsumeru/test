using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddPhysicalCollectionMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AddPhysicalCollectionMapperConfiguration() : base()
        {
            CreateMap<AddPhysicalCollectionDto, Collection>()
           .ForMember(cm => cm.CollectionDate, Dm => Dm.MapFrom(dModel => dModel.PhysicalReceiptDate));

            CreateMap<CashAPIModel, Cash>();

            CreateMap<ChequeAPIModel, Cheque>();
            ;
        }
    }
}