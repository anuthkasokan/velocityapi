# Video Games Catalogue API

A demonstration project showcasing **Clean Architecture**, **Entity Framework Core**, and **ASP.NET Core Minimal APIs** for a RESTful video games catalogue service.

## 🏗️ Architecture Overview

This project follows **Clean Architecture** (Onion Architecture) principles with clear separation of concerns:

```
┌─────────────────────────────────────────────────────┐
│                    API Layer                        │
│  • Minimal APIs (REST endpoints)                    │
│  • Dependency Injection configuration               │
│  • HTTP pipeline middleware                         │
└──────────────────┬──────────────────────────────────┘
                   │ depends on
┌──────────────────▼──────────────────────────────────┐
│              Application Layer                      │
│  • Service interfaces (IGameService)                │
│  • DTOs (Data Transfer Objects)                     │
│  • Business logic contracts                         │
└──────────────────┬──────────────────────────────────┘
                   │ depends on
┌──────────────────▼──────────────────────────────────┐
│            Infrastructure Layer                     │
│  • EF Core DbContext                                │
│  • Service implementations                          │
│  • Database migrations                              │
│  • External dependencies                            │
└──────────────────┬──────────────────────────────────┘
                   │ depends on
┌──────────────────▼──────────────────────────────────┐
│                Domain Layer                         │
│  • Entities (Game, Developer, Publisher, etc.)      │
│  • Value Objects                                    │
│  • Domain logic (none in this simple CRUD project)  │
└─────────────────────────────────────────────────────┘
```

### Key Architectural Decisions

- **Dependency Inversion**: API and Infrastructure depend on Application abstractions
- **Separation of Concerns**: Each layer has a single, well-defined responsibility
- **Testability**: Interfaces allow easy mocking for unit tests
- **Domain-Driven Design**: Core business entities isolated from infrastructure concerns

## 🚀 Technologies Used

- **.NET 10** - Latest C# features (primary constructors, collection expressions)
- **ASP.NET Core Minimal APIs** - Lightweight alternative to MVC controllers
- **Entity Framework Core 10** - ORM with Code-First migrations
- **SQL Server** - Relational database (configurable via connection string)
- **NUnit** - Unit testing framework
- **Moq** - Mocking library for tests
- **EF Core InMemory** - In-memory database provider for testing

## 📁 Project Structure

```
velocityapi/
├── API/                           # Presentation Layer
│   ├── Endpoints/                 # Minimal API endpoint definitions
│   │   └── VideoGamesEndpoints.cs # RESTful CRUD operations
│   ├── Program.cs                 # Application entry point & DI setup
│   └── appsettings.json           # Configuration (connection strings)
│
├── Application/                   # Application Layer
│   ├── DTOs/                      # Data Transfer Objects
│   │   ├── GameDto.cs             # Response model
│   │   ├── AddGameRequest.cs      # Create operation model
│   │   └── UpdateGameRequest.cs   # Update operation model
│   └── Services/
│       └── IGameService.cs        # Service contract (interface)
│
├── Infrastructure/                # Infrastructure Layer
│   ├── Persistence/
│   │   ├── VideoGamesDbContext.cs # EF Core DbContext
│   │   └── Migrations/            # Code-first database migrations
│   └── Services/
│       └── GameService.cs         # IGameService implementation
│
├── Domain/                        # Domain Layer
│   └── Entity/
│       ├── Game.cs                # Core entity
│       ├── Developer.cs           # Related entity
│       ├── Publisher.cs           # Related entity
│       ├── Genre.cs               # Reference data
│       └── Platform.cs            # Reference data
│
└── Tests/                         # Test Projects
    ├── API.Tests/                 # API endpoint tests (Moq)
    ├── Infrastructure.Tests/      # Service layer tests (EF InMemory)
    ├── Application.Tests/         # (Empty - no business logic to test)
    └── Domain.Tests/              # (Empty - POCOs with no logic)
```

## 🔌 API Endpoints

| Method | Endpoint      | Description          | Response       |
| ------ | ------------- | -------------------- | -------------- |
| GET    | `/games`      | Get all games        | 200 OK         |
| GET    | `/games/{id}` | Get game by ID       | 200 OK / 404   |
| POST   | `/games`      | Create new game      | 201 Created    |
| PUT    | `/games/{id}` | Update existing game | 204 No Content |
| DELETE | `/games/{id}` | Delete game          | 204 No Content |

### Example Request/Response

**POST** `/games`

```json
{
  "title": "The Witcher 3: Wild Hunt",
  "description": "Open-world RPG",
  "releaseDate": "2015-05-19T00:00:00Z",
  "publisherId": 1,
  "developerId": 2
}
```

**Response** `201 Created`

```json
{
  "id": 42
}
```

**Location** header: `/games/42`

## 🛠️ Setup & Running

### Prerequisites

- .NET 10 SDK
- SQL Server (local or Docker)

### Database Setup

1. Update connection string in `API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost,1433;Database=VideoGames;User Id=sa;Password=YourPassword;TrustServerCertificate=True"
  }
}
```

2. Apply migrations to create the database:

```bash
dotnet ef database update --project Infrastructure --startup-project API
```

### Run the API

```bash
dotnet run --project API
```

The API will be available at `https://localhost:5068` (or configured port).

### OpenAPI/Swagger

In development mode, access the API documentation at:

- OpenAPI spec: `https://localhost:5001/openapi/v1.json`

## 🧪 Testing

### Run All Tests

```bash
dotnet test --nologo
```

### Run Specific Test Project

```bash
dotnet test Tests/Infrastructure.Tests/Infrastructure.Tests.csproj --nologo
dotnet test Tests/API.Tests/API.Tests.csproj --nologo
```

### Test Coverage

- **Infrastructure.Tests** (9 tests): Tests `GameService` CRUD operations using EF Core InMemory provider
- **API.Tests** (6 tests): Tests endpoint logic and service integration using Moq

## 🎯 Design Patterns & Best Practices

### Implemented Patterns

1. **Repository Pattern** (implicit via EF Core DbContext)
   - DbContext acts as a Unit of Work
   - DbSet<T> acts as repositories

2. **Dependency Injection**
   - Constructor injection for services
   - Parameter injection in Minimal API endpoints

3. **DTO Pattern**
   - Separates domain entities from API contracts
   - Prevents over-posting attacks
   - Allows different representations for different operations

4. **CQRS (lightweight)**
   - Separate DTOs for commands (Add/Update) and queries (GameDto)

5. **Extension Methods**
   - Endpoint organization via `MapVideoGamesEndpoints()`
   - Keeps `Program.cs` clean and maintainable

### Code Quality Features

- **Nullable Reference Types** enabled for null safety
- **Primary Constructors** (C# 14) for concise DI
- **Record types** for immutable DTOs
- **Async/await** throughout for scalability
- **Idempotent operations** (PUT/DELETE return 204 even if resource doesn't exist)
- **RESTful conventions** (proper HTTP status codes)

## 📊 Database Schema

```sql
Games
  ├─ Id (PK, int, identity)
  ├─ Title (nvarchar(200), required)
  ├─ Description (nvarchar(max), nullable)
  ├─ ReleaseDate (datetime2, nullable)
  ├─ PublisherId (FK, nullable) -> Publishers.Id
  └─ DeveloperId (FK, nullable) -> Developers.Id

Publishers
  ├─ Id (PK)
  └─ Name (nvarchar(200), required)

Developers
  ├─ Id (PK)
  └─ Name (nvarchar(200), required)

Genres (reference data)
  ├─ Id (PK)
  └─ Name (nvarchar(100), required)

Platforms (reference data)
  ├─ Id (PK)
  └─ Name (nvarchar(100), required)
```

## 🔧 Configuration

### Connection Strings

- **Development**: `appsettings.json`
- **Production**: Environment variables or Azure Key Vault

### Entity Framework Migrations

Create new migration:

```bash
dotnet ef migrations add MigrationName --project Infrastructure --startup-project API
```

Revert last migration:

```bash
dotnet ef migrations remove --project Infrastructure --startup-project API
```

## 📝 Notes for Reviewers

This project demonstrates:

✅ **Clean Architecture** with proper layer separation  
✅ **SOLID Principles** (especially Dependency Inversion)  
✅ **Modern .NET features** (Minimal APIs, primary constructors, records)  
✅ **Entity Framework Core** (Code-First, Migrations, Eager Loading)  
✅ **RESTful API design** (proper HTTP semantics)  
✅ **Unit Testing** (NUnit, Moq, EF InMemory)  
✅ **Code Documentation** (XML comments, inline comments)

The codebase is intentionally well-commented to demonstrate understanding of architectural decisions and design patterns.

---

**Author**: Anuth Asokan  
**Date**: February 2026
**Purpose**: Technical Assessment
