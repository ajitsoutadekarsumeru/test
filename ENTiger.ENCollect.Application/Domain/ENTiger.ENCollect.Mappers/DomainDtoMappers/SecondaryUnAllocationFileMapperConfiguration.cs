using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SecondaryUnAllocationFileMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SecondaryUnAllocationFileMapperConfiguration() : base()
        {
            CreateMap<SecondaryUnAllocationFileDto, SecondaryUnAllocationFile>();
            CreateMap<SecondaryUnAllocationFile, SecondaryUnAllocationFileDto>();
            CreateMap<SecondaryUnAllocationFileDtoWithId, SecondaryUnAllocationFile>();
            CreateMap<SecondaryUnAllocationFile, SecondaryUnAllocationFileDtoWithId>();
        }
    }
}