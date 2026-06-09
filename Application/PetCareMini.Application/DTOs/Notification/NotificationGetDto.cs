namespace PetCareMini.Application.DTOs.Notification;

public class NotificationGetDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public int? ReferenceId { get; set; }
    public DateTime CreatedAt { get; set; }
}