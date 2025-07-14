# 🏘️ Community Reporter API

An **ASP.NET Core Web API** that allows users to report community issues such as potholes, electricity faults, crimes, and more — with secure authentication and role-based access control.

---

## 🚀 Features

- ✅ User Registration & Login with **JWT Authentication**
- ✅ **Role-based Authorization** (e.g., Admin-only routes)
- ✅ Report **CRUD Operations** (Create, Read, Update, Delete)
- ✅ Integrated with **PostgreSQL**
- ✅ **Swagger Documentation**
- ✅ Secure **Password Hashing** with built-in salting (ASP.NET Core Identity)
- ✅ Implements **Security Headers** to prevent XSS, CSRF, and clickjacking
- ✅ Follows **OWASP Top 10** secure coding principles

---

## 📦 Technologies Used

- ASP.NET Core 7
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- Swagger / OpenAPI
- C#
- .NET Security Headers Middleware

---

## 📂 Project Structure

CommunityReporterApp/
│
├── Controllers/ # API Controllers (Auth, Reports)
├── Models/ # Entity Models (AppUser, Report, etc.)
├── Data/ # DbContext and Seeder
├── SeedData/ # Initial dummy data
├── Middleware/ # Custom middlewares (e.g., SecurityHeaders)
├── Program.cs # Main entry point with middleware config
├── appsettings.json # Configuration settings
└── ...

yaml
Copy
Edit

---

## 🔐 Authentication & Authorization

- Uses **JWT Bearer Tokens** for authentication.
- Endpoints are protected based on **roles** (`Admin`, `User`, etc).
- Admins can access all reports and user data; users can only manage their own.

**Example** header for protected endpoints:
Authorization: Bearer <your-jwt-token>

markdown
Copy
Edit

---

## 🔒 Security Enhancements

- 🛡️ **Security Headers** added via custom middleware:
  - `Content-Security-Policy`
  - `X-Frame-Options`
  - `X-Content-Type-Options`
  - `Permissions-Policy`
- 🧠 Prevents **Cross-Site Scripting (XSS)** and **CSRF**
- 🔐 Implements **secure password hashing** (with salting via ASP.NET Identity)

---

## 📬 API Endpoints

### 🔑 Auth

| Method | Endpoint               | Description             |
|--------|------------------------|-------------------------|
| POST   | `/api/auth/register`   | Register a new user     |
| POST   | `/api/auth/login`      | Login and receive a JWT |
| GET    | `/api/auth/debug-token`| Check your token info   |
| GET    | `/api/auth/debug-auth` | Check your role/auth    |

### 📄 Reports

| Method | Endpoint                        | Description               |
|--------|----------------------------------|---------------------------|
| GET    | `/api/reports`                   | Get all reports (Admin)   |
| GET    | `/api/reports/owner/{owner}`     | Reports by specific owner |
| GET    | `/api/reports/{id}`              | Get report by ID          |
| POST   | `/api/reports`                   | Create a report           |
| PUT    | `/api/reports/{id}`              | Update a report           |
| DELETE | `/api/reports/{id}`              | Delete a report           |

---

## ⚙️ Getting Started

1. **Clone the repo**
```bash
git clone https://github.com/Sim001-debug/CommunityReporterApp.git
cd CommunityReporterApp
Set up the database

Ensure PostgreSQL is running

Update your appsettings.json with your connection string

Run the application

bash
Copy
Edit
dotnet build
dotnet run
Open Swagger UI
<img width="1902" height="956" alt="image" src="https://github.com/user-attachments/assets/d8c806a1-186d-4bcc-bb4a-2d4487f91df8" />

Navigate to: https://localhost:<port>/swagger

🧠 Learning Goals
This project was built with a focus on:

Secure API development

Practical authentication & authorization

Backend best practices

Deploy-ready .NET apps

Real-world role-based access

👨🏽‍💻 Author
Simbongile Dyi
Junior Software Engineer | Full Stack Developer
📧 Simbongile.Dyi99@gmail.com
https://www.linkedin.com/in/simbongile-dyi-288227249/

