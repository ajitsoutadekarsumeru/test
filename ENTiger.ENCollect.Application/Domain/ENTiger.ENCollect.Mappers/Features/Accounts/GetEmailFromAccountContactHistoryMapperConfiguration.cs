using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetEmailFromAccountContactHistoryMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetEmailFromAccountContactHistoryMapperConfiguration() : base()
        {
            CreateMap<AccountContactHistory, GetEmailFromAccountContactHistoryDto>()
                 .ForMember(vm => vm.Email, dm => dm.MapFrom(dModel => dModel.ContactValue))
                .ForMember(vm => vm.LastModifiedDate, dm => dm.MapFrom(dModel => dModel.LastModifiedDate.DateTime));

        }
    }
}
