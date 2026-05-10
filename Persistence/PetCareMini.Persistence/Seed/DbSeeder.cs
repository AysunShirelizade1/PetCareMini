using Microsoft.EntityFrameworkCore;
using PetCareMini.Domain.Entities;
using PetCareMini.Domain.Enums;
using PetCareMini.Persistence.Contexts;
using PetCareMini.Persistence.Helpers;

namespace PetCareMini.Persistence.Seed;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await SeedCategoriesAsync(context);
        await SeedProductsAsync(context);
        await SeedServicesAsync(context);
        await SeedVeterinariansAsync(context);
        await SeedFaqsAsync(context);
        await SeedUsersAsync(context);
        await SeedCouponsAsync(context);
    }

    // ── CATEGORIES ──────────────────────────────────────────────
    private static async Task SeedCategoriesAsync(AppDbContext context)
    {
        if (await context.ProductCategories.AnyAsync()) return;

        await context.ProductCategories.AddRangeAsync(new List<ProductCategory>
        {
            new() { NameAz = "Qida",        NameEn = "Food",        DescriptionAz = "Pet qidaları",          DescriptionEn = "Pet food products"    },
            new() { NameAz = "Aksesuarlar", NameEn = "Accessories", DescriptionAz = "Pet aksesuarları",      DescriptionEn = "Pet accessories"       },
            new() { NameAz = "Oyuncaqlar",  NameEn = "Toys",        DescriptionAz = "Pet oyuncaqları",       DescriptionEn = "Pet toys"              },
            new() { NameAz = "Sağlamlıq",   NameEn = "Health",      DescriptionAz = "Sağlamlıq məhsulları", DescriptionEn = "Health products"       },
            new() { NameAz = "Yataq",       NameEn = "Bedding",     DescriptionAz = "Yataq və istirahət",   DescriptionEn = "Beds and rest"         },
        });

        await context.SaveChangesAsync();
    }

    // ── PRODUCTS ────────────────────────────────────────────────
    private static async Task SeedProductsAsync(AppDbContext context)
    {
        if (await context.Products.AnyAsync()) return;

        var categories = await context.ProductCategories.ToListAsync();
        var food = categories.First(c => c.NameEn == "Food").Id;
        var acc = categories.First(c => c.NameEn == "Accessories").Id;
        var toys = categories.First(c => c.NameEn == "Toys").Id;
        var health = categories.First(c => c.NameEn == "Health").Id;
        var bed = categories.First(c => c.NameEn == "Bedding").Id;

        await context.Products.AddRangeAsync(new List<Product>
        {
            // Qida
            new() { NameAz = "İt yemi Premium",      NameEn = "Dog Food Premium",    DescriptionAz = "Yüksək keyfiyyətli it yemi",      DescriptionEn = "High quality dog food",        Price = 25.99m, StockQuantity = 50, CategoryId = food,   ImageUrl = "https://placehold.co/400x300?text=Dog+Food",    IsActive = true },
            new() { NameAz = "Pişik yemi Deluxe",    NameEn = "Cat Food Deluxe",     DescriptionAz = "Premium pişik yemi",              DescriptionEn = "Premium cat food",             Price = 19.99m, StockQuantity = 40, CategoryId = food,   ImageUrl = "https://placehold.co/400x300?text=Cat+Food",    IsActive = true },
            new() { NameAz = "Quş yemi Natural",     NameEn = "Bird Food Natural",   DescriptionAz = "Təbii quş yemi",                  DescriptionEn = "Natural bird food",            Price = 12.50m, StockQuantity = 30, CategoryId = food,   ImageUrl = "https://placehold.co/400x300?text=Bird+Food",   IsActive = true },
            // Aksesuarlar
            new() { NameAz = "İt boyunbağısı",       NameEn = "Dog Collar",          DescriptionAz = "Dəri it boyunbağısı",             DescriptionEn = "Leather dog collar",           Price = 15.00m, StockQuantity = 25, CategoryId = acc,    ImageUrl = "https://placehold.co/400x300?text=Collar",      IsActive = true },
            new() { NameAz = "Pişik daşıyıcısı",    NameEn = "Cat Carrier",         DescriptionAz = "Yüngül pişik daşıyıcısı",        DescriptionEn = "Lightweight cat carrier",      Price = 45.00m, StockQuantity = 15, CategoryId = acc,    ImageUrl = "https://placehold.co/400x300?text=Carrier",     IsActive = true },
            new() { NameAz = "İt qayışı",            NameEn = "Dog Leash",           DescriptionAz = "Möhkəm it qayışı",               DescriptionEn = "Strong dog leash",             Price = 10.00m, StockQuantity = 35, CategoryId = acc,    ImageUrl = "https://placehold.co/400x300?text=Leash",       IsActive = true },
            // Oyuncaqlar
            new() { NameAz = "Rezin sümük oyuncaq",  NameEn = "Rubber Bone Toy",     DescriptionAz = "İtlər üçün rezin sümük",         DescriptionEn = "Rubber bone for dogs",         Price = 8.99m,  StockQuantity = 60, CategoryId = toys,   ImageUrl = "https://placehold.co/400x300?text=Bone+Toy",    IsActive = true },
            new() { NameAz = "Pişik tüy oyuncağı",  NameEn = "Cat Feather Toy",     DescriptionAz = "Pişiklər üçün tüy oyuncaq",      DescriptionEn = "Feather toy for cats",         Price = 5.99m,  StockQuantity = 45, CategoryId = toys,   ImageUrl = "https://placehold.co/400x300?text=Feather+Toy", IsActive = true },
            new() { NameAz = "İnteraktiv top",       NameEn = "Interactive Ball",    DescriptionAz = "Elektronik interaktiv top",       DescriptionEn = "Electronic interactive ball",  Price = 22.00m, StockQuantity = 20, CategoryId = toys,   ImageUrl = "https://placehold.co/400x300?text=Ball",        IsActive = true },
            // Sağlamlıq
            new() { NameAz = "Vitamin kompleksi",    NameEn = "Vitamin Complex",     DescriptionAz = "İtlər üçün vitamin kompleksi",   DescriptionEn = "Vitamin complex for dogs",     Price = 18.50m, StockQuantity = 30, CategoryId = health, ImageUrl = "https://placehold.co/400x300?text=Vitamins",    IsActive = true },
            new() { NameAz = "Pişik şampunu",        NameEn = "Cat Shampoo",         DescriptionAz = "Yumşaq pişik şampunu",           DescriptionEn = "Gentle cat shampoo",           Price = 9.99m,  StockQuantity = 40, CategoryId = health, ImageUrl = "https://placehold.co/400x300?text=Shampoo",     IsActive = true },
            new() { NameAz = "Bit əleyhinə damcı",  NameEn = "Anti-flea Drops",     DescriptionAz = "Bit və gənə əleyhinə damcı",     DescriptionEn = "Anti-flea and tick drops",     Price = 14.00m, StockQuantity = 3,  CategoryId = health, ImageUrl = "https://placehold.co/400x300?text=Drops",       IsActive = true },
            // Yataq
            new() { NameAz = "İt yatağı Comfort",   NameEn = "Dog Bed Comfort",     DescriptionAz = "Yumşaq it yatağı",               DescriptionEn = "Soft dog bed",                 Price = 35.00m, StockQuantity = 12, CategoryId = bed,    ImageUrl = "https://placehold.co/400x300?text=Dog+Bed",     IsActive = true },
            new() { NameAz = "Pişik evi",            NameEn = "Cat House",           DescriptionAz = "Taxta pişik evi",                DescriptionEn = "Wooden cat house",             Price = 55.00m, StockQuantity = 8,  CategoryId = bed,    ImageUrl = "https://placehold.co/400x300?text=Cat+House",   IsActive = true },
            new() { NameAz = "Həmər yataq",          NameEn = "Hammock Bed",         DescriptionAz = "Pişik həməri",                   DescriptionEn = "Cat hammock",                  Price = 16.00m, StockQuantity = 4,  CategoryId = bed,    ImageUrl = "https://placehold.co/400x300?text=Hammock",     IsActive = true },
        });

        await context.SaveChangesAsync();
    }

    // ── SERVICES ─────────────────────────────────────────────────
    private static async Task SeedServicesAsync(AppDbContext context)
    {
        if (await context.Services.AnyAsync()) return;

        await context.Services.AddRangeAsync(new List<Service>
        {
            new() { NameAz = "Qroominq",           NameEn = "Grooming",      DescriptionAz = "Professional pet qroominq xidməti", DescriptionEn = "Professional pet grooming",  Price = 35.00m, DurationMinutes = 60,   ImageUrl = "https://placehold.co/400x300?text=Grooming",  IsActive = true },
            new() { NameAz = "Veterinar müayinə",  NameEn = "Vet Checkup",   DescriptionAz = "Ümumi veterinar müayinəsi",          DescriptionEn = "General veterinary checkup", Price = 25.00m, DurationMinutes = 30,   ImageUrl = "https://placehold.co/400x300?text=Checkup",   IsActive = true },
            new() { NameAz = "Peyvənd",            NameEn = "Vaccination",   DescriptionAz = "İllik peyvənd xidməti",              DescriptionEn = "Annual vaccination service", Price = 20.00m, DurationMinutes = 15,   ImageUrl = "https://placehold.co/400x300?text=Vaccine",   IsActive = true },
            new() { NameAz = "Pet oteli",          NameEn = "Pet Hotel",     DescriptionAz = "Günlük pet otel xidməti",            DescriptionEn = "Daily pet hotel service",    Price = 30.00m, DurationMinutes = 1440, ImageUrl = "https://placehold.co/400x300?text=Hotel",     IsActive = true },
            new() { NameAz = "Tədris",             NameEn = "Training",      DescriptionAz = "İt tədris kursları",                 DescriptionEn = "Dog training courses",       Price = 50.00m, DurationMinutes = 90,   ImageUrl = "https://placehold.co/400x300?text=Training",  IsActive = true },
        });

        await context.SaveChangesAsync();
    }

    // ── VETERINARIANS ────────────────────────────────────────────
    private static async Task SeedVeterinariansAsync(AppDbContext context)
    {
        if (await context.Veterinarians.AnyAsync()) return;

        await context.Veterinarians.AddRangeAsync(new List<Veterinarian>
        {
            new() { FullName = "Dr. Əli Həsənov",     Specialty = "Cərrahiyyə",     PhoneNumber = "+994501234567", Email = "ali@petcare.az",   ProfileImageUrl = "https://placehold.co/300x300?text=Dr+Ali",   ExperienceYears = 8,  IsAvailable = true },
            new() { FullName = "Dr. Leyla Məmmədova", Specialty = "Dərmatologiya",  PhoneNumber = "+994552345678", Email = "leyla@petcare.az", ProfileImageUrl = "https://placehold.co/300x300?text=Dr+Leyla", ExperienceYears = 5,  IsAvailable = true },
            new() { FullName = "Dr. Rauf Quliyev",    Specialty = "Diş həkimi",     PhoneNumber = "+994703456789", Email = "rauf@petcare.az",  ProfileImageUrl = "https://placehold.co/300x300?text=Dr+Rauf",  ExperienceYears = 12, IsAvailable = true },
            new() { FullName = "Dr. Nigar Əliyeva",   Specialty = "Ümumi praktika", PhoneNumber = "+994604567890", Email = "nigar@petcare.az", ProfileImageUrl = "https://placehold.co/300x300?text=Dr+Nigar", ExperienceYears = 3,  IsAvailable = true },
        });

        await context.SaveChangesAsync();
    }

    // ── FAQS ─────────────────────────────────────────────────────
    private static async Task SeedFaqsAsync(AppDbContext context)
    {
        if (await context.Faqs.AnyAsync()) return;

        await context.Faqs.AddRangeAsync(new List<Faq>
        {
            new() { QuestionAz = "Çatdırılma neçə günə olur?",             QuestionEn = "How long does delivery take?",        AnswerAz = "Sifariş verildikdən 1-3 iş günü ərzində.",        AnswerEn = "Within 1-3 business days after ordering."  },
            new() { QuestionAz = "Geri qaytarma mümkündürmü?",             QuestionEn = "Is return possible?",                 AnswerAz = "Alışdan 14 gün ərzində geri qaytara bilərsiniz.", AnswerEn = "You can return within 14 days of purchase." },
            new() { QuestionAz = "Ödəniş üsulları hansılardır?",           QuestionEn = "What payment methods are available?", AnswerAz = "Kart, nağd və onlayn ödəniş qəbul edilir.",       AnswerEn = "Card, cash and online payment accepted."    },
            new() { QuestionAz = "Veterinar xidməti üçün necə qeydiyyat?", QuestionEn = "How to register for vet service?",    AnswerAz = "Saytdan appointment sifariş edə bilərsiniz.",      AnswerEn = "You can book an appointment on our website."},
            new() { QuestionAz = "Minimum sifariş məbləği varmı?",         QuestionEn = "Is there a minimum order amount?",    AnswerAz = "Minimum sifariş məbləği yoxdur.",                 AnswerEn = "There is no minimum order amount."          },
        });

        await context.SaveChangesAsync();
    }

    // ── USERS ────────────────────────────────────────────────────
    private static async Task SeedUsersAsync(AppDbContext context)
    {
        if (await context.Users.AnyAsync()) return;

        await context.Users.AddRangeAsync(new List<User>
        {
            new()
            {
                FullName     = "Admin User",
                Email        = "admin@petcare.az",
                PasswordHash = PasswordHasher.HashPassword("Admin123!"),
                PhoneNumber  = "+994501111111",
                Role         = UserRole.Admin
            },
            new()
            {
                FullName     = "Test User",
                Email        = "user@petcare.az",
                PasswordHash = PasswordHasher.HashPassword("User123!"),
                PhoneNumber  = "+994502222222",
                Role         = UserRole.User
            },
        });

        await context.SaveChangesAsync();
    }

    // ── COUPONS ──────────────────────────────────────────────────
    private static async Task SeedCouponsAsync(AppDbContext context)
    {
        if (await context.Coupons.AnyAsync()) return;

        await context.Coupons.AddRangeAsync(new List<Coupon>
        {
            new() { Code = "WELCOME10", DiscountPercent = 10, IsActive = true, ExpireDate = DateTime.UtcNow.AddYears(1)  },
            new() { Code = "SUMMER20",  DiscountPercent = 20, IsActive = true, ExpireDate = DateTime.UtcNow.AddMonths(6) },
            new() { Code = "PET50",     DiscountPercent = 50, IsActive = true, ExpireDate = DateTime.UtcNow.AddMonths(3) },
        });

        await context.SaveChangesAsync();
    }
}