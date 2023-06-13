using Nac.Dal.Repos;
using Nac.Dal.Repos.Interfaces;

namespace Nac.Dal.Initialization;

public static class SampleDataInitializer
{
    public static async Task DropAndCreateDatabaseAsync(NacDbContext context)
    {
        await context.Database.EnsureDeletedAsync();
        await context.Database.MigrateAsync();
    }

    public static async Task InitializeDataAsync(NacDbContext context)
    {
        await DropAndCreateDatabaseAsync(context);
        await SeedDataAsync(context);
    }

    public static async Task ClearAndReseedDatabaseAsync(NacDbContext context)
    {
        await ClearAllDataAsync(context);
        await SeedDataAsync(context);
    }

    public static async Task ClearForWebAppDatabaseAsync(NacDbContext context)
    {
        await ClearDataForWebAppAsync(context);
    }

    internal static async Task ClearDataAsync(NacDbContext context, string[] entities)
    {
        foreach (var entityName in entities)
        {
            var entity = context.Model.FindEntityType(entityName);
            var tableName = entity!.GetTableName();
            var schemaName = entity!.GetSchema();
            await context.Database.ExecuteSqlRawAsync($"DELETE FROM {schemaName}.{tableName}");
        }
    }

    internal static async Task ClearAllDataAsync(NacDbContext context)
    {
        await ClearDataForWebAppAsync(context);
        var entities = new[]
        {
            typeof(User).FullName,
        };
        await ClearDataAsync(context, entities!);
    }

    internal static async Task ClearDataForWebAppAsync(NacDbContext context)
    {
        var entities = new[]
        {
            typeof(Invoice).FullName,
            typeof(Selling).FullName,
            typeof(Product).FullName,
        };
        await ClearDataAsync(context, entities!);
    }

    internal static async Task SeedDataAsync(NacDbContext context)
    {
        try
        {
            await ProcessInsertAsync(context, context.Users!, SampleData.Users);

            await ProcessInsertAsync(context, context.Products!, SampleData.Products);
            await ProcessInsertAsync(context, context.Invoices!, SampleData.Invoices);
            await ProcessInsertAsync(context, context.Sellings!, SampleData.Sellings);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            //Set a break point here to determine what the issues is
            throw;
        }
    }

    static async Task ProcessInsertAsync<TEntity>(
        NacDbContext context,
        DbSet<TEntity> table,
        List<TEntity> records) where TEntity : BaseEntity
    {
        if (await table.AnyAsync())
        {
            return;
        }
        await table.AddRangeAsync(records);
        await context.SaveChangesAsync();
    }

}
