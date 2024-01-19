# Sports-Backend
> Sports Backend is an API Services for the sports website that provide JWT Security Authentication and Sports API Services for Tourments, Teams, Players, Matchs, etc...


[![](http://img.shields.io/badge/framework-.NetCore-blue.svg?style=flat)](https://maven.apache.org/)
[![](http://img.shields.io/badge/language-CShap-brightgreen.svg?color=darkgreen)](https://www.oracle.com/java/technologies/downloads/)
![](https://img.shields.io/github/last-commit/kareem983/Sports-Backend)

# About
The API Database Design Entities Divided Into (Tourment, Team, TourmentTeam, Player, Match)
- Tourment (ID, Name, Description, Logo)
- Team (ID, Name, Description, Logo, Website, FoundationDate)
- TourmentTeam (TourmentId, TeamId)
- Player (Id, FirstName, LastName, BirthDate. TeamId)
- Match (ID, MatchDate, Result, TourmentId, HomeTeamId, HostedTeamId)

# Getting into the Database Design
<details>
  <summary>ERD</summary>
<p>

![Sports Database ERD](https://github.com/kareem983/CRUD_Practice/assets/52586356/871a9dbc-c3f5-4c7e-93c9-689a789e4ce1)

</p>
</details>

# Prerequisites
- [1] Install Visual Studio (recommended 2022).
- [2] Install .Net Core 7.
- [3] Install SQL Server Management System (recommended 2022).

# Before you start
- [1] Open the project on Visual Studio.
- [2] Open Package Manager Console.
- [3] Write the database migration command to create the database.
- [4] make sure that you select default project on the Package Manager Console `Infrastructure`.
- [5] the command is `update-database`. so the database is created with named SportsDatabase.
- [6] open the appsettings.json and change the port number to your project port number on the ValidIssuer string.


# API Services
<details>
  <summary>API Services Sample</summary>
<p>

![Screenshot 2024-01-20 003721](https://github.com/kareem983/CRUD_Practice/assets/52586356/f4656d35-ba66-4cef-97d8-71692f2483f9)
  
</p>
</details>

- [1] User Authentication Using Identity Model Customization.
- [2] User Security Authentication Using JWT Bearer Token.
- [3] User Authentication (Register, Login, Logout).
- [4] Tourment API Services for Admin (Add, Update, Delete) and For User (GetAll, GetById, GetTeamsByTourmentID, GetMatchsByTourmentID).
- [5] Team API Services for Admin (Add, Update, Delete) and For User (GetAll, GetById, GetPlayersByTeamID).
- [6] Player API Services for Admin (Add, Update, Delete) and For User (GetAll, GetById).
- [7] Match API Services for Admin (Add, Update, Delete) and For User (GetAll, GetById).

# Used Concepts
- [1] 3 Tier Architecture Pattern.
- [2] Repository Design Pattern.
- [3] JWT Tokenization.
- [4] Identity Model Customization.
- [5] Dependency Injection.
- [6] Auto Mapper.
- [7] Database Design & Migration.
- [8] API Testing (Postman).



# References/Resources

- [.Net Guide](https://visualstudio.microsoft.com/vs/features/net-development/)

- [Database Guide](https://learn.microsoft.com/en-us/sql/?view=sql-server-ver16)

- [AutoMapper Guide](https://code-maze.com/automapper-net-core/)

- [Dependency Injection Guide](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-8.0)
  
  
