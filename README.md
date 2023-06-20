# Vehicle Management System
Web application that allows users to manage vehicle makes and models. It provides functionality to view, create, update, and delete vehicle makes and models.

## FEATURES
### Vehicle Makes
- View a list of all vehicle makes
- View details of a specific vehicle make
- Create a new vehicle make
- Edit an existing vehicle make
- Delete a vehicle make

### Vehicle Models
- View a list of all vehicle models
- View details of a specific vehicle model
- Create a new vehicle model
- Edit an existing vehicle model
- Delete a vehicle model

## TECHNOLOGIES USED
- ASP.NET Core MVC: The web application framework used to build the application
- Entity Framework Core: The object-relational mapping (ORM) framework used for database access
- Microsoft SQL Server: The relational database management system used to store the data (Managed using SQL Server Management Studio (SSMS))
- HTML/CSS: The markup and styling languages used to create the user interface

## HOW TO RUN
- Clone or download the repository: https://github.com/cicekl/MinimalisticVehicleManagement
- Restore the database:
  - Download the database backup file (VehicleDB.bak) from the repository
  - Open SQL Server Management Studio (SSMS) and connect to your SQL Server instance
  - Right-click on the Databases node in the Object Explorer and choose Restore Database
  - Select the "Device" option and browse for the backup file (VehicleDB.bak)
  - Restore the database.
- Open the project in Visual Studio
- Modify the connection string:
  - In the project, locate the appsettings.json file
  - Find the connection string and update it with your database credentials
- Build the project to restore NuGet packages and compile the application
- Launch the application in your web browser
- Navigate to the "Vehicle Makes" section to manage vehicle makes
- Navigate to the "Vehicle Models" section to manage vehicle models
- Use the actions such as create, edit and delete vehicle makes and models
