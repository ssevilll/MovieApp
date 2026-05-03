# MovieApp
A .NET 8 console application for managing movies and directors, built with a clean 3-layer architecture using Entity Framework Core and AutoMapper.
## Table of Contents
- [Overview](#overview)
- [Architecture](#architecture)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Database Setup](#database-setup)
  - [Build and Run](#build-and-run)
- [Features](#features)
---
## Overview
MovieApp is a .NET 8 console application that demonstrates a layered architecture pattern for CRUD operations on a movie catalog. It manages two core entities — **Movies** and **Directors** — with a one-to-many relationship between them.
## Architecture
The solution is organized into three projects following a standard n-tier pattern:
| Layer | Project | Responsibility |
|---|---|---|
| Presentation | `MovieApp.Presentation` | Entry point; composes the DI container and runs the application |
| Business | `MoveiApp.Business` | Services, DTOs, AutoMapper profiles, and business logic |
| Data Access | `MovieApp.DataAccess` | EF Core DbContext, entity models, repository pattern, and migrations |
## Tech Stack
- **.NET 8**
- **Entity Framework Core** – ORM and database migrations
- **SQL Server (LocalDB / SQL Express)** – relational database
- **AutoMapper 12** – object-to-object mapping between entities and DTOs
- **Microsoft.Extensions.DependencyInjection** – dependency injection container
## Project Structure
```
MovieApp/
├── MovieApp.sln
│
├── MovieApp.Presentation/          # Console application entry point
│   └── Program.cs
│
├── MoveiApp.Business/              # Business logic layer
│   ├── DTOs/
│   │   ├── DirectorDtos/           # Director create/update/return DTOs
│   │   └── MovieDtos/              # Movie create/update/return DTOs
│   ├── Interfaces/
│   │   ├── IDirectorService.cs
│   │   └── IMovieService.cs
│   ├── Profiles/
│   │   └── MapperProfile.cs        # AutoMapper configuration
│   └── Services/
│       ├── DirectorService.cs
│       └── MovieService.cs
│
└── MovieApp.DataAccess/            # Data access layer
    ├── Configurations/             # EF Core Fluent API configurations
    ├── Concretes/
    │   └── Repository.cs           # Generic repository implementation
    ├── Data/
    │   ├── MovieAppDbContext.cs
    │   └── Migrations/
    ├── Interfaces/
    │   └── IRepository.cs          # Generic repository interface
    └── Models/
        ├── BaseEntity.cs
        ├── Director.cs
        └── Movie.cs
```
## Getting Started
### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server Express (default instance `.\SQLEXPRESS`) or another SQL Server instance
### Database Setup
1. Update the connection string in `MovieApp.DataAccess/Data/MovieAppDbContext.cs` if your SQL Server instance differs from the default (`.\SQLEXPRESS`):
   ```csharp
   optionsBuilder.UseSqlServer(
       "Server=.\\SQLEXPRESS;Database=MovieAppDb;Trusted_Connection=True;TrustServerCertificate=True;");
   ```
2. Apply the existing migrations to create the database:
   ```bash
   cd MovieApp.DataAccess
   dotnet ef database update
   ```
### Build and Run
```bash
# Restore dependencies and build the solution
dotnet build MovieApp.sln
# Run the console application
dotnet run --project MovieApp.Presentation
```
## Features
### Movies
- **GetAllMoviesAsync** – retrieve all movies with their director info
- **GetMovieByIdAsync** – fetch a single movie by ID
- **GetMoviesByDirectorAsync** – list all movies for a given director
- **SearchMoviesAsync** – search movies by title or description keyword
- **AddMovieAsync** – add a new movie (validates uniqueness and director existence)
- **UpdateMovieAsync** – update movie details
- **DeleteMovieAsync** – remove a movie
### Directors
- **GetAllDirectorsAsync** – retrieve all directors with their associated movies
- **GetDirectorByIdAsync** – fetch a single director by ID (includes movie list)
- **GetAllDirectorsSearchAsync** – search directors by name
- **AddDirectorAsync** – add a new director (validates name uniqueness)
- **UpdateDirectorAsync** – update director details
- **DeleteDirectorAsync** – remove a director
