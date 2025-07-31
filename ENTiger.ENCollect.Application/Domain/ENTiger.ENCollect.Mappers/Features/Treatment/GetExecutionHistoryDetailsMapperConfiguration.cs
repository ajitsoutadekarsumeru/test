using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetExecutionHistoryDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetExecutionHistoryDetailsMapperConfiguration() : base()
        {
            CreateMap<TreatmentHistory, GetExecutionHistoryDetailsDto>();
        }
    }
}