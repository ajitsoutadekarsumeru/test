using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddCollectionMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AddCollectionMapperConfiguration() : base()
        {
            CreateMap<AddCollectionDto, Collection>();
            CreateMap<CashAPIModel, Cash>();

            CreateMap<ChequeAPIModel, Cheque>();
        }
    }
}