namespace ENTiger.ENCollect;

public interface IPushNotificationProvider
{
    Task<bool> SendNotificationAsync(NotificationRequest request);
}
