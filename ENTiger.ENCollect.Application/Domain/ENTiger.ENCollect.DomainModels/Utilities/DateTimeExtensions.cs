namespace ENTiger.ENCollect
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts a local DateTime to a UTC DateTimeOffset (offset zero).
        /// </summary>
        /// <param name="localTime">Local DateTime (Kind = Local or Unspecified, interpreted as local)</param>
        /// <returns>A DateTimeOffset in UTC time (offset = +00:00)</returns>
        public static DateTimeOffset ToUtc(this DateTime localTime)
        {
            // Convert local time to UTC DateTime
            var utcTime = localTime.ToUniversalTime();

            // Wrap that UTC DateTime in a DateTimeOffset with offset 0
            return new DateTimeOffset(utcTime, TimeSpan.Zero);
        }

        public static DateTimeOffset ToDateOnlyOffset(this DateTime localTime)
        {
            return new DateTimeOffset(localTime.Year, localTime.Month, localTime.Day, 0, 0, 0, TimeSpan.Zero);            
        }
    }
}
