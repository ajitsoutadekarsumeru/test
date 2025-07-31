namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the settings for concurrency limits.
    /// </summary>
    public class ConcurrencySettings
    {
        /// <summary>
        /// Gets or sets the maximum concurrency level.
        /// </summary>
        public string Maximum { get; set; } = "";

        /// <summary>
        /// Gets or sets the minimum concurrency level.
        /// </summary>
        public string Minimum { get; set; } = "";
    }
}