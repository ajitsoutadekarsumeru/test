using Sumeru.Flex;

namespace ENTiger.ENCollect.HierarchyModule;

public partial class GetMastersByIdMapperConfiguration : FlexMapperProfile
{
    public GetMastersByIdMapperConfiguration() : base()
    {
        CreateMap<HierarchyMaster, GetMastersByIdDto>()
            .ForMember(o => o.Name, opt => opt.MapFrom(o => o.Item))
            .ForMember(o => o.ParentId, opt => opt.MapFrom(o => o.Parent.Id))
            .ForMember(o => o.ParentName, opt => opt.MapFrom(o => o.Parent.Item));
    }
}
