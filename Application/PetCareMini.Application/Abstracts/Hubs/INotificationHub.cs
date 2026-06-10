namespace PetCareMini.Application.Abstracts.Hubs;

public interface INotificationHub
{
    Task SendNotificationAsync(int userId, object notification);
}