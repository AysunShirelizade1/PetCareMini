using PetCareMini.Application.DTOs.Notification;

namespace PetCareMini.Application.Abstracts.Services;

public interface INotificationService
{
    Task SendAsync(int userId, string title, string message, string type, int? referenceId = null);
    Task<List<NotificationGetDto>> GetUserNotificationsAsync(int userId);
    Task MarkAsReadAsync(int notificationId, int userId);
    Task MarkAllAsReadAsync(int userId);
    Task<int> GetUnreadCountAsync(int userId);
}