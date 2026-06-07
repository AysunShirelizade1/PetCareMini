using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.Shared.Settings;
using PetCareMini.Infrastructure.Services;

namespace PetCareMini.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();

        return services;
    }
}