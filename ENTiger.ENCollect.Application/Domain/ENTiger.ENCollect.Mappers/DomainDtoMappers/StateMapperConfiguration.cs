using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class StateMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public StateMapperConfiguration() : base()
        {
            CreateMap<StateDto, State>();
            CreateMap<State, StateDto>();
            CreateMap<StateDtoWithId, State>();
            CreateMap<State, StateDtoWithId>();
        }
    }
}