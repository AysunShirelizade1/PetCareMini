using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Persistence.Contexts;
using PetCareMini.Persistence.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Persistence.Services;

namespace PetCareMini.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IVeterinarianRepository, VeterinarianRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();

        services.AddScoped<IVeterinarianService, VeterinarianService>();
        services.AddScoped<IServiceService, ServiceService>();
        return services;
    }
}