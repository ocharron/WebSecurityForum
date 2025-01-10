# Web Security Forum - Demonstration of Secure Coding Practices

[![en](https://img.shields.io/badge/lang-en-red.svg)](https://github.com/ocharron/WebSecurityForum/blob/master/README.md)
[![fr](https://img.shields.io/badge/lang-fr-blue.svg)](https://github.com/ocharron/WebSecurityForum/blob/master/README_fr.md)  

The **Web Security Forum** project is a forum application designed to demonstrate secure coding practices and protections against common web vulnerabilities. This project showcases advanced web security techniques using ASP.NET Core MVC.

---

## Technologies Used

- **Framework**: ASP.NET Core (.NET 8.0)  
- **Database**: Microsoft SQL Server 2022  
- **ORM**: Entity Framework Core  
- **Authentication**: ASP.NET Core Identity  

---

## Installation

To install and run Web Security Forum on your local machine, follow these steps:

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)  
- [Visual Studio](https://visualstudio.microsoft.com/downloads/) or your preferred text editor  
- [Microsoft SQL Server 2022](https://www.microsoft.com/en-ca/sql-server/sql-server-downloads) (or later version)  

### Instructions

1. **Clone the Repository**
   ```bash
   git clone https://github.com/ocharron/WebSecurityForum.git
   ```

2. **Database Configuration**
   - Create a new database in SQL Server.
   - Update the connection string in the `appsettings.json` file with your database details.
   - Execute migrations to set up the database schema:
     ```bash
     dotnet ef migrations add <MigrationName>
     dotnet ef database update
     ```

3. **Compilation and Execution**
   - Open the project in Visual Studio or use the command line.
   - Run the following command to restore dependencies:
     ```bash
     dotnet restore
     ```
   - Then, run the application:
     ```bash
     dotnet run
     ```

---

## Key Features

1. **Secure Authentication**: Implements ASP.NET Identity for secure user authentication.
2. **Role-Based Authorization**: Manage user roles and permissions to protect sensitive features.
3. **Protection Against Vulnerabilities**: Safeguards against SQL Injection, XSS, and CSRF.
4. **Logging and Monitoring**: Includes security logging for monitoring login attempts and suspicious activities.
5. **Error Handling**: Customized exception handling to prevent sensitive information exposure.

---

## Author

This project was developed by [Olivier Charron](https://github.com/ocharron).