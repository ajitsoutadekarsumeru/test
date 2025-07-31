using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetAddressFromAccountContactHistoryMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetAddressFromAccountContactHistoryMapperConfiguration() : base()
        {
            CreateMap<AccountContactHistory, GetAddressFromAccountContactHistoryDto>()
             .ForMember(vm => vm.Address, dm => dm.MapFrom(dModel => dModel.ContactValue))
                .ForMember(vm => vm.LastModifiedDate, dm => dm.MapFrom(dModel => dModel.LastModifiedDate.DateTime));
        }
    }
}
