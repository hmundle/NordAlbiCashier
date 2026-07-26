using Microsoft.Extensions.DependencyInjection;
using Nac.Dal.Repos;
using Nac.Dal.Repos.Interfaces;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Nac.Dal.Configuration;

public static class DalConfiguration
{
    public static IServiceCollection RegisterDalServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContextPool<NacDbContext>(
            optionsAction: optionsBuilder => DalConfiguration.ConfigureDbOptions(connectionString, optionsBuilder),
            poolSize: 50);

        services.AddScoped<IProductRepo, ProductRepo>();
        services.AddScoped<ISellingRepo, SellingRepo>();
        services.AddScoped<IInvoiceRepo, InvoiceRepo>();
        services.AddScoped<ICashStatusRepo, CashStatusRepo>();
        services.AddScoped<ICashFlowRepo, CashFlowRepo>();

        services.AddScoped<IUserRepo, UserRepo>();

        return services;
    }

    public static DbContextOptionsBuilder ConfigureDbOptions(string connectionString, DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(connectionString, npgsqlOptionsBuilder => DalConfiguration.ConfigureNpgsqlDbOptions(npgsqlOptionsBuilder))
            .UseSnakeCaseNamingConvention()
            .UseValidationCheckConstraints()
            .EnableSensitiveDataLogging()
        ;
        return optionsBuilder;
    }

    public static NpgsqlDbContextOptionsBuilder ConfigureNpgsqlDbOptions(NpgsqlDbContextOptionsBuilder npgsqlOptionsBuilder)
    {
        npgsqlOptionsBuilder.EnableRetryOnFailure().CommandTimeout(60)
            .EnableRetryOnFailure().CommandTimeout(60)
            .MigrationsHistoryTable("ef_migrations_history", "migration")
            .MapEnum<SyncStatus>("sync_status")
            .MapEnum<PaymentType>("payment_type")
            .MapEnum<ProductCategory>("product_category")
            .MapEnum<ProductGroup>("product_group")
            ;

        return npgsqlOptionsBuilder;
    }


}
