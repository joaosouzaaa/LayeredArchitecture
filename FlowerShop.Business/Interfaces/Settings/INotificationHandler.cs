using FlowerShop.Business.Settings.NotificationSettings;

namespace FlowerShop.Business.Interfaces.Settings;
public interface INotificationHandler
{
    List<Notification> GetNotifications();
    bool HasNotifications();
    void AddNotification(string key, string message);
}
