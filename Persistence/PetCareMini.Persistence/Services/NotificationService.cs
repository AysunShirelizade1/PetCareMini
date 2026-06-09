using PetCareMini.Application.Abstracts.Hubs;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Notification;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _repository;
    private readonly INotificationHub _hub;

    public NotificationService(INotificationRepository repository, INotificationHub hub)
    {
        _repository = repository;
        _hub = hub;
    }

    public async Task SendAsync(int userId, string title, string message, string type, int? referenceId = null)
    {
        var notification = new Notification
        {
            UserId = userId,
            Title = title,
            Message = message,
            Type = type,
            ReferenceId = referenceId
        };

        await _repository.AddAsync(notification);
        await _repository.SaveChangesAsync();

        await _hub.SendNotificationAsync(userId, new
        {
            notification.Id,
            notification.Title,
            notification.Message,
            notification.Type,
            notification.IsRead,
            notification.ReferenceId,
            notification.CreatedAt
        });
    }

    public async Task<List<NotificationGetDto>> GetUserNotificationsAsync(int userId)
    {
        var notifications = await _repository.GetByUserIdAsync(userId);
        return notifications.Select(n => new NotificationGetDto
        {
            Id = n.Id,
            Title = n.Title,
            Message = n.Message,
            Type = n.Type,
            IsRead = n.IsRead,
            ReferenceId = n.ReferenceId,
            CreatedAt = n.CreatedAt
        }).ToList();
    }

    public async Task MarkAsReadAsync(int notificationId, int userId)
    {
        var notification = await _repository.GetByIdAndUserIdAsync(notificationId, userId)
            ?? throw new KeyNotFoundException("Bildiriş tapılmadı.");

        notification.IsRead = true;
        await _repository.SaveChangesAsync();
    }

    public async Task MarkAllAsReadAsync(int userId)
    {
        var notifications = await _repository.GetUnreadByUserIdAsync(userId);
        notifications.ForEach(n => n.IsRead = true);
        await _repository.SaveChangesAsync();
    }

    public async Task<int> GetUnreadCountAsync(int userId)
        => await _repository.GetUnreadCountAsync(userId);
}