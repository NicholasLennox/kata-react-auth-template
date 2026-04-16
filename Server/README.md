# Backend - .NET Auth API

A JWT-based authentication API built with ASP.NET Core 10, Entity Framework Core, and SQL Server.

This is a provided backend. You do not need to modify it - your job is to build the React frontend that talks to it.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/products/docker-desktop) (for SQL Server)

## Getting started

### 1. Start SQL Server

If you do not have SQL Server running locally, start one with Docker:

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong!Passw0rd" -p 1433:1433 -d mcr.microsoft.com/mssql/server
```

### 2. Configure the connection string

Open `appsettings.json`. The connection string is already set to match the Docker container above:

```json
"ConnectionStrings": {
  "Default": "Server=localhost,1433;Database=ReactAuth;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True"
}
```

If you are using a different SQL Server instance, update this string to match.

### 3. Create the database

```bash
dotnet ef database update
```

This runs the existing migrations and creates the database. You do not need to create any migrations yourself.

### 4. Run the API

```bash
dotnet run
```

Check the terminal output for the port. It will look something like:

```
Now listening on: http://localhost:5102
```

Make a note of this port - you will need it when configuring your React frontend.



## Resetting the database

If you want to start fresh:

```bash
dotnet ef database drop
dotnet ef database update
```



## Testing the API

An `.http` file is included in the project. Open it in VS Code (with the REST Client extension) or Rider to test the endpoints directly.

Before testing the protected `/api/me` endpoint, register or log in first and paste the returned token into the `@token` variable at the top of the file.

### Endpoints

```
POST /api/register   { email, password }          → { token, email }
POST /api/login      { email, password }           → { token, email }
GET  /api/me         Authorization: Bearer <token> → { id, email }
```



## Configuration reference

All configuration lives in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "Default": "..."
  },
  "JwtSettings": {
    "Secret": "super-secret-key-replace-in-production-min-32-chars",
    "Issuer": "react-auth",
    "Audience": "react-auth",
    "ExpireMinutes": 60
  }
}
```

`JwtSettings` controls how tokens are generated. The secret must be at least 32 characters. In a real application this would never be committed to source control - for this exercise it is hardcoded to keep things simple.