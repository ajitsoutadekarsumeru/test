using Sumeru.Flex;

namespace ENTiger.ENCollect.DispositionModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCodesByGroupIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetCodesByGroupIdMapperConfiguration() : base()
        {
            CreateMap<DispositionCodeMaster, GetCodesByGroupIdDto>();
        }
    }
}