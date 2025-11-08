using CurrencyApp.WebApi.Middleware;
using CurrencyApp.WebApi.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

namespace CurrencyApp.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<GetRatesRequestValidator>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Currency API", Version = "v1" }));
        services.AddTransient<ExceptionMiddleware>();

        return services;
    }

    public static IApplicationBuilder UseWebApi(this IApplicationBuilder app, bool dev)
    {
        if (dev) 
        { 
            app.UseSwagger(); 
            app.UseSwaggerUI(); 
        }

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        return app;
    }
}
