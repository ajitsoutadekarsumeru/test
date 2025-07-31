using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class EnableTreatmentsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public EnableTreatmentsMapperConfiguration() : base()
        {
            CreateMap<EnableTreatmentsDto, Treatment>();
        }
    }
}