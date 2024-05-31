using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using TaskManager.Application.Services;
using TaskManager.Core.Abstractions;
using TaskManager.DataAccess;
using TaskManager.DataAccess.Repositories;
using TaskManager.Infastructure;
using User.Application.Service;
using User.Infastructure;
using Users.Core.Abstractions;
using WebApplication3.Extentions;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost";
    options.InstanceName = "local";
});


// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddControllersWithViews();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication();
builder.Services.AddApiAuthentication(builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>());


builder.Services.AddDi();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddDbContext<TaskManagerDbContext>(
    options => { options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(TaskManagerDbContext))); }
);


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();


app.MapControllers();

app.Run();