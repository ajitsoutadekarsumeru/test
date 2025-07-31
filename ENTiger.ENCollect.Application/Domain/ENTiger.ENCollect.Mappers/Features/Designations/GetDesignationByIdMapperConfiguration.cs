using Sumeru.Flex;

namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDesignationByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetDesignationByIdMapperConfiguration() : base()
        {
            CreateMap<Designation, GetDesignationByIdDto>();
        }
    }
}