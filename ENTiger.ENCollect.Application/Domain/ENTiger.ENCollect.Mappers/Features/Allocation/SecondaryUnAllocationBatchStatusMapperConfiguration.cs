using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SecondaryUnAllocationBatchStatusMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        private readonly FileConfigurationSettings _fileConfigurationSettings;
        public SecondaryUnAllocationBatchStatusMapperConfiguration() : base()
        {
            _fileConfigurationSettings = (FileConfigurationSettings?)FlexContainer.ServiceProvider.GetRequiredService<IOptions<FileConfigurationSettings>>().Value;
            string FileExtension = _fileConfigurationSettings.DefaultExtension;

            CreateMap<SecondaryUnAllocationFile, SecondaryUnAllocationBatchStatusDto>()
                .ForMember(o => o.TransactionId, opt => opt.MapFrom(o => o.CustomId))
                .ForMember(cm => cm.DownloadFileName, Dm => Dm.MapFrom(dModel =>
                   dModel.Status == "Uploaded" || dModel.Status == "Invalid File Format"
                   ? dModel.FileName : $"{dModel.CustomId}{FileExtension}"))
                .ForMember(o => o.UnAllocationType, opt => opt.MapFrom(o => o.Description));

        }
    }
}