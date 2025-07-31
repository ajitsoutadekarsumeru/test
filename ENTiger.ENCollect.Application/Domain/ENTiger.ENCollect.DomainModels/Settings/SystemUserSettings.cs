namespace ENTiger.ENCollect
{
    public class SystemUserSettings
    {
        public string? SystemUserEmailId { get; set; }
        public string? SystemUserId { get; set; }
        public int UserInactivityDormantDays { get; set; } = 1000;
    }
}
