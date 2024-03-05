using FlowerShop.CrossCutting.Settings.NotificationSettings;

namespace FlowerShop.CrossCutting.Interfaces.Settings;
public interface INotificationHandler
{
    List<Notification> GetNotifications();
    bool HasNotifications();
    void AddNotification(string key, string message);
}
