dotnet ef migrations add init --context TaskManagerDbContext -s .\TaskManager.API\ -p .\TaskManager.DataAccess\
 
 
dotnet ef database update --context TaskManagerDbContext -s .\TaskManager.API\ -p .\TaskManager.DataAccess\


