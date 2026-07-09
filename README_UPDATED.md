# Docklly API - AI-Powered Legal Document Management System

## Overview

Docklly is a comprehensive legal document management platform built with ASP.NET Core that leverages artificial intelligence to streamline document creation, management, compliance checking, and secure storage.

### Key Features

✅ **AI-Assisted Document Drafting** - Generate legal documents with intelligent suggestions
✅ **Document Version Control** - Track all changes with complete history
✅ **Compliance Automation** - GDPR, HIPAA, SOX compliance checking
✅ **Risk Assessment** - AI-powered contract risk analysis
✅ **Role-Based Access Control** - Fine-grained permission management
✅ **Audit Logging** - Complete compliance audit trails
✅ **RESTful API** - Comprehensive API with Swagger documentation

---

## Technology Stack

- **Framework**: ASP.NET Core 8.0
- **Language**: C#
- **Database**: Microsoft SQL Server
- **ORM**: Entity Framework Core
- **Containerization**: Docker & Docker Compose
- **API Documentation**: Swagger/OpenAPI
- **Authentication**: JWT Tokens

---

## Quick Start

### Prerequisites

- .NET 8.0 SDK or higher
- Docker & Docker Compose
- SQL Server (via Docker)

### Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/raman-kumar-kheti/Docklly.git
   cd Docklly
   ```

2. **Start the SQL Server database**
   ```bash
   docker-compose up -d
   ```

3. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

4. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access the API**
   - Swagger UI: `http://localhost:5058/swagger`
   - Health Check: `http://localhost:5058/health`
   - API Base URL: `http://localhost:5058/api`

---

## API Endpoints

### Users Management
```
GET    /api/users
GET    /api/users/{id}
POST   /api/users
PUT    /api/users/{id}
DELETE /api/users/{id}
```

### Document Management
```
GET    /api/documents
GET    /api/documents/{id}
POST   /api/documents
PUT    /api/documents/{id}
DELETE /api/documents/{id}
GET    /api/documents/search?query=
POST   /api/documents/{id}/versions
```

### AI Services
```
POST   /api/ai/draft
POST   /api/ai/analyze/{id}
POST   /api/ai/risk-assessment/{id}
GET    /api/ai/clauses?type=
```

### Compliance
```
POST   /api/compliance/check/{id}
GET    /api/compliance/checks/{id}
GET    /api/compliance/score/{id}
```

### Access Control
```
POST   /api/documents/{id}/access
GET    /api/documents/{id}/access
DELETE /api/documents/{id}/access/{userId}
```

---

## Database Models

### Core Entities

**Users**
- User authentication and role management
- Role: Admin, Attorney, Paralegal, Reviewer, Viewer

**Document**
- Legal document storage with metadata
- Status: Draft, Review, Approved, Archived
- Version control and change tracking

**DocumentVersion**
- Complete version history of documents
- Change tracking with author attribution

**ComplianceCheck**
- Regulatory compliance validation
- Supports GDPR, HIPAA, SOX

**AiAnalysis**
- AI-generated insights and suggestions
- Risk assessment, clause analysis, etc.

**DocumentAccess**
- Role-based access control
- Fine-grained permission management

**AuditLog**
- Complete audit trail for compliance
- Tracks all user actions

---

## Entity Framework Migrations

### Create New Migration
```bash
dotnet ef migrations add <MigrationName>
```

Example:
```bash
dotnet ef migrations add AddDocumentModels
```

### Apply Migrations
```bash
dotnet ef database update
```

### List All Migrations
```bash
dotnet ef migrations list
```

### Remove Last Migration
```bash
dotnet ef migrations remove
```

### Drop Database
```bash
dotnet ef database drop
```

---

## Configuration

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=docklly;User Id=sa;Password=docklly!123;Encrypt=True;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key-at-least-32-chars",
    "ExpirationMinutes": 60
  },
  "AiSettings": {
    "Provider": "OpenAI",
    "ApiKey": "your-api-key",
    "Model": "gpt-4"
  }
}
```

---

## Project Structure

```
Docklly/
├── Controllers/          # API Endpoints
├── DTOs/                 # Data Transfer Objects
├── Models/               # Entity Models
│   ├── Users.cs
│   ├── Document.cs
│   ├── DocumentVersion.cs
│   ├── ComplianceCheck.cs
│   ├── AiAnalysis.cs
│   ├── DocumentAccess.cs
│   └── AuditLog.cs
├── Services/             # Business Logic
│   ├── UsersServices.cs
│   ├── DocumentService.cs
│   ├── AiDocumentService.cs
│   └── ComplianceService.cs
├── Database/             # EF Core Context
├── Middleware/           # Custom Middleware
│   ├── HttpCustomMiddleware.cs
│   └── ErrorHandlingMiddleware.cs
├── Extension/            # Extension Methods
│   ├── ServiceExtensions.cs
│   └── HealthCheckExtensions.cs
├── Program.cs            # Application Startup
├── appsettings.json      # Configuration
├── Docklly.csproj        # Project File
└── docker-compose.yml    # Docker Configuration
```

---

## Docker Deployment

### Docker Compose (Development)

```bash
docker-compose up -d
```

Stops containers:
```bash
docker-compose down
```

---

## Security Features

🔒 **Encryption**
- TLS 1.2+ for data in transit
- AES-256 for data at rest

🔐 **Authentication**
- JWT token-based authentication
- Refresh token mechanism

👥 **Authorization**
- Role-Based Access Control (RBAC)
- Fine-grained permissions

📋 **Audit Trail**
- Complete logging of all actions
- Compliance audit logs

---

## Logging

The application uses structured logging via Serilog. Logs are output to:
- Console (Development)
- File (Production)
- Application Insights (Optional)

---

## Support & Documentation

- 📚 **Full Documentation**: See `PAGES.md`
- 🔗 **API Docs**: Visit `/swagger` endpoint
- 💬 **Issues**: Report on GitHub
- 📧 **Email**: support@docklly.com

---

## License

This project is licensed under the MIT License.

---

## Contributors

- **Raman Kumar Kheti** - Project Lead

---

**Last Updated**: July 2026
**Version**: 1.0.0
