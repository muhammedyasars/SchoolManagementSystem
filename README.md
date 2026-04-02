# SchoolManagementSystem
Professional SMS backend built with .NET 8, EF Core, and Clean Architecture.

## Roles
- **SuperAdmin**: Full control over schools.
- **Teacher**: Manage classes and students.
- **Student**: View class and teacher info.

## Features
- JWT Authentication
- Role-based Authorization
- Student Points/Rewards System
- Student Swap Logic
- Data Seeding

## Detailed Changes & Modified Files

### 1. Database & Domain Layer Updates
- **[User.cs](file:///d:/G3%20Interactive/SchoolManagementSystem/SchoolManagement.Domain/Entities/User.cs)**: Added `FirstName`, `LastName` fields. Centralized user identity for both Teachers and Students.
- **[Student.cs](file:///d:/G3%20Interactive/SchoolManagementSystem/SchoolManagement.Domain/Entities/Student.cs)**: Removed duplicate `FirstName`, `LastName`, and `Email`. Linked to `User` table via `UserId`.
- **[Class.cs](file:///d:/G3%20Interactive/SchoolManagementSystem/SchoolManagement.Domain/Entities/Class.cs)**: Removed redundant Teacher name/email fields. Added `TeacherUserId` to link to `User` table.

### 2. Infrastructure & Logic Updates
- **[ApplicationDbContext.cs](file:///d:/G3%20Interactive/SchoolManagementSystem/SchoolManagement.Infrastructure/Data/ApplicationDbContext.cs)**: 
    - Updated relationships (User-Student One-to-One, Class-Teacher).
    - Added Seeding for **SuperAdmin**, **Teachers**, and **Students**.
- **[StudentService.cs](file:///d:/G3%20Interactive/SchoolManagementSystem/SchoolManagement.Infrastructure/Services/StudentService.cs)**: Updated CRUD logic to handle linked `User` accounts. Added `ActivateDeactivate` logic.
- **[ClassService.cs](file:///d:/G3%20Interactive/SchoolManagementSystem/SchoolManagement.Infrastructure/Services/ClassService.cs)**: Updated to fetch Teacher details from the `User` table.

### 3. API & Security Updates
- **[SchoolController.cs](file:///d:/G3%20Interactive/SchoolManagementSystem/SchoolManagement.Api/Controllers/SchoolController.cs)**: Restricted School creation/modification to **SuperAdmin** only.
- **[ClassController.cs](file:///d:/G3%20Interactive/SchoolManagementSystem/SchoolManagement.Api/Controllers/ClassController.cs)** & **[StudentController.cs](file:///d:/G3%20Interactive/SchoolManagementSystem/SchoolManagement.Api/Controllers/StudentController.cs)**: Updated permissions for **Teacher** and **SuperAdmin** roles.
- **[appsettings.json](file:///d:/G3%20Interactive/SchoolManagementSystem/SchoolManagement.Api/appsettings.json)**: Updated SQL Server connection string for `YAAZOOZ\SQLEXPRESS`.

### 4. Application Layer (DTOs)
- **[StudentDto.cs](file:///d:/G3%20Interactive/SchoolManagementSystem/SchoolManagement.Application/DTOs/StudentDto.cs)** & **[ClassDto.cs](file:///d:/G3%20Interactive/SchoolManagementSystem/SchoolManagement.Application/DTOs/ClassDto.cs)**: Updated properties to reflect the normalized database structure.

### 5. New Files Added
- **[README.md](file:///d:/G3%20Interactive/SchoolManagementSystem/README.md)**: Project documentation and change log.
- **Migrations**: Added 3 new migrations for Seeding, Schema Normalization, and SuperAdmin setup.
