using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class PrimaryUnAllocationFileMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public PrimaryUnAllocationFileMapperConfiguration() : base()
        {
            CreateMap<PrimaryUnAllocationFileDto, PrimaryUnAllocationFile>();
            CreateMap<PrimaryUnAllocationFile, PrimaryUnAllocationFileDto>();
            CreateMap<PrimaryUnAllocationFileDtoWithId, PrimaryUnAllocationFile>();
            CreateMap<PrimaryUnAllocationFile, PrimaryUnAllocationFileDtoWithId>();
        }
    }
}