using ENTiger.ENCollect.FeedbackModule;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SearchCollectionBulkUploadStatusMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
         private readonly FileConfigurationSettings _fileConfigurationSettings;
        public SearchCollectionBulkUploadStatusMapperConfiguration() : base()
        {
            _fileConfigurationSettings = (FileConfigurationSettings?)FlexContainer.ServiceProvider.GetRequiredService<IOptions<FileConfigurationSettings>>().Value;
            string FileExtension = _fileConfigurationSettings.DefaultExtension;

            CreateMap<CollectionUploadFile, SearchCollectionBulkUploadStatusDto>()
               .ForMember(o => o.TransactionId, opt => opt.MapFrom(o => o.CustomId))
               .ForMember(cm => cm.DownloadFileName, Dm => Dm.MapFrom(dModel =>
                  dModel.Status == "Uploaded" || dModel.Status == "Invalid File Format"
                  ? dModel.FileName : $"{dModel.CustomId}{FileExtension}"));

        }
    }
}
