using Sumeru.Flex;

namespace ENTiger.ENCollect.HierarchyModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetMastersByParentIdsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetMastersByParentIdsMapperConfiguration() : base()
        {
            CreateMap<HierarchyMaster, GetMastersByParentIdsDto>()
                .ForMember(s => s.Name, opt => opt.MapFrom(d => d.Item))
                .ForMember(s => s.ParentId, opt => opt.MapFrom(d => d.Parent.Id))
                .ForMember(s => s.ParentName, opt => opt.MapFrom(d => d.Parent.Item));
        }
    }
}
