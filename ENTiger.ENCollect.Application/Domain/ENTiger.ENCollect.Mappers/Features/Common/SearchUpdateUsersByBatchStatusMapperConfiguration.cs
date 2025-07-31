using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchUpdateUsersByBatchStatusMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchUpdateUsersByBatchStatusMapperConfiguration() : base()
        {
            CreateMap<UsersUpdateFile, SearchUpdateUsersByBatchStatusDto>()
                .ForMember(o => o.TransactionId, opt => opt.MapFrom(o => o.CustomId))
                .ForMember(o => o.DownloadFileName, opt => opt.MapFrom(o => 
                    (o.Status.ToLower() == FileStatusEnum.Uploaded.Value.ToLower() || 
                     o.Status.ToLower() == FileStatusEnum.InvalidFileFormat.Value) ? o.FileName : $"{o.CustomId}.csv"));
        }
    }
}