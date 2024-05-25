using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Services;
using TaskManager.Core.Abstractions;
using TaskManager.DataAccess;
using TaskManager.DataAccess.Repositories;
using User.Application.Service;
using User.DataAccess;
using User.DataAccess.Repositories;
using User.Infastructure;
using Users.Core.Abstractions;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskRepository, TasksRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

Console.WriteLine(builder.Configuration.GetSection("JwtOptions:SecretKey").Value);

builder.Services.AddDbContext<TaskManagerDbContext>(
    options => { options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(TaskManagerDbContext))); }
);

builder.Services.AddDbContext<UserDbContext>(
    options => { options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(TaskManagerDbContext))); }
);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();