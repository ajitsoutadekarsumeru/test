using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class PrimaryAllocationFileMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public PrimaryAllocationFileMapperConfiguration() : base()
        {
            CreateMap<PrimaryAllocationFileDto, PrimaryAllocationFile>();
            CreateMap<PrimaryAllocationFile, PrimaryAllocationFileDto>();
            CreateMap<PrimaryAllocationFileDtoWithId, PrimaryAllocationFile>();
            CreateMap<PrimaryAllocationFile, PrimaryAllocationFileDtoWithId>();
        }
    }
}