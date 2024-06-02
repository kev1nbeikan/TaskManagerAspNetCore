using System.Net;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets.ElasticSearch;
using NLog.Targets.OpenSearch;
using NLog.Web;
using OpenSearch.Net;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.OpenSearch;
using TaskManager.DataAccess;
using User.Infastructure;
using WebApplication3.Extentions;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


builder.AddLogger();

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


builder.Host.UseNLog();
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

var logger2 = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

logger2.Debug("Debug");
logger2.Info("Info");
logger2.Warn("Warn");
logger2.Error("Error");
logger2.Fatal("Fatal");

app.Run();