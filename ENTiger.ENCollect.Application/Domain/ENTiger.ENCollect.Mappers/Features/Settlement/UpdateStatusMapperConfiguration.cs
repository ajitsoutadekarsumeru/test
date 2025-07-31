using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateStatusMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public UpdateStatusMapperConfiguration() : base()
        {
            CreateMap<UpdateStatusDto, Settlement>();

        }
    }
}
