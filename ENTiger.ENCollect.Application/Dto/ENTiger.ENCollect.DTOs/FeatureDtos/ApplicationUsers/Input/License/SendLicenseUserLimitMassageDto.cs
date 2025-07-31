
namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class SendLicenseUserLimitMassageDto : DtoBridge
    {
        public string UserName { get; set; }
        public string UserType { get; set; }
        public List<GetUserTypeDetailsDto> UserTypeDetails { get; set; }
    }
}
