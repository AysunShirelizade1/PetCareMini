using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Infrastructure.Services;
using PetCareMini.Infrastructure.Settings;

namespace PetCareMini.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddScoped<IEmailService, EmailService>();
        return services;
    }
}