using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class GetIdentificationTypeMapperConfiguration : FlexMapperProfile
    {
        public GetIdentificationTypeMapperConfiguration() : base()
        {
            CreateMap<TFlexIdentificationType, GetIdentificationTypesDto>()
            .ForMember(o => o.IdentificationDocTypes, opt => opt.MapFrom(o => o.IdentificationDocTypes))
            .ForMember(o => o.IdentificationType, opt => opt.MapFrom(o => o.Description));
            ;

            CreateMap<TFlexIdentificationDocType, IdentificationDoctypeOutputModel>()
            .ForMember(vm => vm.IdentificationDoc, Dm => Dm.MapFrom(dModel => dModel.Description)); ;
        }
    }
}