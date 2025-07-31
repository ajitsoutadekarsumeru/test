using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchCreateUsersByBatchStatusMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchCreateUsersByBatchStatusMapperConfiguration() : base()
        {
            CreateMap<UsersCreateFile, SearchCreateUsersByBatchStatusDto>()
                .ForMember(o => o.TransactionId, opt => opt.MapFrom(o => o.CustomId))
                .ForMember(o => o.DownloadFileName, opt => opt.MapFrom(o =>
                    (o.Status == FileStatusEnum.Uploaded.Value || o.Status == FileStatusEnum.InvalidFileFormat.Value)
                        ? o.FileName
                        : $"{o.CustomId}.csv"));
        }

    }
}