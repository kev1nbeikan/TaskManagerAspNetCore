using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using TaskManager.DataAccess;
using User.Infastructure;
using WebApplication3.Extentions;

var builder = WebApplication.CreateBuilder(args);


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<TaskManagerDbContext>(
    options => { options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(TaskManagerDbContext))); }
);


builder.AddLogger();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost";
    options.InstanceName = "local";
});


builder.Services.AddControllers();

builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication();
builder.Services.AddApiAuthentication(builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>());


builder.Services.AddDi();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));



builder.Host.UseNLog();
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();

app.Run();