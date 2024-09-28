POS System API

This is the backend API for the POS System. It handles business logic and data storage using .NET Core and follows Clean Architecture principles. The system uses manual authentication and authorization, including password hashing using the Bcrypt package.

Project Structure
- **PosSystem.Core**: Contains the core entities, enums, exceptions, and other base classes.
- **PosSystem.Infrastructure**: Responsible for external communication, primarily with SQL Server using Entity Framework Core.
- **PosSystem.Application**: Holds the business logic, services, and interfaces.
- **PosSystem.API**: The presentation layer, which includes controllers for managing API requests and responses.

Main Features
- Product API: Manage products, categories, and units.
- Invoice API: Create and update invoices.
- Authentication and Authorization: JWT-based token authentication.
  - Passwords are securely hashed using the Bcrypt package.
- Role-based Access Control: Admin and Employee roles.
- Database Migrations: Handle database migrations using Entity Framework Core.

Technologies Used
- .NET Core: The main framework for the API.
- Entity Framework Core: For database interactions.
- SQL Server: As the primary database.
- Bcrypt: For password hashing.
- JWT: For token-based authentication.

Setup Instructions

1. Navigate to the API directory:
   cd PosSystem.API

2. Restore the required packages:
   dotnet restore

3. Update the database using Entity Framework Core:
   dotnet ef database update

4. Run the API:
   dotnet run

5. The API will be available at:
   https://localhost:7168
