using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AreaPinCodeMappingMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AreaPinCodeMappingMapperConfiguration() : base()
        {
            CreateMap<AreaPinCodeMappingDto, AreaPinCodeMapping>();
            CreateMap<AreaPinCodeMapping, AreaPinCodeMappingDto>();
            CreateMap<AreaPinCodeMappingDtoWithId, AreaPinCodeMapping>();
            CreateMap<AreaPinCodeMapping, AreaPinCodeMappingDtoWithId>();
        }
    }
}