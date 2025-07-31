using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect;

//public partial class UserNotificationToken : PersistenceModelBridge
//{
//    protected readonly ILogger<UserNotificationToken> _logger;

//    public UserNotificationToken()
//    {
//    }

//    public UserNotificationToken(ILogger<UserNotificationToken> logger)
//    {
//        _logger = logger;
//    }
    
//    /// <summary>
//    /// User identifier (foreign key to your Users table).
//    /// </summary>
//    [StringLength(32)]
//    public string UserId { get; set; }
//    public ApplicationUser User { get; set; }

//    /// <summary>
//    /// Push notification token.
//    /// </summary>
//    [StringLength(255)]
//    public string DeviceToken { get; set; }

//    /// <summary>
//    /// Platform of the device (e.g., Web, Android, iOS).
//    /// </summary>
//    public NotificationPlatform Platform { get; set; }

//    /// <summary>
//    /// Optional: Device identifier or description.
//    /// </summary>
//    [StringLength(255)]
//    public string? DeviceInfo { get; set; }

//    /// <summary>
//    /// Optional: Is the token currently active or revoked?
//    /// </summary>
//    public bool IsActive { get; set; } = true;
//}
