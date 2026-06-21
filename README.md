🐾 PetFun

Azərbaycan bazarı üçün rəqəmsal baytarlıq və ev heyvanı qayğı platforması

Backend: Aysun Şirəlizadə · Frontend: Sübhanə Əlibəyova · Komanda: Vertex

</div>

📌 Layihə haqqında

PetFun — ev heyvanı sahiblərinə hərtərəfli rəqəmsal xidmət təklif edən veb platformadır. İstifadəçilər öz heyvanlarını qeydiyyatdan keçirə, baytarlara görüş təyin edə, onlayn mağazadan məhsul ala, bloq oxuya və süni intellekt ilə məsləhətləşə bilər.

Layihə Azerbaijani Technical University tərəfindən akademik kurs işi kimi hazırlanmış, lakin real production arxitekturası əsasında qurulmuşdur.


🏗️ Arxitektura

Layihə Clean Architecture prinsipinə uyğun 5 qatlı strukturla qurulub:

PetFun/
├── Domain/          # Entity-lər, enum-lar, domain interface-ləri
├── Application/     # Use case-lər, DTO-lar, FluentValidation, interface-lər
├── Persistence/     # EF Core konfiqurasiyaları, Repository implementasiyaları, DbContext
├── Infrastructure/  # JWT, Email, Cloudinary, SignalR, Claude AI xidmətləri
└── WebApi/          # Controller-lər, Middleware, Program.cs


 Texnologiya Yığımı

Backend

Texnologiyaİstifadə məqsədi.NET  / C#Əsas backend frameworkASP.NET Core Web APIRESTful APIEntity Framework CoreORM, Code-First migrationsPostgreSQLVerilənlər bazasıJWT + Refresh TokenAutentifikasiya və avtorizasiyaSignalRReal-vaxt bildirişlərCloudinaryŞəkil yükləmə və idarəetməGmail SMTPEmail bildirişləriAnthropic Claude APIAI məsləhət moduluFluentValidationDaxiletmə validasiyasıSerilogLoglamaSwagger / ScalarAPI sənədləşməsi

Frontend

Texnologiyaİstifadə məqsədiReact 18UI frameworkTypeScriptTip təhlükəsizliyiViteBuild alətiReact RouterSəhifə yönləndirməAxiosHTTP sorğularıBootstrapUI komponentləriContext APIQlobal state idarəetməsi


 Funksionallıq Modulları

 Autentifikasiya


Qeydiyyat, giriş, çıxış
JWT access token + refresh token
Email təsdiqləmə
Şifrə sıfırlama (Gmail SMTP)
Şifrə dəyişdirmə


 İstifadəçi & Admin


Rol əsaslı giriş nəzarəti (Admin, User, Veterinarian)
Admin paneli: istifadəçi, baytarlıq, məhsul idarəetməsi
Profil idarəetməsi


 Ev Heyvanı İdarəetməsi


Heyvan əlavə etmə / yeniləmə / silmə
Növ, cins, yaş, çəki məlumatları
Cloudinary ilə şəkil yükləmə


 Görüş Sistemi


Baytarla görüş təyin etmə
Rol əsaslı görüntüləmə (baytarlar yalnız öz görüşlərini görür)
Status idarəetməsi


 Baytarlıq Profili & Rəylər


Baytarların ixtisas, iş saatı, yer məlumatları
İstifadəçi rəyləri və reytinq sistemi


 E-Ticarət


Məhsullar, Kateqoriyalar
Səbət (Cart), İstək siyahısı (Wishlist)
Sifariş (Orders), Kupon sistemi


 Bloq


Məqalə yaratma, redaktə, silmə
Tag sistemi, şərhlər
Slug avtomatik generasiyası
Cloudinary ilə şəkil yükləmə


 Bildiriş Sistemi


SignalR ilə real-vaxt bildirişlər
Verilənlər bazasında bildiriş saxlanması
Oxunmuş/Oxunmamış statusu


 AI Məsləhət Modulu


Anthropic Claude API inteqrasiyası
Ev heyvanı qayğısı üzrə AI söhbəti
Mock fallback dəstəyi


 Əlaqə & FAQ


Əlaqə forması (e-mail ilə göndərilir)
Tez-tez verilən suallar idarəetməsi



 Quraşdırma

Tələblər


.NET  SDK
PostgreSQL 15+
Node.js 18+


Backend

bash# Repo-nu klonla
git clone https://gitlab.com/aztu-peerexchange/vertex/aztu-peervertex.git
cd petfun/backend

# appsettings.json-u konfiqurasiya et (aşağıya bax)

# Migration-ları tətbiq et
dotnet ef database update

# Layihəni işə sal
dotnet run --project WebApi

Frontend

bashcd petfun/frontend

# Asılılıqları yüklə
npm install

# Development server-i işə sal
npm run dev


 Konfiqurasiya

WebApi/appsettings.json faylında aşağıdakı dəyərləri doldur:

json{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=petfun_db;Username=postgres;Password=YOUR_PASSWORD"
  },
  "JwtSettings": {
    "SecretKey": "YOUR_SECRET_KEY",
    "Issuer": "PetFunApi",
    "Audience": "PetFunClient",
    "ExpiryMinutes": 60
  },
  "CloudinarySettings": {
    "CloudName": "YOUR_CLOUD_NAME",
    "ApiKey": "YOUR_API_KEY",
    "ApiSecret": "YOUR_API_SECRET"
  },
  "EmailSettings": {
    "From": "your@gmail.com",
    "Password": "YOUR_APP_PASSWORD",
    "Host": "smtp.gmail.com",
    "Port": 587
  },
  "ClaudeSettings": {
    "ApiKey": "YOUR_ANTHROPIC_API_KEY"
  }
}


 Layihə Strukturu (qısa)

PetFun/
├── backend/
│   ├── PetFun.Domain/
│   ├── PetFun.Application/
│   ├── PetFun.Persistence/
│   ├── PetFun.Infrastructure/
│   └── PetFun.WebApi/
└── frontend/
    ├── src/
    │   ├── components/
    │   ├── pages/
    │   ├── services/
    │   └── context/
    └── public/


 Komanda

Ad Rol Aysun Şirəlizadə Team Lead & Backend Developer   // Sübhanə Əlibəyova Frontend Developer

Komanda adı: Vertex

Universitet: Azərbaycan Texniki Universiteti (ATU)

İxtisas: İnformasiya Təhlükəsizliyi


 Lisenziya

Bu layihə akademik məqsədlər üçün hazırlanmışdır.