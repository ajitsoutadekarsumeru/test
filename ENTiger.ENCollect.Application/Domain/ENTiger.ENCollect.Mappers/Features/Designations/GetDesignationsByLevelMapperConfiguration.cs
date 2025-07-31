using Sumeru.Flex;

namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDesignationsByLevelMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetDesignationsByLevelMapperConfiguration() : base()
        {
            CreateMap<Designation, GetDesignationsByLevelDto>();
        }
    }
}