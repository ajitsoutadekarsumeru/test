using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetMobileFromAccountContactHistoryMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetMobileFromAccountContactHistoryMapperConfiguration() : base()
        {
            CreateMap<AccountContactHistory, GetMobileFromAccountContactHistoryDto>()
                .ForMember(vm => vm.MobileNo, dm => dm.MapFrom(dModel => dModel.ContactValue))
                .ForMember(vm => vm.LastModifiedDate, dm => dm.MapFrom(dModel => dModel.LastModifiedDate.DateTime));
        }
    }
}
