dotnet ef migrations add init --context UserDbContext  -s .\TaskManager.API\ -p .\User.DataAccess\
 
dotnet ef database update  --context UserDbContext -s .\TaskManager.API\ -p .\User.DataAccess\


