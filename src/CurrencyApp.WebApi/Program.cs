using CurrencyApp.Application;
using CurrencyApp.Infrastructure;
using CurrencyApp.WebApi;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddWebApi();

builder.Services.AddProblemDetails();
builder.Services.AddRouting(o => o.LowercaseUrls = true);
builder.Services.AddCors(o => o.AddDefaultPolicy(p 
    => p.WithOrigins(builder.Configuration["FrontendOrigin"])
    .AllowAnyHeader()
    .AllowAnyMethod()));

// Build
var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseCors();

app.UseWebApi(app.Environment.IsDevelopment());
app.MapControllers();

app.Run();
