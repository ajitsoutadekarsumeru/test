using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class SearchTemplatesMapperConfiguration : FlexMapperProfile
    {
        public SearchTemplatesMapperConfiguration() : base()
        {
            CreateMap<CommunicationTemplate, SearchTemplatesDto>()
            .ForMember(vm => vm.TemplateName, dm => dm.MapFrom(dModel => dModel.Name))
            .ForMember(vm => vm.Channel, dm => dm.MapFrom(dModel => dModel.TemplateType))
            .ForMember(vm => vm.Languages, dm => dm.MapFrom(dModel => dModel.CommunicationTemplateDetails.Select(s => s.Language).ToArray()))
            .ForMember(vm => vm.Status, dm => dm.MapFrom(dModel => dModel.IsActive ? "Enabled" : "Disabled"))
            .ForMember(vm => vm.ConnectedTriggers, dm => dm.MapFrom(dModel => dModel.TemplateTriggers.Select(s => s.CommunicationTrigger.Id).ToList()));
        }
    }
}