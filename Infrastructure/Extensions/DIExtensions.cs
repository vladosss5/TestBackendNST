using App.Core.Interfaces;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class DIExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DBContext>(options => options.UseNpgsql(new Decode(connectionString).ConnectionString));
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IPersonService, PersonService>();
        return services;
    }
}