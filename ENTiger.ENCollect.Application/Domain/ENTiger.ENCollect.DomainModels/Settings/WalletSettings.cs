namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the wallet utilization and limit settings.
    /// </summary>
    public class WalletSettings
    {

        /// <summary>
        /// Gets or sets the threshold for low wallet utilization (Green).
        /// Utilization below this percentage is considered low.
        /// Default: 40%.
        /// </summary>
        public int GreenThreshold { get; set; } = 40;

        /// <summary>
        /// Gets or sets the threshold for medium wallet utilization (Amber).
        /// Utilization between the GreenThreshold and this value is considered medium.
        /// Default: 60%.
        /// </summary>
        public int AmberThreshold { get; set; } = 60;

        /// <summary>
        /// Gets or sets the threshold for high wallet utilization (Red).
        /// Utilization above the AmberThreshold up to this value is considered high.
        /// Default: 100%.
        /// </summary>
        public int RedThreshold { get; set; } = 100;

    }

}
