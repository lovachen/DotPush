using Consul;
using DotPush.IMServie;
using DotPush.IMServie.Models;
using DotPush.Web.Core;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

var builder = WebApplication.CreateBuilder(args);

//builder.Host.ConfigureAppConfiguration(build =>
//{
//    build.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json");
//});

// Add services to the container.
builder.Services.AddControllers();

var cfg = builder.Services.ConfigureStartupConfig<ServiceInfoConfig>(builder.Configuration.GetSection("ServiceInfoConfig"));
builder.Services.AddSingleton(cfg);

builder.Services.AddSingleton<ConsulRegisterService>();
builder.Services.AddSingleton<IConsulClient>(new ConsulClient(x => x.Address = new Uri(cfg.ConsulAddress)));
builder.Services.AddHostedService<ConsulRegisterService>();

//builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();
//var urls = builder.Configuration.GetValue<string>("urls");

//builder.WebHost.UseUrls(builder.Configuration.GetValue<string>("urls"));

// Add services to the container.
var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
