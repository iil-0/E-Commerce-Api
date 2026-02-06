# E-Commerce REST API

<div align="center">

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=flat-square&logo=c-sharp&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=flat-square&logo=postgresql&logoColor=white)
![Entity Framework](https://img.shields.io/badge/EF%20Core-9.0-purple?style=flat-square)
![JWT](https://img.shields.io/badge/JWT-000000?style=flat-square&logo=jsonwebtokens&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=flat-square&logo=docker&logoColor=white)

**Production-ready RESTful API for E-Commerce Platforms**

</div>

---

## Overview

This project is a fully functional e-commerce backend API built with **.NET 9** and **ASP.NET Core**. It demonstrates enterprise-level software development practices including clean architecture, secure authentication, and scalable design patterns.

The API provides complete functionality for managing products, categories, orders, and users with JWT-based authentication and role-based authorization.

---

## Technical Highlights

- **Clean Architecture** with layered separation of concerns
- **RESTful API Design** following industry standards
- **JWT Authentication** with role-based access control (Admin/User)
- **Entity Framework Core 9** with Code-First migrations
- **Soft Delete Implementation** across all entities using Global Query Filters
- **Global Exception Handling** with standardized API responses
- **Structured Logging** using Serilog with file rotation
- **Dual API Implementation** - Controller-based and Minimal API endpoints
- **Docker Support** for containerized deployment
- **Swagger/OpenAPI** documentation with JWT integration

---

## Technology Stack

| Layer | Technology |
|-------|------------|
| Framework | .NET 9, ASP.NET Core |
| Language | C# 13 |
| ORM | Entity Framework Core 9 |
| Database | PostgreSQL |
| Authentication | JWT Bearer Tokens |
| Logging | Serilog (Console + Rolling File) |
| Documentation | Swagger / OpenAPI |
| Containerization | Docker |

---

## Architecture

The application follows a layered architecture pattern:

```
Presentation Layer
    └── Controllers, Minimal API Endpoints, Middlewares
            │
Business Layer
    └── Services, Interfaces, DTOs
            │
Data Layer
    └── DbContext, Entity Models, Migrations
            │
Database
    └── PostgreSQL
```

### Design Patterns Implemented

| Pattern | Usage |
|---------|-------|
| Dependency Injection | Service registration and resolution |
| Repository Pattern | Data access abstraction via services |
| DTO Pattern | API contract separation from domain models |
| Middleware Pattern | Cross-cutting concerns (exception handling, logging) |
| Global Query Filters | Automatic soft delete filtering |

---

## API Endpoints

### Authentication
| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| POST | `/api/auth/login` | User authentication | Public |
| POST | `/api/auth/register` | User registration | Public |

### Products
| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| GET | `/api/products` | Retrieve all products | Required |
| GET | `/api/products/{id}` | Retrieve product by ID | Required |
| POST | `/api/products` | Create new product | Required |
| PUT | `/api/products/{id}` | Update existing product | Required |
| DELETE | `/api/products/{id}` | Delete product (soft delete) | Required |

### Categories
| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| GET | `/api/categories` | Retrieve all categories | Required |
| GET | `/api/categories/{id}` | Retrieve category by ID | Required |
| POST | `/api/categories` | Create new category | Required |
| PUT | `/api/categories/{id}` | Update existing category | Required |
| DELETE | `/api/categories/{id}` | Delete category (soft delete) | Required |

### Orders
| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| GET | `/api/orders` | Retrieve all orders | Required |
| GET | `/api/orders/{id}` | Retrieve order by ID | Required |
| POST | `/api/orders` | Create new order | Required |
| PUT | `/api/orders/{id}` | Update order status | Required |
| DELETE | `/api/orders/{id}` | Cancel order (soft delete) | Required |

### Users
| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| GET | `/api/users` | Retrieve all users | Required |
| GET | `/api/users/{id}` | Retrieve user by ID | Required |
| PUT | `/api/users/{id}` | Update user profile | Required |
| DELETE | `/api/users/{id}` | Delete user (soft delete) | Required |

### Minimal API Endpoints
Alternative lightweight endpoints available at `/api/minimal/categories` and `/api/minimal/products`.

---

## Database Schema

### Entity Relationships

```
Users (1) ─────── (N) Orders (1) ─────── (N) OrderItems (N) ─────── (1) Products (N) ─────── (1) Categories
```

### Entities

| Entity | Description |
|--------|-------------|
| User | User accounts with Admin/User roles |
| Category | Product categorization |
| Product | Product catalog with pricing and inventory |
| Order | Customer orders with status and payment tracking |
| OrderItem | Individual line items within orders |

### Enumerations

| Enum | Values |
|------|--------|
| OrderStatus | Pending, Processing, Shipped, Delivered, Cancelled |
| PaymentMethod | CreditCard, DebitCard, Cash, BankTransfer |
| UserRole | User, Admin |

---

## API Response Format

All endpoints return responses in a standardized format:

```json
{
  "success": true,
  "message": "Operation completed successfully",
  "data": { }
}
```

Error responses follow the same structure with `success: false` and relevant error message.

---

## Getting Started

### Prerequisites

- .NET 9 SDK
- PostgreSQL 14+
- Docker (optional)

### Installation

```bash
# Clone repository
git clone https://github.com/iil-0/E-Commerce-Api.git
cd E-Commerce-Api

# Configure database connection in appsettings.json

# Apply migrations
dotnet ef database update

# Run application
dotnet run
```

### Access

| Resource | URL |
|----------|-----|
| Swagger UI | http://localhost:5185/swagger |
| API Base URL | http://localhost:5185/api |

### Test Credentials

| Role | Email | Password |
|------|-------|----------|
| Admin | admin@ecommerce.com | admin123 |
| User | user@ecommerce.com | user123 |

---

## Docker Deployment

```bash
# Build image
docker build -t ecommerce-api .

# Run container
docker run -d -p 5185:80 ecommerce-api
```

---

## Project Structure

```
├── Common/              # Shared utilities (ApiResponse)
├── Context/             # DbContext and database seeder
├── Controllers/         # API controllers
├── DTOs/                # Data transfer objects
├── Endpoints/           # Minimal API endpoints
├── Enums/               # Enumerations
├── Interfaces/          # Service interfaces
├── Middlewares/         # Custom middleware components
├── Migrations/          # EF Core migrations
├── Models/              # Entity models
├── Services/            # Business logic implementation
├── appsettings.json     # Application configuration
├── Dockerfile           # Container configuration
└── Program.cs           # Application entry point
```

---

## Author

Developed by [iil-0](https://github.com/iil-0)
