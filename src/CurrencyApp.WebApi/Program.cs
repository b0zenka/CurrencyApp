using CurrencyApp.Application.Abstractions;
using CurrencyApp.Application.Features.Exchange.GetRates;
using CurrencyApp.Infrastructure.Configuration;
using CurrencyApp.Infrastructure.Http;
using CurrencyApp.Infrastructure.Providers;
using CurrencyApp.Infrastructure.Providers.Nbp;
using CurrencyApp.Infrastructure.Time;
using CurrencyApp.WebApi.Middleware;
using CurrencyApp.WebApi.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Controllers + FluentValidation
builder.Services
    .AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<GetRatesRequestValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Options configuration
builder.Services.Configure<ApiAddressesOptions>(
    builder.Configuration.GetSection(ApiAddressesOptions.SectionName));
builder.Services.Configure<UiFormattingOptions>(
    builder.Configuration.GetSection(UiFormattingOptions.SectionName));
builder.Services.Configure<NbpOptions>(
    builder.Configuration.GetSection("Apis:Nbp"));

// Middleware
builder.Services.AddTransient<ExceptionMiddleware>();

// Providers (Keyed Services) + Factory
builder.Services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
builder.Services.AddTransient<IRateProviderFactory, RateProviderFactory>();
builder.Services.AddKeyedTransient<IExchangeRateProvider, NbpExchangeRateProvider>("nbp");

// HttpClient
builder.Services.AddHttpClient<NbpClient>((sp, http) =>
{
    var nbp = sp.GetRequiredService<IOptions<NbpOptions>>().Value;
    http.BaseAddress = new Uri(nbp.BaseUrl.TrimEnd('/') + "/");
})
.AddPolicyHandler(PollyPolicies.Retry());

// Application services
builder.Services.AddScoped<GetRatesHandler>();

// Build
var app = builder.Build();

// Pipeline
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
