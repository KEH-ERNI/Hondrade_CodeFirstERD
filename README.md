# Government System Documentation

## Overview

This project is a Government System developed using ASP.NET Core with a Code-First approach. The system manages entities such as Employee, Citizen, Application, Service, Contact, and Department. The project includes a RESTful API to perform CRUD operations on these entities.

## Table of Contents

1. [Project Setup](#project-setup)
2. [Entity-Relationship Diagram (ERD)](#entity-relationship-diagram-erd)
3. [Database Context](#database-context)


## Project Setup

### Prerequisites

- .NET SDK 5.0 or later
- SQL Server

### Installation

1. Clone the repository:

    ```sh
    git clone https://github.com/KEH-ERNI/Hondrade_CodeFirstERD
    cd hondrade_codefirsterd
    ```

2. Restore the dependencies:

    ```sh
    dotnet restore
    ```

3. Update the database connection string in `appsettings.json`:

    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=your_server;Database=GovernmentSystem;User Id=your_user;Password=your_password;"
    }
    ```

4. Apply migrations and update the database:

    ```sh
    dotnet ef database update
    ```

5. Run the application:

    ```sh
    dotnet run
    ```

## Entity-Relationship Diagram (ERD)
![ERD](https://github.com/KEH-ERNI/Hondrade_CodeFirstERD/blob/master/Hondrade_CodeFirstERD/wwwroot/erd-image.png)
   
## Database Context
```sh
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Citizen> Citizens { get; set; }

    public DbSet<Department> Departments { get; set; }

    public DbSet<Service> Services { get; set; }

    public DbSet<Application> Applications { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>()
            .HasOne(a => a.Citizen)
            .WithMany(c => c.Applications)
            .HasForeignKey(a => a.CitizenID);

        modelBuilder.Entity<Application>()
            .HasOne(a => a.Service)
            .WithMany(c => c.Applications)
            .HasForeignKey(a => a.ServiceID);

        modelBuilder.Entity<Service>()
            .HasOne(a => a.Department)
            .WithMany(c => c.Services)
            .HasForeignKey(a => a.DepID);

        modelBuilder.Entity<Employee>()
            .HasOne(a => a.Department)
            .WithMany(c => c.Employees)
            .HasForeignKey(a => a.DepID);

        modelBuilder.Entity<Contact>()
            .HasOne(a => a.Employee)
            .WithMany(c => c.Contacts)
            .HasForeignKey(a => a.EmpID);

        modelBuilder.Entity<Contact>()
            .HasOne(a => a.Citizen)
            .WithMany(c => c.Contacts)
            .HasForeignKey(a => a.CitizenID);
    }
}
```
