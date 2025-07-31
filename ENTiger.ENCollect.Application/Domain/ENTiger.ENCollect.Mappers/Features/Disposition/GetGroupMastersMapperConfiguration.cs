using Sumeru.Flex;

namespace ENTiger.ENCollect.DispositionModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetGroupMastersMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetGroupMastersMapperConfiguration() : base()
        {
            CreateMap<DispositionGroupMaster, GetGroupMastersDto>();
        }
    }
}