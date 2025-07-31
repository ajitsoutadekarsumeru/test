using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAreaPinCodesByAreaIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetAreaPinCodesByAreaIdMapperConfiguration() : base()
        {
            CreateMap<AreaPinCodeMapping, GetAreaPinCodesByAreaIdDto>()
                .ForMember(x => x.pinCode, x => x.MapFrom(o => o.PinCode.Value))
                .ForMember(x => x.AreaName, x => x.MapFrom(o => o.Area.Name))
                .ForMember(x => x.CreatedDate, x => x.MapFrom(o => o.CreatedDate.DateTime))
                .ForMember(x => x.LastModifiedDate, x => x.MapFrom(o => o.LastModifiedDate.DateTime));
        }
    }
}