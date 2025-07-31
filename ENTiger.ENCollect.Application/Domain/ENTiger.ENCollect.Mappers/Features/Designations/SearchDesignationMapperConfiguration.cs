using Sumeru.Flex;

namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchDesignationMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchDesignationMapperConfiguration() : base()
        {
            CreateMap<Designation, SearchDesignationDto>();
        }
    }
}