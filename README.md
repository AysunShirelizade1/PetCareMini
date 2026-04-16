# 🐾 PetCare API

![.NET](https://img.shields.io/badge/.NET-8-blue)
![License](https://img.shields.io/badge/license-MIT-green)
![Build](https://img.shields.io/badge/build-passing-brightgreen)

---

## 📌 About the Project

PetCare API is a backend system for managing pet care services.
It allows users to manage pets, book appointments with veterinarians, and explore available services.

This project is built with **ASP.NET Core Web API** using **Onion Architecture**.

---

## 🚀 Features

* 🔐 User authentication (JWT)
* 🐶 Pet management
* 👨‍⚕️ Veterinarian listing
* 📅 Appointment booking system
* 🛠 Service management
* 📨 Contact messages

---

## 🧱 Architecture

This project follows **Onion Architecture**:

* **PetCare.API** → Controllers (Presentation Layer)
* **PetCare.Application** → Business logic
* **PetCare.Domain** → Entities & Enums
* **PetCare.Persistence** → Database & EF Core

---

## 🛠 Technologies

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT Authentication
* Swagger

---

## ⚙️ Installation

### 1. Clone repository

```
git clone https://github.com/your-username/PetCare.git
```

### 2. Open in Visual Studio

### 3. Configure database

Edit `appsettings.json`:

```
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=PetCareDb;Trusted_Connection=True;"
}
```

### 4. Run migrations

```
Update-Database
```

### 5. Run project

---

## 📖 API Documentation

After running the project, open:

```
https://localhost:xxxx/swagger
```

You can test all endpoints from Swagger UI.

---

## 🔑 Example Endpoints

```
POST   /api/auth/register
POST   /api/auth/login
GET    /api/veterinarians
POST   /api/appointments
```

---

## 📸 Preview (Swagger)

(Add screenshot here later)

---

## ⚠️ Notes

* SQL Server must be running
* Check connection string before migration
* JWT token required for protected routes

---

## 👩‍💻 Author

* Aysun Shirezade

---

## ⭐ Support

If you like this project, give it a ⭐ on GitHub!

---
