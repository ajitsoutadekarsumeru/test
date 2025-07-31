namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the authentication settings for the application.
    /// </summary>
    public class AuthSettings
    {
        /// <summary>
        /// Gets or sets the URL used for authentication.
        /// </summary>
        public string AuthUrl { get; set; } = "";

        /// <summary>
        /// Gets or sets the base URL for the application.
        /// </summary>
        public string BaseUrl { get; set; } = "";

        /// <summary>
        /// Gets or sets the password for the application emil authentication.
        /// </summary>
        public string Password { get; set; } = "";

        /// <summary>
        /// Gets or sets the confirmation password for the application emil authentication
        /// </summary>
        public string ConfirmPassword { get; set; } = "";
    }
}