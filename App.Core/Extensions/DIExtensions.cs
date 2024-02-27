using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace App.Core.Extensions;

public static class DIExtensions
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services) =>
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}