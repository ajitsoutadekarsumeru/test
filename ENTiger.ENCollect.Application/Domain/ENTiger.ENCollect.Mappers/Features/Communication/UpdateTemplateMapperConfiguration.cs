using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateTemplateMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UpdateTemplateMapperConfiguration() : base()
        {
            CreateMap<UpdateTemplateDto, CommunicationTemplate>()
            .ForMember(vm => vm.CommunicationTemplateDetails, dm => dm.MapFrom(dModel => dModel.CommunicationTemplateDetails));

            CreateMap<UpdateCommunicationTemplateDetailDto, CommunicationTemplateDetail>()
            .ForMember(vm => vm.Body, dm => dm.MapFrom(dModel => dModel.Body.Replace("\n", "<br/>")));
        }
    }
}