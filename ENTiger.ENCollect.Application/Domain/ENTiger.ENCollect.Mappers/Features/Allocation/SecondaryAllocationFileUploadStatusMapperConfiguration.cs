using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SecondaryAllocationFileUploadStatusMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        private readonly FileConfigurationSettings _fileConfigurationSettings;
        public SecondaryAllocationFileUploadStatusMapperConfiguration() : base()
        {
            _fileConfigurationSettings = (FileConfigurationSettings?)FlexContainer.ServiceProvider.GetRequiredService<IOptions<FileConfigurationSettings>>().Value;
            string FileExtension = _fileConfigurationSettings.DefaultExtension;

            CreateMap<SecondaryAllocationFile, SecondaryAllocationFileUploadStatusDto>()
                .ForMember(cm => cm.TransactionId, Dm => Dm.MapFrom(dModel => dModel.CustomId))
                 .ForMember(cm => cm.FileName, Dm => Dm.MapFrom(dModel => dModel.FileName))
                 .ForMember(cm => cm.Status, Dm => Dm.MapFrom(dModel => dModel.Status))
                 .ForMember(cm => cm.DownloadFileName, Dm => Dm.MapFrom(dModel =>
                   dModel.Status == "Uploaded" || dModel.Status == "Invalid File Format"
                   ? dModel.FileName : $"{dModel.CustomId}{FileExtension}"))
                 .ForMember(cm => cm.AllocationMethod, Dm => Dm.MapFrom(dModel => dModel.Description));
 

        }
    }
}