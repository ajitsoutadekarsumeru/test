namespace ENTiger.ENCollect;

/// <summary>
/// Represents the settings for Google services, including CAPTCHA.
/// </summary>
public class GoogleSettings
{
    /// <summary>
    /// Gets or sets the CAPTCHA settings for Google services.
    /// </summary>
    public CaptchaSettings Captcha { get; set; } = new CaptchaSettings();

    /// <summary>
    /// Gets or sets the Distance Matrix API settings for Google services.
    /// </summary>
    public DistanceMatrixSettings DistanceMatrix { get; set; } = new DistanceMatrixSettings();

    public string EncryptedAPIKey { get; set; } = "";

    public GoogleDistanceSettings Distance { get; set; } = new GoogleDistanceSettings();

    public FireBaseSettings FireBase { get; set; } = new FireBaseSettings();
}

public class FireBaseSettings
{
    public string CredentialJsonPath { get; set; }
    public string Url { get; set; }
    public string ScopeUrl { get; set; }
}