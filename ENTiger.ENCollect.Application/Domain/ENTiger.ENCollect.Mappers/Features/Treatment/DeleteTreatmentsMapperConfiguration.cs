using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DeleteTreatmentsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public DeleteTreatmentsMapperConfiguration() : base()
        {
            CreateMap<DeleteTreatmentsDto, Treatment>();
        }
    }
}