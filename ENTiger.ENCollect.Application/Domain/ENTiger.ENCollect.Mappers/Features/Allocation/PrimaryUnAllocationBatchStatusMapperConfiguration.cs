using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class PrimaryUnAllocationBatchStatusMapperConfiguration : FlexMapperProfile
    {
        private readonly FileConfigurationSettings _fileConfigurationSettings;
        public PrimaryUnAllocationBatchStatusMapperConfiguration() : base()
        {
            _fileConfigurationSettings = (FileConfigurationSettings?)FlexContainer.ServiceProvider.GetRequiredService<IOptions<FileConfigurationSettings>>().Value;
            string FileExtension = _fileConfigurationSettings.DefaultExtension;

            CreateMap<PrimaryUnAllocationFile, PrimaryUnAllocationBatchStatusDto>()
                 .ForMember(o => o.TransactionId, opt => opt.MapFrom(o => o.CustomId))
                 .ForMember(cm => cm.DownloadFileName, Dm => Dm.MapFrom(dModel =>
                   dModel.Status == FileStatusEnum.Uploaded.Value 
                                        || dModel.Status == FileStatusEnum.InvalidFileFormat.Value
                                        || dModel.Status == FileStatusEnum.Error.Value
                                    ? dModel.FileName 
                                    : $"{dModel.CustomId}{FileExtension}"))
                 .ForMember(o => o.UnAllocationType, opt => opt.MapFrom(o => o.Description));
        }
    }
}