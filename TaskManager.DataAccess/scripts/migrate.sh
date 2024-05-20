dotnet ef migrations add init  -s .\TaskManager.API\ -p .\TaskManager.DataAccess\
 
 
dotnet ef database update -s .\TaskManager.API\ -p .\TaskManager.DataAccess\


