using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddressMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AddressMapperConfiguration() : base()
        {
            CreateMap<AddressDto, Address>();
            CreateMap<Address, AddressDto>();
            CreateMap<AddressDtoWithId, Address>();
            CreateMap<Address, AddressDtoWithId>();
        }
    }
}