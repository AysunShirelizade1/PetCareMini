namespace PetCareMini.Application.Abstracts.Services;

public interface IEmailService
{
    Task SendAsync(string toEmail, string subject, string body);
}