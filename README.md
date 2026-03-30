# 💎 DigitalAssetsApp

A **Full-Stack .NET 9 Web API** project simulating a **digital asset platform** with Web3-style interactions.  
Designed for technical assessments, showcasing **Clean Architecture**, **EF Core**, **FluentValidation**, **Swagger**, and **Serilog logging**.

---

## 🏗 Project Structure
DigitalAssetsApp/
│
├─ API/ # ASP.NET Core Web API (Startup)
├─ Application/ # Application layer: DTOs, Services, Interfaces, Validators
├─ Domain/ # Domain entities
├─ Infrastructure/ # EF Core DbContext, Repositories, Services implementations
└─ README.md



---

## ✨ Key Features

- **💰 Assets Management**: List and view assets  
- **🔄 Transactions**: Create transactions between wallets  
- **👛 Wallets**: Query balances  
- **⛓ Simulated Blockchain**: Transaction hashes & status  
- **✅ Validation**: FluentValidation with centralized middleware  
- **📊 Logging**: Serilog structured logs (Console + File, daily rolling, last 7 days)  
- **📖 API Docs**: Swagger UI for interactive exploration  

---

## 🛠 Tech Stack

| Layer | Technology |
|-------|------------|
| Backend | .NET 9 Web API |
| ORM | EF Core 9 + SQL Server |
| Validation | FluentValidation |
| Logging | Serilog |
| API Docs | Swagger |
| Frontend | Optional HTML/JS for demo |

---

## ⚙️ Getting Started

### 1. Clone the repo

git clone https://github.com/<your-username>/DigitalAssetsApp.git
cd DigitalAssetsApp: Optional basic HTML/JS (for demo)

---
### 2. Configure SQL Server

Update appsettings.json connection string:

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=DigitalAssetsDb;Trusted_Connection=True;"
}
---
### 3. Apply Migrations
cd Infrastructure
dotnet ef database update --startup-project ../API
---
### 4. Run the App
cd API
dotnet run

Swagger UI: https://localhost:<port>/index.html
---
### 🔧 Architecture Decisions

Clean Architecture: Decouples layers, easy to maintain
FluentValidation: Centralized, clean input validation
Middleware: Global exception handling
Serilog Logging: Structured logging for production-grade observability
EF Core + SQL: Real database for persistence, migrations support
Swagger: Interactive API documentation

---
### ✅ Improvements & Next Steps
Add unit & integration tests for services and controllers
Implement JWT authentication for secure API
Expand Web3 simulation with event tracking
Add frontend SPA with React/Blazor
Add Docker support for deployment
---
### 📂 Folder Structure Visual
API/
├─ Controllers/
├─ Middleware/
Application/
├─ DTOs/
├─ Services/
├─ Validators/
Infrastructure/
├─ Data/
├─ Services/
Domain/
├─ Entities/
README.md

---
### 🔑 Key Learnings

Structured logging is essential for production apps
Clean architecture ensures maintainability and scalability
FluentValidation + middleware gives professional-grade input validation
EF Core migrations allow fast iteration with SQL database
