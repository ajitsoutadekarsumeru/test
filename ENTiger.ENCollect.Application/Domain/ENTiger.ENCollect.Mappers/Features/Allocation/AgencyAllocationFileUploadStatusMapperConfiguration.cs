using ENTiger.ENCollect.AllocationModule;
using ENTiger.ENCollect;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using Microsoft.Extensions.DependencyInjection;

public partial class AgencyAllocationFileUploadStatusMapperConfiguration : FlexMapperProfile
{
    /// <summary>
    /// File configuration settings
    /// </summary>
    private readonly FileConfigurationSettings _fileConfigurationSettings;
    private string DefaultExtension = string.Empty;

    // Constructor with IOptions dependency injection
    public AgencyAllocationFileUploadStatusMapperConfiguration() : base()
    {
        _fileConfigurationSettings = (FileConfigurationSettings?)FlexContainer.ServiceProvider.GetRequiredService<IOptions<FileConfigurationSettings>>().Value;
        DefaultExtension = _fileConfigurationSettings.DefaultExtension;

        // Initialize the mappings
        CreateMap<PrimaryAllocationFile, AgencyAllocationFileUploadStatusDto>()
            .ForMember(cm => cm.TransactionId, Dm => Dm.MapFrom(dModel => dModel.CustomId))
            .ForMember(cm => cm.FileName, Dm => Dm.MapFrom(dModel => dModel.FileName))
            .ForMember(cm => cm.Status, Dm => Dm.MapFrom(dModel => dModel.Status))
            .ForMember(cm => cm.AllocationMethod, Dm => Dm.MapFrom(dModel => dModel.Description))
            .ForMember(cm => cm.DownloadFileName, Dm => Dm.MapFrom(dModel =>
                dModel.Status == FileStatusEnum.Uploaded.Value 
                                        || dModel.Status == FileStatusEnum.InvalidFileFormat.Value 
                                        || dModel.Status == FileStatusEnum.Error.Value
                                ? dModel.FileName 
                                : $"{dModel.CustomId}{DefaultExtension}"));
    }
}
