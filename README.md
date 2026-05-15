# 🐾 PetCareMini — Backend API

> A full-featured RESTful API for a pet shop, pet care services, and veterinary appointment management platform. Built with ASP.NET Core 8 and PostgreSQL.





## 👥 Team

| Role | GitHub |
|---|---|
| 🔧 Backend Developer | [@AysunShirelizade1](https://github.com/AysunShirelizade1) |
| 🎨 Frontend Developer | [@Subhane00](https://github.com/Subhane00) |

---

## 🚀 Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 8 Web API |
| ORM | Entity Framework Core 8 |
| Database | PostgreSQL (Npgsql) |
| Authentication | JWT Bearer + Refresh Token |
| Authorization | Role-Based (Admin / User) |
| Architecture | Layered Architecture (4 layers) |
| Validation | FluentValidation 11 |
| Logging | Serilog (Console + File) |
| Documentation | Swagger / OpenAPI |
| Language Support | Azerbaijani (AZ) + English (EN) |

---

## 🏗️ Project Structure

```
PetCareMini/
├── PetCareMini.Domain          # Entities, Enums, BaseEntity
├── PetCareMini.Application     # DTOs, Interfaces, Validators, Common
├── PetCareMini.Persistence     # DbContext, Repositories, Services, Seed
└── PetCareMini.WebApi          # Controllers, Middlewares, Program.cs
```

---

## ✨ Features

- 🔐 JWT Authentication with Refresh Token support
- 👮 Role-based authorization (Admin / User)
- 🛍️ Product management with filter, sort, search & pagination
- 🛒 Cart & Wishlist system
- 🎫 Coupon & discount support
- 📦 Order & checkout flow
- ⭐ Product review system
- 🐾 Pet profile management
- 📅 Veterinary appointment booking
- 📊 Admin dashboard statistics
- 🌐 Multilanguage API support (AZ / EN)
- 🔍 Global exception handling middleware
- 📝 Serilog file & console logging
- ✅ FluentValidation on all input DTOs
- 🌱 Automatic database seeding

---

## ⚙️ Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/AysunShirelizade1/PetCareMini.git
cd PetCareMini
```

### 2. Update `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=PetCareDb;Username=postgres;Password=yourpassword"
  },
  "Jwt": {
    "Key": "your-secret-key-min-32-characters",
    "Issuer": "PetCareMiniApi",
    "Audience": "PetCareMiniClient"
  }
}
```

### 3. Apply migrations

```bash
dotnet ef database update \
  --project PetCareMini.Persistence \
  --startup-project PetCareMini.WebApi
```

### 4. Run the project

```bash
dotnet run --project PetCareMini.WebApi
```

### 5. Open Swagger

```
https://localhost:{port}/swagger
```

---

## 🌱 Seed Data

The database is seeded automatically on startup.

### Default Accounts

| Role | Email | Password |
|---|---|---|
| Admin | admin@petcare.az | Admin123! |
| User | user@petcare.az | User123! |

### Coupon Codes

| Code | Discount |
|---|---|
| WELCOME10 | 10% |
| SUMMER20 | 20% |
| PET50 | 50% |

### Seeded Content

- 15 Products
- 5 Categories
- 5 Services
- 4 Veterinarians
- 5 FAQs
- 2 Users (Admin + Test)
- 3 Coupons

---

## 📡 API Endpoints

### 🔑 Authentication

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| POST | `/api/auth/register` | Public | Register new user |
| POST | `/api/auth/login` | Public | Login and get JWT token |
| POST | `/api/auth/refresh-token` | Public | Get new access token |
| GET | `/api/auth/me` | User | Get current user info |

### 🛍️ Products

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| GET | `/api/products` | Public | List with filter/sort/pagination |
| GET | `/api/products/{id}` | Public | Single product |
| GET | `/api/products/{id}/recommended` | Public | Recommended products |
| POST | `/api/products` | Admin | Create product |
| PUT | `/api/products/{id}` | Admin | Update product |
| DELETE | `/api/products/{id}` | Admin | Soft delete product |

### 📂 Categories

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| GET | `/api/productcategories` | Public | List categories |
| POST | `/api/productcategories` | Admin | Create category |
| PUT | `/api/productcategories/{id}` | Admin | Update category |
| DELETE | `/api/productcategories/{id}` | Admin | Delete category |

### 🐾 Pets

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| GET | `/api/pets` | User | Get my pets |
| GET | `/api/pets/{id}` | User | Get single pet |
| POST | `/api/pets` | User | Create pet |
| PUT | `/api/pets/{id}` | User | Update pet |
| DELETE | `/api/pets/{id}` | User | Delete pet |

### 📅 Appointments

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| GET | `/api/appointments/my` | User | Get my appointments |
| POST | `/api/appointments` | User | Book appointment |
| GET | `/api/appointments` | Admin | Get all appointments |
| PATCH | `/api/appointments/{id}/status` | Admin | Update appointment status |

### 🛒 Cart & Wishlist

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| GET | `/api/cart` | User | Get cart (`?lang=az`) |
| POST | `/api/cart/{productId}` | User | Add to cart |
| PUT | `/api/cart/{productId}` | User | Update quantity |
| DELETE | `/api/cart/{productId}` | User | Remove from cart |
| GET | `/api/wishlist` | User | Get wishlist (`?lang=az`) |
| POST | `/api/wishlist/{productId}` | User | Add to wishlist |
| DELETE | `/api/wishlist/{productId}` | User | Remove from wishlist |

### 📦 Orders

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| POST | `/api/orders/checkout` | User | Checkout (cart → order) |
| GET | `/api/orders/my-orders` | User | My orders |

### ⭐ Reviews

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| POST | `/api/review` | User | Write a review |
| GET | `/api/review/product/{productId}` | Public | Get product reviews |

### 🎫 Coupons

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| POST | `/api/coupon/apply` | User | Apply coupon |
| POST | `/api/coupon` | Admin | Create coupon |
| PATCH | `/api/coupon/{id}/deactivate` | Admin | Deactivate coupon |

### 🩺 Veterinarians & Services

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| GET | `/api/veterinarians` | Public | List veterinarians |
| GET | `/api/services` | Public | List services |
| GET | `/api/faqs` | Public | List FAQs (`?lang=az`) |

### 📊 Admin

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| GET | `/api/admin/statistics` | Admin | Dashboard statistics |

---

## 🌐 Multilanguage Support

All variable content supports AZ and EN via `?lang=` query parameter:

```
GET /api/products?lang=az
GET /api/products?lang=en
GET /api/cart?lang=en
GET /api/wishlist?lang=az
GET /api/faqs?lang=en
GET /api/appointments/my?lang=en
```

Supported modules: Products, Categories, Services, Cart, Wishlist, FAQ, Appointments

---

## 🔄 Refresh Token Flow

```
1. POST /api/auth/login
   → Returns: { token, refreshToken, ... }

2. Store both tokens on frontend

3. When token expires (60 min):
   POST /api/auth/refresh-token
   Body: { "refreshToken": "..." }
   → Returns: new { token, refreshToken }

4. Refresh token expires after 7 days
   → User must login again
```

---

## 📅 Appointment Booking Flow

```
1. User creates pet profile       → POST /api/pets
2. User selects veterinarian      → GET /api/veterinarians
3. User selects service           → GET /api/services
4. User books appointment         → POST /api/appointments
5. Backend validates:
   - Pet ownership
   - Veterinarian availability
   - Future date
   - No booking conflicts
6. Appointment created (Pending)
7. Admin approves or updates      → PATCH /api/appointments/{id}/status
```

---

## 🛒 Checkout Flow

```
1. User logs in                   → POST /api/auth/login
2. Adds products to cart          → POST /api/cart/{productId}
3. (Optional) Apply coupon        → POST /api/coupon/apply
4. Checkout                       → POST /api/orders/checkout?couponCode=WELCOME10
5. Backend converts cart → order
6. Cart is cleared automatically

Response includes:
  - orderId
  - originalPrice
  - discountAmount
  - finalPrice
  - couponUsed
```

---

## ❌ Error Response Format

All errors return consistent JSON:

```json
{
  "statusCode": 404,
  "message": "Product with id 99 not found."
}
```

| Status Code | Meaning |
|---|---|
| 200 | Success |
| 201 | Created |
| 400 | Validation error |
| 401 | Missing or invalid token |
| 403 | Access forbidden (wrong role or ownership) |
| 404 | Resource not found |
| 409 | Conflict (duplicate, already exists) |
| 500 | Unexpected server error |

---

## 📝 Logging

Serilog is configured to log to both Console and File:

```
Logs/
└── log-20260510.txt   ← daily rotating log files (kept for 7 days)
```

Sample log output:
```
[INF] HTTP GET /api/products responded 200 in 45ms
[INF] HTTP POST /api/auth/login responded 200 in 120ms
[ERR] Unhandled exception: Product with id 99 not found.
```

---

## 🏛️ Architecture Highlights

- **Layered Architecture** — Domain / Application / Persistence / WebApi
- **Repository Pattern** — abstracted data access
- **DTO-based responses** — clean separation of concerns
- **Global Exception Middleware** — consistent error handling
- **Role-based authorization** — Admin and User roles
- **Ownership validation** — users can only access their own resources
- **Soft delete support** — deleted products remain in order history
- **FluentValidation** — all input DTOs validated automatically
- **Refresh Token** — seamless session management
- **Seed data system** — auto-populated on startup

---

## 📄 License

MIT License — free to use for educational purposes.
