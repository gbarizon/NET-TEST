using Ambev.DeveloperEvaluation.Application.Services;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.MessageBrocker;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class ApplicationModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddCors();
        builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
        builder.Services.AddScoped<ISaleService, SaleService>();
        builder.Services.AddScoped<IMessageBroker, FakeMessageBroker>();
    }
}