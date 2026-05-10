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
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        #region Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IVeterinarianRepository, VeterinarianRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWishlistRepository, WishlistRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IFaqRepository, FaqRepository>();
        services.AddScoped<IProductReviewRepository, ProductReviewRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IPetRepository, PetRepository>();
        #endregion
        #region Services
        services.AddScoped<IPetService, PetService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<ICouponService, CouponService>();    
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IProductReviewService, ProductReviewService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IFaqService, FaqService>();
        services.AddScoped<IWishlistService, WishlistService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IVeterinarianService, VeterinarianService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IProductCategoryService, ProductCategoryService>();
        services.AddScoped<IProductService, ProductService>();
        #endregion
        return services;
    }
}

