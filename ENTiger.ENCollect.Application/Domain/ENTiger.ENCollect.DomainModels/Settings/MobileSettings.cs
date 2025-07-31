namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the settings related to the mobile application, including version and app URL.
    /// </summary>
    public class MobileSettings
    {
        /// <summary>
        /// Gets or sets the version of the mobile application.
        /// </summary>
        public string AppVersion { get; set; } = "";

        /// <summary>
        /// Gets or sets the URL for the mobile application.
        /// </summary>
        public string AppUrl { get; set; } = "";

        /// <summary>
        /// Settings related to device validation.
        /// </summary>
        public DeviceValidationSettings DeviceValidation { get; set; }= new DeviceValidationSettings();
    }
}