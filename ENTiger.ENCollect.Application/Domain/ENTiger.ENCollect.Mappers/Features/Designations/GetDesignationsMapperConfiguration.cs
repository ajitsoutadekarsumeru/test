using Sumeru.Flex;

namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDesignationsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetDesignationsMapperConfiguration() : base()
        {
            CreateMap<Designation, GetDesignationsDto>()
                .ForMember(o => o.DesignationAcronym, opt => opt.MapFrom(o => o.DesignationTypeId));
        }
    }
}