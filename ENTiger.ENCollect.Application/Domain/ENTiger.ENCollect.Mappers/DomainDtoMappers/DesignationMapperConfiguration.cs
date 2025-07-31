using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DesignationMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public DesignationMapperConfiguration() : base()
        {
            CreateMap<DesignationDto, Designation>();
            CreateMap<Designation, DesignationDto>();
            CreateMap<DesignationDtoWithId, Designation>();
            CreateMap<Designation, DesignationDtoWithId>();

            CreateMap<UserDesignationDto, Designation>();
            CreateMap<Designation, UserDesignationDto>();
        }
    }
}