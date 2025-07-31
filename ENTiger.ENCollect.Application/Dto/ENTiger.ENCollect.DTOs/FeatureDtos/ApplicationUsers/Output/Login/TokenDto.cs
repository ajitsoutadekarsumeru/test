namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public class TokenDto : DtoBridge
    {
        public string? token_type { get; set; }
        public string? access_token { get; set; }        
        public int? expires_in { get; set; }
        public string? message { get; set; }
    }

    public class AuthToken : TokenDto
    {
        public DateTime? password_last_updated { get; set; }
    }
}