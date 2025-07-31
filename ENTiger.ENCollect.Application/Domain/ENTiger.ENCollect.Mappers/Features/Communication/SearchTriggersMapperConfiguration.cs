using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SearchTriggersMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public SearchTriggersMapperConfiguration() : base()
        {
            CreateMap<CommunicationTrigger, SearchTriggersDto>()
                .ForMember(vm => vm.TriggerName, dm => dm.MapFrom(dModel => dModel.Name))
                .ForMember(vm => vm.Status, dm => dm.MapFrom(dModel => dModel.IsActive ? "Enabled" : "Disabled"))
                .ForMember(vm => vm.TriggerTypeName, dm => dm.MapFrom(dModel => dModel.TriggerType.Name))
                 .ForMember(vm => vm.ConnectedTemplates, dm => dm.MapFrom(dModel => dModel.TriggerTemplates.Select(s => s.CommunicationTemplateId).ToList()));
        }
    }
}
