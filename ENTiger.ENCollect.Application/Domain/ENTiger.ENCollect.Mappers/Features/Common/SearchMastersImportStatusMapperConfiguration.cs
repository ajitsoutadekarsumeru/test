using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchMastersImportStatusMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchMastersImportStatusMapperConfiguration() : base()
        {
            CreateMap<MasterFileStatus, SearchMastersImportStatusDto>()
                .ForMember(o => o.TransactionId, opt => opt.MapFrom(o => o.CustomId))
                .ForMember(o => o.FileType, opt => opt.MapFrom(o => o.UploadType))
                .ForMember(o => o.UploadedDate, opt => opt.MapFrom(o => o.FileUploadedDate))
                .ForMember(o => o.ProcessedDate, opt => opt.MapFrom(o => o.FileProcessedDateTime));
        }
    }
}