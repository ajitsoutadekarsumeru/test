using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetTriggerByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetTriggerByIdMapperConfiguration() : base()
        {
            CreateMap<CommunicationTrigger, GetTriggerByIdDto>()
                .ForMember(vm => vm.TriggerTypeId, dm => dm.MapFrom(dModel => dModel.TriggerTypeId))
                .ForMember(vm => vm.ConnectedTemplates, dm => dm.MapFrom(dModel => dModel.TriggerTemplates.Select(s => s.CommunicationTemplateId).ToArray()));
        }
    }
}
