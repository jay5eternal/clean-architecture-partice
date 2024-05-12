using Application.Interfaces;
using Application.UseCases;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

/// <summary>
/// ServiceCollection Extensions
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// AddUseCases
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        //// UseCases services
        services.AddScoped<IInitialDataUseCase, InitialDataUseCase>();
        services.AddScoped<IGetShelfUseCase, GetShelfUseCase>();
        services.AddScoped<IAddSkuToShelfUseCase, AddSkuToShelfUseCase>();
        services.AddScoped<IMoveSkuInShelfUseCase, MoveSkuInShelfUseCase>();

        return services;
    }

    /// <summary>
    /// AddInfrastructureServices
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        //// Persistence services
        services.AddScoped<IShelfRepository, ShelfRepository>();
        services.AddScoped<ISkuRepository, SkuRepository>();

        return services;
    }

    /// <summary>
    /// AddMongoDBServices
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <param name="configuration">IConfiguration</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddMongoDBServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<MongoDBContext>(provider =>
            new MongoDBContext(configuration));
        return services;
    }
}