dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet tool install --global dotnet-ef
dotnet ef --version
dotnet ef migrations add InitialCreate

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design


dotnet ef migrations add InitialCreate
dotnet ef database update


dotnet add package MySql.EntityFrameworkCore
