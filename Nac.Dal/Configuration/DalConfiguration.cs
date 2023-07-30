using Microsoft.Extensions.DependencyInjection;
using Nac.Dal.Repos;
using Nac.Dal.Repos.Interfaces;

namespace Nac.Dal.Configuration;

public static class DalConfiguration
{
    public static IServiceCollection RegisterDalServices(this IServiceCollection services, string connectionString)
    {
        /*using*/
        var dataSource = NacDbContext.BuildDataSource(connectionString);

        services.AddDbContextPool<NacDbContext>(
            options => options
                .UseNpgsql(dataSource, sqlOptions => sqlOptions.EnableRetryOnFailure().CommandTimeout(60))
                .UseSnakeCaseNamingConvention()
                .UseValidationCheckConstraints()
            );

        services.AddScoped<IProductRepo, ProductRepo>();
        services.AddScoped<ISellingRepo, SellingRepo>();
        services.AddScoped<IInvoiceRepo, InvoiceRepo>();
        services.AddScoped<ICashStatusRepo, CashStatusRepo>();
        services.AddScoped<ICashFlowRepo, CashFlowRepo>();

        services.AddScoped<IUserRepo, UserRepo>();

        return services;
    }
}
