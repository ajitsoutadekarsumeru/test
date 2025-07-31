namespace ENTiger.ENCollect
{
    public class LockoutSettings
    {
        /// <summary>
        /// Maximum number of failed login attempts before the account is locked
        /// </summary>
        public int Attempts { get; set; } = 3;
        /// <summary>
        /// Duration (in hours) that the account remains locked after exceeding the maximum attempts
        /// </summary>
        public int EndInHours { get; set; } = 24;
    }
}
