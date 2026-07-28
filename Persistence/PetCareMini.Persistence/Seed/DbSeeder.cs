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
        //await SeedVeterinariansAsync(context);
        await SeedFaqsAsync(context);
        await SeedUsersAsync(context);
        await SeedCouponsAsync(context);
        await SeedVeterinaryReviewsAsync(context);
        await SeedPetsAsync(context);
        await SeedContactMessagesAsync(context);
        await SeedBlogAsync(context);
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
    //private static async Task SeedVeterinariansAsync(AppDbContext context)
    //{
    //    if (await context.Veterinarians.AnyAsync()) return;

    //    await context.Veterinarians.AddRangeAsync(new List<Veterinarian>
    //    {
    //        new() { FullName = "Dr. Əli Həsənov",     Specialty = "Cərrahiyyə",     PhoneNumber = "+994501234567", Email = "ali@petcare.az",   ProfileImageUrl = "https://placehold.co/300x300?text=Dr+Ali",   ExperienceYears = 8,  IsAvailable = true },
    //        new() { FullName = "Dr. Leyla Məmmədova", Specialty = "Dərmatologiya",  PhoneNumber = "+994552345678", Email = "leyla@petcare.az", ProfileImageUrl = "https://placehold.co/300x300?text=Dr+Leyla", ExperienceYears = 5,  IsAvailable = true },
    //        new() { FullName = "Dr. Rauf Quliyev",    Specialty = "Diş həkimi",     PhoneNumber = "+994703456789", Email = "rauf@petcare.az",  ProfileImageUrl = "https://placehold.co/300x300?text=Dr+Rauf",  ExperienceYears = 12, IsAvailable = true },
    //        new() { FullName = "Dr. Nigar Əliyeva",   Specialty = "Ümumi praktika", PhoneNumber = "+994604567890", Email = "nigar@petcare.az", ProfileImageUrl = "https://placehold.co/300x300?text=Dr+Nigar", ExperienceYears = 3,  IsAvailable = true },
    //    });

    //    await context.SaveChangesAsync();
    //}

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


    // ── VETERINARY REVIEWS ─────────────────────────────────────────
    private static async Task SeedVeterinaryReviewsAsync(AppDbContext context)
    {
        if (await context.VeterinaryReviews.AnyAsync()) return;

        var users = await context.Users.ToListAsync();
        var veterinarians = await context.Veterinarians.ToListAsync();
        var services = await context.Services.ToListAsync();

        if (!users.Any() || !veterinarians.Any() || !services.Any())
            return;

        await context.VeterinaryReviews.AddRangeAsync(new List<VeterinaryReview>
    {
        new()
        {
            UserId = users[1].Id,
            VeterinarianId = veterinarians[0].Id,
            ServiceId = services[1].Id,

            Rating = 5,

            CommentAz = "Möhtəşəm həkimdir. Çox diqqətli və peşəkardır.",
            CommentEn = "Amazing doctor. Very caring and professional.",

            IsApproved = true,
            IsFeatured = true
        },

        new()
        {
            UserId = users[1].Id,
            VeterinarianId = veterinarians[1].Id,
            ServiceId = services[0].Id,

            Rating = 4,

            CommentAz = "Klinika çox təmiz idi və xidmət sürətli oldu.",
            CommentEn = "Clinic was very clean and service was fast.",

            IsApproved = true,
            IsFeatured = true
        },

        new()
        {
            UserId = users[1].Id,
            VeterinarianId = veterinarians[2].Id,
            ServiceId = services[2].Id,

            Rating = 5,

            CommentAz = "İndiyə qədərki ən yaxşı veterinar təcrübəsi idi.",
            CommentEn = "Best veterinary experience ever!",

            IsApproved = true,
            IsFeatured = false
        },

        new()
        {
            UserId = users[1].Id,
            VeterinarianId = veterinarians[3].Id,
            ServiceId = services[1].Id,

            Rating = 3,

            CommentAz = "Xidmət yaxşı idi amma gözləmə vaxtı uzun çəkdi.",
            CommentEn = "Good service but waiting time was long.",

            IsApproved = true,
            IsFeatured = false
        },

        new()
        {
            UserId = users[1].Id,
            VeterinarianId = veterinarians[0].Id,
            ServiceId = services[4].Id,

            Rating = 5,

            CommentAz = "İtim təlim seanslarını çox sevdi.",
            CommentEn = "My dog loved the training sessions.",

            IsApproved = true,
            IsFeatured = true
        },

        new()
        {
            UserId = users[1].Id,
            VeterinarianId = veterinarians[2].Id,
            ServiceId = services[3].Id,

            Rating = 4,

            CommentAz = "Pet hotel xidməti çox rahat və təmiz idi.",
            CommentEn = "Pet hotel service was very comfortable and clean.",

            IsApproved = true,
            IsFeatured = false
        },

        new()
        {
            UserId = users[1].Id,
            VeterinarianId = veterinarians[1].Id,
            ServiceId = services[2].Id,

            Rating = 5,

            CommentAz = "Peyvənd prosesi çox sürətli və təhlükəsiz keçdi.",
            CommentEn = "Vaccination process was quick and safe.",

            IsApproved = true,
            IsFeatured = true
        },

        new()
        {
            UserId = users[1].Id,
            VeterinarianId = veterinarians[3].Id,
            ServiceId = services[0].Id,

            Rating = 4,

            CommentAz = "Qroominq xidməti çox peşəkar idi.",
            CommentEn = "Grooming service was very professional.",

            IsApproved = true,
            IsFeatured = false
        },

        new()
        {
            UserId = users[1].Id,
            VeterinarianId = veterinarians[0].Id,
            ServiceId = services[1].Id,

            Rating = 5,

            CommentAz = "Həkim bütün suallarıma ətraflı cavab verdi.",
            CommentEn = "The doctor answered all my questions clearly.",

            IsApproved = true,
            IsFeatured = true
        },

        new()
        {
            UserId = users[1].Id,
            VeterinarianId = veterinarians[2].Id,
            ServiceId = services[4].Id,

            Rating = 4,

            CommentAz = "Təlimlər faydalı və maraqlı idi.",
            CommentEn = "Training sessions were useful and engaging.",

            IsApproved = true,
            IsFeatured = false
        },

        new()
        {
            UserId = users[1].Id,
            VeterinarianId = veterinarians[1].Id,
            ServiceId = services[3].Id,

            Rating = 5,

            CommentAz = "Oteldə heyvanlara çox yaxşı baxılır.",
            CommentEn = "Pets are treated very well at the hotel.",

            IsApproved = true,
            IsFeatured = true
        },

        new()
        {
            UserId = users[1].Id,
            VeterinarianId = veterinarians[3].Id,
            ServiceId = services[2].Id,

            Rating = 3,

            CommentAz = "Normal xidmət idi, amma daha yaxşı ola bilərdi.",
            CommentEn = "Average service, could be better.",

            IsApproved = true,
            IsFeatured = false
        }
    });

        await context.SaveChangesAsync();
    }
    private static async Task SeedPetsAsync(AppDbContext context)
    {
        if (await context.Pets.AnyAsync()) return;

        var users = await context.Users.ToListAsync();

        if (!users.Any())
            return;

        await context.Pets.AddRangeAsync(new List<Pet>
    {
        new()
        {
            Name = "Max",
            Age = 3,
            Gender = "Male",
            Type = "Dog",
            Breed = "Golden Retriever",
            Weight = 28.5m,
            Notes = "Very friendly and energetic.",
            OwnerId = users[1].Id
        },

        new()
        {
            Name = "Mia",
            Age = 2,
            Gender = "Female",
            Type = "Cat",
            Breed = "British Shorthair",
            Weight = 4.2m,
            Notes = "Loves sleeping all day.",
            OwnerId = users[1].Id
        },

        new()
        {
            Name = "Rocky",
            Age = 5,
            Gender = "Male",
            Type = "Dog",
            Breed = "German Shepherd",
            Weight = 35.0m,
            Notes = "Needs regular training.",
            OwnerId = users[1].Id
        },

        new()
        {
            Name = "Luna",
            Age = 1,
            Gender = "Female",
            Type = "Cat",
            Breed = "Siamese",
            Weight = 3.8m,
            Notes = "Very playful kitten.",
            OwnerId = users[1].Id
        },

        new()
        {
            Name = "Coco",
            Age = 4,
            Gender = "Female",
            Type = "Bird",
            Breed = "Parrot",
            Weight = 1.1m,
            Notes = "Can say a few words.",
            OwnerId = users[1].Id
        }
    });

        await context.SaveChangesAsync();
    }
    // ── CONTACT MESSAGES ─────────────────────────────────────────
    private static async Task SeedContactMessagesAsync(AppDbContext context)
    {
        if (await context.ContactMessages.AnyAsync()) return;

        var users = await context.Users.ToListAsync();
        if (!users.Any()) return;

        var userId = users[1].Id; // Test User

        await context.ContactMessages.AddRangeAsync(new List<ContactMessage>
    {
        new()
        {
            Subject = "Çatdırılma haqqında sual",
            Message = "Salam, sifariş verdim amma hələ çatdırılmayıb. Nə vaxt gəlib çatacaq?",
            UserId = userId,
            IsRead = true,
            ReadAt = DateTime.UtcNow.AddDays(-3),
            IsArchived = false,
            ReplyMessage = "Hörmətli müştəri, sifarişiniz 1-2 iş günü ərzində çatdırılacaq. Səbriniz üçün təşəkkür edirik.",
            RepliedAt = DateTime.UtcNow.AddDays(-3)
        },
        new()
        {
            Subject = "Məhsul geri qaytarma",
            Message = "Aldığım it yemi məhsulu açıldıqdan sonra xarab çıxdı. Geri qaytara bilərəmmi?",
            UserId = userId,
            IsRead = true,
            ReadAt = DateTime.UtcNow.AddDays(-5),
            IsArchived = false,
            ReplyMessage = "Bəli, məhsulu 14 gün ərzində geri qaytara bilərsiniz. Zəhmət olmasa məhsulu orijinal qablaşdırmada göndərin.",
            RepliedAt = DateTime.UtcNow.AddDays(-5)
        },
        new()
        {
            Subject = "Veterinar randevusu haqqında",
            Message = "Sabah randevum var, amma gələ bilməyəcəyəm. Ləğv etmək mümkündürmü?",
            UserId = userId,
            IsRead = true,
            ReadAt = DateTime.UtcNow.AddDays(-2),
            IsArchived = false,
            ReplyMessage = "Randevunuzu ləğv etdik. Yeni randevu üçün istənilən vaxt müraciət edə bilərsiniz.",
            RepliedAt = DateTime.UtcNow.AddDays(-2)
        },
        new()
        {
            Subject = "Kupon kodu işləmir",
            Message = "SUMMER20 kupon kodunu daxil etdim amma endirim tətbiq olunmur. Kömək edə bilərsinizmi?",
            UserId = userId,
            IsRead = true,
            ReadAt = DateTime.UtcNow.AddDays(-1),
            IsArchived = false,
            ReplyMessage = "Kupon kodunun istifadə şərtlərini yoxlayın. Minimum sifariş məbləği 30 AZN olmalıdır. Hər hansı problem olarsa bildirin.",
            RepliedAt = DateTime.UtcNow.AddDays(-1)
        },
        new()
        {
            Subject = "Stokda olmayan məhsul",
            Message = "Pişik evi məhsulu stokda yoxdur görünür. Nə vaxt gəlib çatacaq?",
            UserId = userId,
            IsRead = false,
            IsArchived = false,
            ReplyMessage = null,
            RepliedAt = null
        },
        new()
        {
            Subject = "Ödəniş problemi",
            Message = "Kart ilə ödəniş etməyə çalışdım amma uğursuz oldu. Başqa ödəniş üsulu varmı?",
            UserId = userId,
            IsRead = false,
            IsArchived = false,
            ReplyMessage = null,
            RepliedAt = null
        },
        new()
        {
            Subject = "Pet otel xidməti haqqında",
            Message = "Pet otel xidmətiniz haqqında ətraflı məlumat ala bilərəmmi? Qiymət və şərtlər nədən ibarətdir?",
            UserId = userId,
            IsRead = true,
            ReadAt = DateTime.UtcNow.AddDays(-7),
            IsArchived = true,
            ReplyMessage = "Pet otel xidmətimiz gündəlik 30 AZN-dir. Yeməkdən tutmuş gəzintiyə qədər bütün qulluq daxildir.",
            RepliedAt = DateTime.UtcNow.AddDays(-7)
        },
        new()
        {
            Subject = "Qroominq xidməti rezervasiyası",
            Message = "Köpəyim üçün qroominq xidməti almaq istəyirəm. Hansı tarixlər mövcuddur?",
            UserId = userId,
            IsRead = true,
            ReadAt = DateTime.UtcNow.AddDays(-10),
            IsArchived = true,
            ReplyMessage = "Bu həftə çərşənbə və cümə günləri boş yerlər var. Randevu sistemi üzərindən rezervasiya edə bilərsiniz.",
            RepliedAt = DateTime.UtcNow.AddDays(-10)
        },
        new()
        {
            Subject = "Məhsul haqqında sual",
            Message = "Vitamin kompleksi məhsulu neçə yaşdan yuxarı itlər üçün uyğundur?",
            UserId = userId,
            IsRead = false,
            IsArchived = false,
            ReplyMessage = null,
            RepliedAt = null
        },
        new()
        {
            Subject = "Saytda xəta var",
            Message = "Məhsulları səbətə əlavə edəndə səhifə donur. Bu problem nə vaxtdan bəridir?",
            UserId = userId,
            IsRead = false,
            IsArchived = false,
            ReplyMessage = null,
            RepliedAt = null
        },
        new()
        {
            Subject = "Sifarişin vəziyyəti",
            Message = "Sifariş verdim amma hələ təsdiqlənməyib. Nə vaxt təsdiqlənəcək?",
            UserId = userId,
            IsRead = true,
            ReadAt = DateTime.UtcNow.AddDays(-4),
            IsArchived = false,
            ReplyMessage = "Sifarişiniz təsdiqlənmək üzrədir. Normalda 24 saat ərzində təsdiqlənir.",
            RepliedAt = DateTime.UtcNow.AddDays(-4)
        }
    });

        await context.SaveChangesAsync();
    }
    // ── BLOG ─────────────────────────────────────────────────────
    private static async Task SeedBlogAsync(AppDbContext context)
    {
        if (await context.BlogCategories.AnyAsync()) return;

        var users = await context.Users.ToListAsync();
        if (!users.Any()) return;

        var adminId = users.First(u => u.Role == UserRole.Admin).Id;
        var userId = users.First(u => u.Role == UserRole.User).Id;

        // ── Kateqoriyalar ────────────────────────────────────────
        var categories = new List<BlogCategory>
    {
        new() { NameAz = "İt Baxımı",       NameEn = "Dog Care",       SlugAz = "it-baximi",       SlugEn = "dog-care",       DescriptionAz = "İtlər üçün baxım məsləhətləri",          DescriptionEn = "Care tips for dogs",             IconUrl = "🐶" },
        new() { NameAz = "Pişik Baxımı",    NameEn = "Cat Care",       SlugAz = "pisik-baximi",    SlugEn = "cat-care",       DescriptionAz = "Pişiklər üçün baxım məsləhətləri",       DescriptionEn = "Care tips for cats",             IconUrl = "🐱" },
        new() { NameAz = "Sağlamlıq",       NameEn = "Health",         SlugAz = "saglamliq",       SlugEn = "health",         DescriptionAz = "Ev heyvanlarının sağlamlığı",            DescriptionEn = "Pet health and wellness",        IconUrl = "💊" },
        new() { NameAz = "Qroominq",        NameEn = "Grooming",       SlugAz = "qroominq",        SlugEn = "grooming",       DescriptionAz = "Baxım və qroominq məsləhətləri",         DescriptionEn = "Grooming tips and tricks",       IconUrl = "✂️" },
        new() { NameAz = "Qidalanma",       NameEn = "Nutrition",      SlugAz = "qidalanma",       SlugEn = "nutrition",      DescriptionAz = "Düzgün qidalanma tövsiyələri",           DescriptionEn = "Proper nutrition advice",        IconUrl = "🍖" },
        new() { NameAz = "Veterinar",       NameEn = "Veterinary",     SlugAz = "veterinar",       SlugEn = "veterinary",     DescriptionAz = "Veterinar məsləhətləri",                 DescriptionEn = "Veterinary advice",             IconUrl = "🩺" },
    };

        await context.BlogCategories.AddRangeAsync(categories);
        await context.SaveChangesAsync();

        // ── Taglər ───────────────────────────────────────────────
        var tags = new List<BlogTag>
    {
        new()
        {
            NameAz = "İt",
            NameEn = "Dog",
            SlugAz = "it",
            SlugEn = "dog"
        },

        new()
        {
            NameAz = "Pişik",
            NameEn = "Cat",
            SlugAz = "pisik",
            SlugEn = "cat"
        },

        new()
        {
            NameAz = "Sağlamlıq",
            NameEn = "Health",
            SlugAz = "saglamliq",
            SlugEn = "health"
        },

        new()
        {
            NameAz = "Qroominq",
            NameEn = "Grooming",
            SlugAz = "qroominq",
            SlugEn = "grooming"
        },

        new()
        {
            NameAz = "Qidalanma",
            NameEn = "Nutrition",
            SlugAz = "qidalanma",
            SlugEn = "nutrition"
        },

        new()
        {
            NameAz = "Yavru",
            NameEn = "Puppy",
            SlugAz = "yavru",
            SlugEn = "puppy"
        },

        new()
        {
            NameAz = "Peyvənd",
            NameEn = "Vaccination",
            SlugAz = "peyvend",
            SlugEn = "vaccination"
        },

        new()
        {
            NameAz = "Məsləhət",
            NameEn = "Advice",
            SlugAz = "meslehet",
            SlugEn = "advice"
        }
    };

        await context.BlogTags.AddRangeAsync(tags);
        await context.SaveChangesAsync();

        // ── Author profillər ─────────────────────────────────────
        var authorProfiles = new List<BlogAuthorProfile>
    {
        new()
        {
            UserId = adminId,
            Bio = "PetCare komandası olaraq ev heyvanlarınızın sağlamlığı üçün ən yaxşı məsləhətləri paylaşırıq.",
            ProfileImageUrl = "https://placehold.co/100x100?text=Admin",
            WebsiteUrl = "https://petcare.az",
            InstagramUrl = "https://instagram.com/petcare",
            LinkedInUrl = null,
            TotalPosts = 0
        },
        new()
        {
            UserId = userId,
            Bio = "Ev heyvanı sevdalısı. İt və pişiklərlə 10 illik təcrübə.",
            ProfileImageUrl = "https://placehold.co/100x100?text=User",
            WebsiteUrl = null,
            InstagramUrl = "https://instagram.com/petlover",
            LinkedInUrl = null,
            TotalPosts = 0
        }
    };

        await context.BlogAuthorProfiles.AddRangeAsync(authorProfiles);
        await context.SaveChangesAsync();

        // ── Postlar ──────────────────────────────────────────────
        var dogCare = categories[0];
        var catCare = categories[1];
        var health = categories[2];
        var grooming = categories[3];
        var nutrition = categories[4];
        var veterinary = categories[5];

        var tagIt = tags[0];
        var tagPisik = tags[1];
        var tagSaglamliq = tags[2];
        var tagQroominq = tags[3];
        var tagQida = tags[4];
        var tagYavru = tags[5];
        var tagPeyvend = tags[6];
        var tagMeslehet = tags[7];

        var posts = new List<BlogPost>
    {
        new()
        {
            TitleAz = "İtin Gündəlik Baxım Rutini",
            TitleEn = "Daily Dog Care Routine",
            SlugAz = "itin-gundelik-baxim-rutini",
            SlugEn = "daily-dog-care-routine",
            SummaryAz = "İtinizin sağlam və xoşbəxt olması üçün gündəlik baxım rutini haqqında bilməli olduğunuz hər şey.",
            SummaryEn = "Everything you need to know about daily care routine to keep your dog healthy and happy.",
            ContentAz = "İtlər sadiq dostlarımızdır və onlara lazımi qayğı göstərmək bizim vəzifəmizdir. Gündəlik baxım rutini olaraq hər gün ən azı 30 dəqiqə gəzintiyə çıxarmaq, düzgün qidalandırmaq və tüklərini daramaq lazımdır. Bundan əlavə, hər həftə çimizdirilməsi tövsiyə olunur.",
            ContentEn = "Dogs are our loyal companions and it is our duty to take proper care of them. As a daily care routine, you should take them for at least 30 minutes of walk, feed them properly and brush their fur. Additionally, bathing them once a week is recommended.",
            CoverImageUrl = "https://placehold.co/800x400?text=Dog+Care",
            CategoryId = dogCare.Id,
            AuthorId = adminId,
            Status = BlogPostStatus.Published,
            PublishedAt = DateTime.UtcNow.AddDays(-10),
            ReadTimeMinutes = 3,
            ViewCount = 245,
            BlogPostTags = new List<BlogPostTag>
            {
                new() { TagId = tagIt.Id },
                new() { TagId = tagMeslehet.Id }
            }
        },
        new()
        {
            TitleAz = "Pişik Yavrusu Evə Gətirərkən Nə Etməli?",
            TitleEn = "What to Do When Bringing a Kitten Home?",
            SlugAz = "pisik-yavrusu-eve-getirende-ne-etmeli",
            SlugEn = "what-to-do-when-bringing-kitten-home",
            SummaryAz = "Evə yeni pişik yavrusu gətirərkən hazırlıq və ilk günlər haqqında praktiki məsləhətlər.",
            SummaryEn = "Practical tips about preparation and first days when bringing a new kitten home.",
            ContentAz = "Yeni pişik yavrusu evə gətirilməzdən əvvəl bir neçə hazırlıq işi görülməlidir. Yataq yeri, qida qabı, su qabı və tualet qumu hazırlanmalıdır. İlk günlər yavru stresli ola bilər, ona görə sakit mühit yaradılmalıdır.",
            ContentEn = "Before bringing a new kitten home, several preparations need to be made. A sleeping area, food bowl, water bowl and litter box should be prepared. The kitten may be stressed in the first days, so a calm environment should be created.",
            CoverImageUrl = "https://placehold.co/800x400?text=Kitten+Home",
            CategoryId = catCare.Id,
            AuthorId = adminId,
            Status = BlogPostStatus.Published,
            PublishedAt = DateTime.UtcNow.AddDays(-8),
            ReadTimeMinutes = 4,
            ViewCount = 189,
            BlogPostTags = new List<BlogPostTag>
            {
                new() { TagId = tagPisik.Id },
                new() { TagId = tagYavru.Id }
            }
        },
        new()
        {
            TitleAz = "İtlər Üçün Peyvənd Cədvəli",
            TitleEn = "Vaccination Schedule for Dogs",
            SlugAz = "itler-ucun-peyvend-cedveli",
            SlugEn = "vaccination-schedule-for-dogs",
            SummaryAz = "İtinizi xəstəliklərdən qorumaq üçün peyvənd cədvəli və tövsiyələr.",
            SummaryEn = "Vaccination schedule and recommendations to protect your dog from diseases.",
            ContentAz = "Peyvəndlər itlərin sağlamlığını qorumaq üçün ən vacib vasitələrdən biridir. Yavru itlər 6-8 həftəlikdən başlayaraq peyvənd olunmalıdır. İllik təkrar peyvəndlər mütləq edilməlidir.",
            ContentEn = "Vaccinations are one of the most important tools to protect dog health. Puppies should be vaccinated starting from 6-8 weeks of age. Annual booster vaccinations are mandatory.",
            CoverImageUrl = "https://placehold.co/800x400?text=Vaccination",
            CategoryId = health.Id,
            AuthorId = adminId,
            Status = BlogPostStatus.Published,
            PublishedAt = DateTime.UtcNow.AddDays(-6),
            ReadTimeMinutes = 5,
            ViewCount = 312,
            BlogPostTags = new List<BlogPostTag>
            {
                new() { TagId = tagIt.Id },
                new() { TagId = tagPeyvend.Id },
                new() { TagId = tagSaglamliq.Id }
            }
        },
        new()
        {
            TitleAz = "Pişikləri Evdə Necə Qroominq Etməli?",
            TitleEn = "How to Groom Cats at Home?",
            SlugAz = "pisikleri-evde-nece-qroominq-etmeli",
            SlugEn = "how-to-groom-cats-at-home",
            SummaryAz = "Pişiyinizi evdə qroominq etmək üçün lazımi alətlər və addım-addım təlimat.",
            SummaryEn = "Necessary tools and step-by-step guide for grooming your cat at home.",
            ContentAz = "Pişiklərin qroomingü onların sağlamlığı üçün vacibdir. Lazımi alətlər: xüsusi daraq, dırnaq makası, pişik şampunu. Həftədə bir dəfə daramaq tüklərin düşməsini azaldır.",
            ContentEn = "Grooming cats is important for their health. Necessary tools: special comb, nail scissors, cat shampoo. Brushing once a week reduces shedding.",
            CoverImageUrl = "https://placehold.co/800x400?text=Cat+Grooming",
            CategoryId = grooming.Id,
            AuthorId = userId,
            Status = BlogPostStatus.Published,
            PublishedAt = DateTime.UtcNow.AddDays(-4),
            ReadTimeMinutes = 4,
            ViewCount = 156,
            BlogPostTags = new List<BlogPostTag>
            {
                new() { TagId = tagPisik.Id },
                new() { TagId = tagQroominq.Id }
            }
        },
        new()
        {
            TitleAz = "İtlər Üçün Düzgün Qidalanma",
            TitleEn = "Proper Nutrition for Dogs",
            SlugAz = "itler-ucun-duzgun-qidalanma",
            SlugEn = "proper-nutrition-for-dogs",
            SummaryAz = "İtinizin yaşına və cinsəsinə görə ən uyğun qidalanma planı necə hazırlanır?",
            SummaryEn = "How to prepare the most suitable nutrition plan according to your dog's age and breed?",
            ContentAz = "İtlər üçün düzgün qidalanma onların sağlamlığının əsasını təşkil edir. Zülal, yağ, karbohidrat və vitaminlər tarazlı şəkildə verilməlidir. Yaşa görə yavru, yetkin və yaşlı it yemleri mövcuddur.",
            ContentEn = "Proper nutrition for dogs forms the basis of their health. Protein, fat, carbohydrates and vitamins should be given in a balanced way. Age-appropriate puppy, adult and senior dog foods are available.",
            CoverImageUrl = "https://placehold.co/800x400?text=Dog+Nutrition",
            CategoryId = nutrition.Id,
            AuthorId = userId,
            Status = BlogPostStatus.Published,
            PublishedAt = DateTime.UtcNow.AddDays(-2),
            ReadTimeMinutes = 6,
            ViewCount = 98,
            BlogPostTags = new List<BlogPostTag>
            {
                new() { TagId = tagIt.Id },
                new() { TagId = tagQida.Id },
                new() { TagId = tagMeslehet.Id }
            }
        },
        new()
        {
            TitleAz = "Veterinara Nə Vaxt Getməli?",
            TitleEn = "When Should You Visit the Vet?",
            SlugAz = "veterinara-ne-vaxt-getmeli",
            SlugEn = "when-should-you-visit-the-vet",
            SummaryAz = "Ev heyvanınızda hansı əlamətlər görünsə dərhal veterinara müraciət etməlisiniz?",
            SummaryEn = "What signs in your pet should prompt you to visit the vet immediately?",
            ContentAz = "Ev heyvanlarınızda iştahsızlıq, letarji, qusma, ishal kimi əlamətlər görünsə dərhal veterinara müraciət etmək lazımdır. İllik yoxlama da mütləq edilməlidir.",
            ContentEn = "If you notice signs such as loss of appetite, lethargy, vomiting or diarrhea in your pets, you should visit the vet immediately. Annual check-ups are also mandatory.",
            CoverImageUrl = "https://placehold.co/800x400?text=Vet+Visit",
            CategoryId = veterinary.Id,
            AuthorId = adminId,
            Status = BlogPostStatus.Published,
            PublishedAt = DateTime.UtcNow.AddDays(-1),
            ReadTimeMinutes = 3,
            ViewCount = 421,
            BlogPostTags = new List<BlogPostTag>
            {
                new() { TagId = tagSaglamliq.Id },
                new() { TagId = tagMeslehet.Id }
            }
        },
        // Pending post — admin gözləyir
        new()
        {
            TitleAz = "Pişiklər Üçün Ev Oyunları",
            TitleEn = "Indoor Games for Cats",
            SlugAz = "pisikler-ucun-ev-oyunlari",
            SlugEn = "indoor-games-for-cats",
            SummaryAz = "Pişiyinizi evdə aktiv saxlamaq üçün əyləncəli oyun fikirleri.",
            SummaryEn = "Fun game ideas to keep your cat active indoors.",
            ContentAz = "Pişiklər evdə aktiv qalmaq üçün stimulyasiyaya ehtiyac duyurlar. Lazer işığı, lələk oyuncaqları və qutular onları saatlarla məşğul edə bilər.",
            ContentEn = "Cats need stimulation to stay active indoors. Laser lights, feather toys and boxes can keep them occupied for hours.",
            CoverImageUrl = "https://placehold.co/800x400?text=Cat+Games",
            CategoryId = catCare.Id,
            AuthorId = userId,
            Status = BlogPostStatus.Pending,
            PublishedAt = null,
            ReadTimeMinutes = 3,
            ViewCount = 0,
            BlogPostTags = new List<BlogPostTag>
            {
                new() { TagId = tagPisik.Id },
                new() { TagId = tagMeslehet.Id }
            }
        },
    };

        await context.BlogPosts.AddRangeAsync(posts);
        await context.SaveChangesAsync();

        // ── Şərhlər ──────────────────────────────────────────────
        var firstPost = posts[0];
        var secondPost = posts[1];

        var comments = new List<BlogComment>
    {
        new()
        {
            BlogPostId = firstPost.Id,
            UserId = userId,
            Content = "Çox faydalı məqalə idi, təşəkkür edirəm!",
            Rating = 5,
            IsApproved = true,
            ParentCommentId = null
        },
        new()
        {
            BlogPostId = firstPost.Id,
            UserId = userId,
            Content = "İtimin baxımı üçün tam axtardığım məlumat idi.",
            Rating = 4,
            IsApproved = true,
            ParentCommentId = null
        },
        new()
        {
            BlogPostId = secondPost.Id,
            UserId = userId,
            Content = "Yavru pişiyim üçün çox köməkçi oldu!",
            Rating = 5,
            IsApproved = true,
            ParentCommentId = null
        },
        new()
        {
            BlogPostId = secondPost.Id,
            UserId = adminId,
            Content = "Suallarınız olsa şərh bölməsindən soruşa bilərsiniz.",
            Rating = 5,
            IsApproved = true,
            ParentCommentId = null
        },
    };

        await context.BlogComments.AddRangeAsync(comments);
        await context.SaveChangesAsync();

        // ── Reply şərh ───────────────────────────────────────────
        var reply = new BlogComment
        {
            BlogPostId = firstPost.Id,
            UserId = adminId,
            Content = "Razıyam, gündəlik rutin çox vacibdir!",
            Rating = 5,
            IsApproved = true,
            ParentCommentId = comments[0].Id
        };

        await context.BlogComments.AddAsync(reply);
        await context.SaveChangesAsync();

        // ── Author TotalPosts yenilə ──────────────────────────────
        var adminProfile = await context.BlogAuthorProfiles
            .FirstOrDefaultAsync(x => x.UserId == adminId);
        var userProfile = await context.BlogAuthorProfiles
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (adminProfile != null)
            adminProfile.TotalPosts = posts.Count(p => p.AuthorId == adminId
                                                   && p.Status == BlogPostStatus.Published);
        if (userProfile != null)
            userProfile.TotalPosts = posts.Count(p => p.AuthorId == userId
                                                  && p.Status == BlogPostStatus.Published);

        await context.SaveChangesAsync();
    }
}
