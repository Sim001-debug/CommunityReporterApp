# 🏘️ Community Reporter API

An ASP.NET Core Web API that allows users to report community issues such as potholes, electricity faults, crimes, and more. Built with JWT authentication and PostgreSQL.

---

## 🚀 Features

- ✅ User Registration & Login with JWT Authentication
- ✅ Report CRUD (Create, Read, Update, Delete)
- ✅ Role-based Authorization (e.g., Admin)
- ✅ Swagger Documentation
- ✅ PostgreSQL Integration
- ✅ Secure Password Hashing

---

## 📦 Technologies Used

ASP.NET Core 7
Entity Framework Core
PostgreSQL
JWT Authentication
Swagger / OpenAPI
C#

---

## 📂 Project Structure

CommunityReporterApp/
│
├── Controllers/ # API Controllers (Auth, Reports)
├── Models/ # Entity Models (AppUser, Report, etc.)
├── Data/ # DbContext and Seeder
├── SeedData/ # Initial dummy data
├── Program.cs # Main entry point with middleware config
├── appsettings.json # Configuration settings
└── ...


---

## 🔐 Authentication

This API uses **JWT** for secure login. After login, you’ll receive a token that must be added in the `Authorization` header (Bearer Token) for secured endpoints.

---

## 📬 API Endpoints

### 🔑 Auth

- `POST /api/auth/register` — Register a new user  
- `POST /api/auth/login` — Login and receive a JWT

### 📄 Reports

- `GET /api/reports` — Get all reports
- `GET /api/reports/{id}` — Get report by ID
- `POST /api/reports` — Create a new report
- `PUT /api/reports/{id}` — Update report
- `DELETE /api/reports/{id}` — Delete report

---

## ⚙️ Getting Started

1. **Clone the repo**
   ```bash
   git clone https://github.com/yourusername/community-reporter-api.git
   cd community-reporter-api

2. **Set u the database**
1. Ensure the PostSQL is running
1. Update appsettings.json with your connection string

3. **Run the application**
1. dotnet build
1. dotnet run

4. **Visist Swagger**
|Method  |  Endpoint                     | Description             |
| ------ | ----------------------------- | ----------------------- |
| POST   | `/api/auth/register`          | Register a new user     |
| POST   | `/api/auth/login`             | Login and get JWT token |
| GET    | `/api/reports`                | Get all reports         |
| POST   | `/api/reports`                | Create a report         |
| DELETE | `/api/reports/{id}`           | Delete a report         |
| GET    | `/api/reports/owner/{owner}`  | Get reports by owner    |
| GET    | `/api/reports/category/{cat}` | Get reports by category |

🙌 Author
Simbongile Dyi
Software Engineer | Full Stack :)

https://www.linkedin.com/in/simbongile-dyi-288227249/
Simbongile.Dyi99@gmail.com