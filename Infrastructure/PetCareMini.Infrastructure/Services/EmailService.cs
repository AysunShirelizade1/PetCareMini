using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.Shared.Settings;

namespace PetCareMini.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> options)
    {
        _settings = options.Value;
    }

    public async Task SendEmailAsync(IEnumerable<string> toEmails, string subject, string body)
    {
        using var smtp = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort)
        {
            Credentials = new NetworkCredential(_settings.SenderEmail, _settings.Password),
            EnableSsl = true
        };

        var message = new MailMessage
        {
            From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        foreach (var email in toEmails.Distinct())
            message.To.Add(email.Trim());

        await smtp.SendMailAsync(message);
    }
}