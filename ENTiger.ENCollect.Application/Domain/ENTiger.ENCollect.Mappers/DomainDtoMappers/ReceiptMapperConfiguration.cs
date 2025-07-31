using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ReceiptMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ReceiptMapperConfiguration() : base()
        {
            CreateMap<ReceiptDto, Receipt>();
            CreateMap<Receipt, ReceiptDto>();
            CreateMap<ReceiptDtoWithId, Receipt>();
            CreateMap<Receipt, ReceiptDtoWithId>();
        }
    }
}