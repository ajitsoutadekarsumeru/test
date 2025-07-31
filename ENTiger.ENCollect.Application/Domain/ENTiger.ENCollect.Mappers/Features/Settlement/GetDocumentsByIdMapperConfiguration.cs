using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetDocumentsByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetDocumentsByIdMapperConfiguration() : base()
        {
            CreateMap<List<SettlementDocument>, GetDocumentsByIdDto>()
                    .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src));

            CreateMap<SettlementDocument, DocumentsDto>();
        }
    }
}
