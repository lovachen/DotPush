using Consul;
using DotPush.IMServie;
using DotPush.IMServie.Models;
using DotPush.Web.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var cfg = builder.Services.ConfigureStartupConfig<ServiceInfoConfig>(builder.Configuration.GetSection("ServiceInfoConfig"));
builder.Services.AddSingleton(cfg);

builder.Services.AddSingleton<ConsulRegisterService>();
builder.Services.AddSingleton<IConsulClient>(new ConsulClient(x => x.Address = new Uri(cfg.ConsulAddress)));
builder.Services.AddHostedService<ConsulRegisterService>();
// Add services to the container.
var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
