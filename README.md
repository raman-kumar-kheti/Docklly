# Docklly API

Welcome to the Docklly project! Here is a cheat sheet of useful commands for running the application and managing your Entity Framework (EF) Core database migrations.

## Docker Database Setup

Before running the application, make sure your SQL Server database container is running:

```bash
# Start the database in the background
docker-compose up -d

# Stop the database
docker-compose down
```

## .NET Core Commands

### Build the Application
Compiles the application and checks for any syntax errors.
```bash
dotnet build
```

### Run the Application
Starts the API server. By default, it will be available at `http://localhost:5058/`.
```bash
dotnet run
```

---

## Entity Framework (EF) Core Commands

*Note: If you haven't installed the EF Core CLI tools yet, run this first: `dotnet tool install --global dotnet-ef`*

### 1. Create a New Migration
When you make changes to your Database Models (e.g., adding a new property to `Users.cs`), you need to create a migration.
```bash
dotnet ef migrations add <MigrationName>
```
*Example: `dotnet ef migrations add InitialCreate` or `dotnet ef migrations add AddEmailToUsers`*

### 2. Update the Database
After creating a migration, you must apply it so the SQL Server database schema is updated.
```bash
dotnet ef database update
```

### 3. List Migrations
View a list of all migrations that currently exist in your project.
```bash
dotnet ef migrations list
```

### 4. Remove the Last Migration
If you added a migration but realize you made a mistake (and you **haven't** run `database update` yet), you can easily remove the last one.
```bash
dotnet ef migrations remove
```

### 5. Drop the Database
*(Use with caution!)* Completely deletes the database. Useful if you want to start fresh.
```bash
dotnet ef database drop
```
