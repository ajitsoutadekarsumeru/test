using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ChequeMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ChequeMapperConfiguration() : base()
        {
            CreateMap<ChequeDto, Cheque>();
            CreateMap<Cheque, ChequeDto>();
            CreateMap<ChequeDtoWithId, Cheque>();
            CreateMap<Cheque, ChequeDtoWithId>();
        }
    }
}