using TaskManager.Application.Services;
using TaskManager.Core.Abstractions;
using TaskManager.DataAccess;
using TaskManager.DataAccess.Repositories;
using TaskManager.Infastructure;
using User.Application.Service;
using User.Infastructure;
using Users.Core.Abstractions;

namespace WebApplication3.Extentions;

public static class DiAddExtentions
{
    public static void AddDi(this IServiceCollection services)
    {
        services.AddScoped<ICache, Cashe>();

        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<ITaskService, TaskService>();

        services.AddScoped<ITaskRepository>(sp =>
        {
            var baseRepo = new TasksRepository(sp.GetRequiredService<TaskManagerDbContext>());
            var cache = sp.GetRequiredService<ICache>();
            return new TasksRepositoryWithCaching(baseRepo, cache);
        });

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IFileService, FilesService>();
        services.AddScoped<IFileSaver, FileSaver>();
    }
}