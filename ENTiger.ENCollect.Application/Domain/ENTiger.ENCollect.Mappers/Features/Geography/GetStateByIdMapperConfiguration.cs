using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetStateByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetStateByIdMapperConfiguration() : base()
        {
            CreateMap<State, GetStateByIdDto>();
        }
    }
}