using Sumeru.Flex;

namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDesignationTypesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetDesignationTypesMapperConfiguration() : base()
        {
            CreateMap<DesignationType, GetDesignationTypesDto>();
        }
    }
}