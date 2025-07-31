using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetAvailableTemplatesForAccountByTypeMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetAvailableTemplatesForAccountByTypeMapperConfiguration() : base()
        {
            CreateMap<CommunicationTemplate, GetAvailableTemplatesForAccountByTypeDto>()
                .ForMember(vm => vm.TemplateName, dm => dm.MapFrom(dModel => dModel.Name));

        }
    }
}
