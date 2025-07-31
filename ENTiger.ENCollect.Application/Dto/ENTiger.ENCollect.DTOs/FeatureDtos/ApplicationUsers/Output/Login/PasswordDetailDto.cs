namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public class PasswordDetailDto : DtoBridge
    {
        public string? Email { get; set; }
        public DateTime? PasswordLastUpdated { get; set; }
    }
}