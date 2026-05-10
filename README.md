# 🐾 PetCareMini — Backend API
 
PetCareMini is a full-featured RESTful API for a pet shop,
pet care, and veterinary appointment management platform.
Built with ASP.NET Core Web API and PostgreSQL.

---

# Tech Stack

- ASP.NET Core 8 Web API  
- Entity Framework Core 8  
- PostgreSQL (Npgsql)  
- JWT Bearer Authentication  
- Role-Based Authorization (Admin / User)  
- Layered Architecture  
- Swagger / OpenAPI  
- Global Exception Middleware  
- Multilanguage Support (AZ / EN)  
- Pagination & Filtering  
- Veterinary Appointment System  
- Pet Management System  

---

# Project Structure

```txt
PetCareMini/
├── PetCareMini.Domain        # Entities, Enums, BaseEntity
├── PetCareMini.Application   # DTOs, Interfaces, Common
├── PetCareMini.Persistence   # DbContext, Repositories, Services, Seed
└── PetCareMini.WebApi        # Controllers, Middlewares, Program.cs
```

---

# Features

- Product management  
- Cart & Wishlist system  
- Coupon & discount support  
- Order & checkout flow  
- Product review system  
- Veterinary appointment booking  
- Pet profile management  
- Admin dashboard statistics  
- Global exception handling  
- Pagination, filtering & sorting  
- Role-based authorization  
- Multilanguage API support  

---

# Getting Started

## 1. Clone the repository

```bash
git clone https://github.com/username/PetCareMini.git
```

---

## 2. Update appsettings.json

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=PetCareMiniDb;
                        Username=postgres;Password=yourpassword"
},
"Jwt": {
  "Key": "your-secret-key-min-32-chars",
  "Issuer": "PetCareMini",
  "Audience": "PetCareMiniUsers",
  "ExpireMinutes": 60
}
```

---

## 3. Apply migrations

```bash
dotnet ef database update --project PetCareMini.Persistence \
  --startup-project PetCareMini.WebApi
```

---

## 4. Run the project

```bash
dotnet run --project PetCareMini.WebApi
```

---

## 5. Open Swagger

```txt
https://localhost:{port}/swagger
```

---

# Seed Data (auto on startup)

```txt
Admin:
admin@petcare.az / Admin123!

User:
user@petcare.az / User123!
```

## Coupons

- WELCOME10 (10%)  
- SUMMER20 (20%)  
- PET50 (50%)  

## Seed Includes

- 15 Products  
- 5 Categories  
- 5 Services  
- 4 Veterinarians  
- 5 FAQs  

---

# API Endpoints

# Authentication

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| POST | /api/auth/register | Public | Register new user |
| POST | /api/auth/login | Public | Login and get JWT token |

---

# Products

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| GET | /api/products | Public | List with filter/sort/pagination |
| GET | /api/products/{id} | Public | Single product |
| GET | /api/products/{id}/recommended | Public | Recommended products |
| POST | /api/products | Admin | Create product |
| PUT | /api/products/{id} | Admin | Update product |
| DELETE | /api/products/{id} | Admin | Soft delete product |

---

# Categories, Services, Veterinarians, FAQ

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| GET | /api/productcategories | Public | List categories |
| POST | /api/productcategories | Admin | Create category |
| PUT | /api/productcategories/{id} | Admin | Update category |
| DELETE | /api/productcategories/{id} | Admin | Delete category |
| GET | /api/services | Public | List services |
| GET | /api/veterinarians | Public | List veterinarians |
| GET | /api/faqs | Public | List FAQs |

---

# Pets

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| GET | /api/pets | User | Get my pets |
| GET | /api/pets/{id} | User | Get single pet |
| POST | /api/pets | User | Create pet |
| PUT | /api/pets/{id} | User | Update pet |
| DELETE | /api/pets/{id} | User | Delete pet |

---

# Appointments

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| GET | /api/appointments/my | User | Get my appointments |
| POST | /api/appointments | User | Create appointment |
| GET | /api/appointments | Admin | Get all appointments |
| PATCH | /api/appointments/{id}/status | Admin | Update appointment status |

---

# Cart & Wishlist

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| GET | /api/cart | User | Get cart (?lang=az) |
| POST | /api/cart/{productId} | User | Add to cart |
| PUT | /api/cart/{productId} | User | Update quantity |
| DELETE | /api/cart/{productId} | User | Remove from cart |
| GET | /api/wishlist | User | Get wishlist (?lang=az) |
| POST | /api/wishlist/{productId} | User | Add to wishlist |
| DELETE | /api/wishlist/{productId} | User | Remove from wishlist |

---

# Orders, Reviews, Coupons

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| POST | /api/orders/checkout | User | Checkout (cart → order) |
| GET | /api/orders/my-orders | User | My orders |
| POST | /api/review | User | Write review |
| GET | /api/review/product/{productId} | Public | Get product reviews |
| POST | /api/coupon/apply | User | Apply coupon |
| POST | /api/coupon | Admin | Create coupon |
| PATCH | /api/coupon/{id}/deactivate | Admin | Deactivate coupon |

---

# Admin

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| GET | /api/admin/statistics | Admin | Dashboard statistics |

---

# Appointment Flow

1. User creates pet profile  
2. User selects veterinarian and service  
3. User books appointment for selected pet  
4. Backend validates:
   - pet ownership
   - veterinarian availability
   - future date
   - booking conflicts
5. Appointment is created with Pending status  
6. Admin approves or updates appointment status  

---

# Error Response Format

All errors return consistent JSON:

```json
{
  "statusCode": 404,
  "message": "Product with id 99 not found."
}
```

## Status Codes

```txt
200 OK           → Success
400 Bad Request  → Validation error
401 Unauthorized → Missing/invalid token
403 Forbidden    → Admin required
404 Not Found    → Resource not found
409 Conflict     → Duplicate/already exists
500 Server Error → Unexpected error
```

---

# Multilanguage Support

All variable content supports AZ and EN:

```txt
GET /api/products?lang=az
GET /api/products?lang=en
GET /api/cart?lang=en
GET /api/wishlist?lang=az
GET /api/faqs?lang=en
```

## Supported Modules

- Products  
- Categories  
- Services  
- Cart  
- Wishlist  
- FAQ  

---

# Checkout Flow

1. User logs in and gets JWT token  
2. Adds products to cart  
3. (Optional) Applies coupon  

```txt
POST /api/coupon/apply
```

4. Checkout request

```txt
POST /api/orders/checkout?couponCode=WELCOME10
```

5. Backend converts cart items into order  
6. Cart is cleared  

## Response includes

- orderId  
- originalPrice  
- discountAmount  
- finalPrice  

---

# Architecture Highlights

- Layered Architecture  
- Repository Pattern  
- DTO-based responses  
- Global Exception Middleware  
- Role-based authorization  
- Ownership validation  
- Appointment conflict prevention  
- Soft delete support  
- Seed data system  
- Professional pagination system  

---

# License

MIT License — feel free to use for educational purposes.