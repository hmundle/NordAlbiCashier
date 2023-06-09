using Nac.Dal.Repos;
using Nac.Dal.Repos.Interfaces;
using Nac.Lib.Serialization.Converter;

namespace Nac.Dal.Initialization;

public static class SampleDataInitializer
{
    public static async Task DropAndCreateDatabaseAsync(PpasDbContext context)
    {
        await context.Database.EnsureDeletedAsync();
        await context.Database.MigrateAsync();
    }

    public static async Task InitializeDataAsync(PpasDbContext context)
    {
        await DropAndCreateDatabaseAsync(context);
        await SeedDataAsync(context);
    }

    public static async Task ClearAndReseedDatabaseAsync(PpasDbContext context)
    {
        await ClearAllDataAsync(context);
        await SeedDataAsync(context);
    }

    public static async Task ClearForWebAppDatabaseAsync(PpasDbContext context)
    {
        await ClearDataForWebAppAsync(context);
    }

    internal static async Task ClearDataAsync(PpasDbContext context, string[] entities)
    {
        foreach (var entityName in entities)
        {
            var entity = context.Model.FindEntityType(entityName);
            var tableName = entity!.GetTableName();
            var schemaName = entity!.GetSchema();
            await context.Database.ExecuteSqlRawAsync($"DELETE FROM {schemaName}.{tableName}");
        }
    }

    internal static async Task ClearAllDataAsync(PpasDbContext context)
    {
        await ClearDataForWebAppAsync(context);
        var entities = new[]
        {
            typeof(User).FullName,
        };
        await ClearDataAsync(context, entities!);
    }

    internal static async Task ClearDataForWebAppAsync(PpasDbContext context)
    {
        var entities = new[]
        {
            typeof(L2Product).FullName,
            typeof(L1Archiving).FullName,
            typeof(L1Delivery).FullName,
            typeof(L1QualityControl).FullName,
            typeof(L1Sip).FullName,
            typeof(L1Product).FullName,
            typeof(L0Archiving).FullName,
            typeof(L0Delivery).FullName,
            typeof(L0QualityControl).FullName,
            typeof(L0Sip).FullName,
            typeof(L0Product).FullName,
            typeof(TProduct).FullName,
            typeof(Pass).FullName,
            typeof(Tape).FullName,
            typeof(ProcError).FullName,
            typeof(ErrorCode).FullName,
            typeof(FileCounter).FullName,
        };
        await ClearDataAsync(context, entities!);
    }

    internal static async Task SeedDataAsync(PpasDbContext context)
    {
        try
        {
            await ProcessInsertAsync(context, context.Users!, SampleData.Users);

            await ProcessInsertAsync(context, context.ErrorCodes!, SampleData.ErrorCodes);
            await ProcessInsertAsync(context, context.ProcErrors!, SampleData.ProcErrors);

            await ProcessInsertAsync(context, context.Tapes!, SampleData.Tapes);
            await ProcessInsertAsync(context, context.Passes!, SampleData.Passes);
            await ProcessInsertProcAsync(context, context.TProducts!, SampleData.TProducts);

            await ProcessInsertProcAsync(context, context.L0Products!, SampleData.L0Products);
            await ProcessInsertAsync(context, context.L0Archivings!, SampleData.L0Archivings);
            await ProcessInsertAsync(context, context.L0Deliveries!, SampleData.L0Deliveries);
            await ProcessInsertAsync(context, context.L0QualityControls!, SampleData.L0QualityControls);
            await ProcessInsertAsync(context, context.L0Sips!, SampleData.L0Sips);

            await ProcessInsertProcAsync(context, context.L1Products!, SampleData.L1Products);
            await ProcessInsertAsync(context, context.L1Archivings!, SampleData.L1Archivings);
            await ProcessInsertAsync(context, context.L1Deliveries!, SampleData.L1Deliveries);
            await ProcessInsertAsync(context, context.L1QualityControls!, SampleData.L1QualityControls);
            await ProcessInsertAsync(context, context.L1Sips!, SampleData.L1Sips);

            await ProcessInsertProcAsync(context, context.L2Products!, SampleData.L2Products);

            var fileCounterList = await InitializeFileCounterDataAsync(context, SampleData.L0Products, SampleData.L1Products);
            await ProcessInsertAsync(context, context.FileCounters!, fileCounterList);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            //Set a break point here to determine what the issues is
            throw;
        }
    }

    static async Task ProcessInsertAsync<TEntity>(
        PpasDbContext context,
        DbSet<TEntity> table,
        List<TEntity> records) where TEntity : BaseEntityId
    {
        if (await table.AnyAsync())
        {
            return;
        }
        IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var metaData = context.Model.FindEntityType(typeof(TEntity).FullName!);
                await table.AddRangeAsync(records);
                await context.SaveChangesAsync();
                await context.Database.ExecuteSqlRawAsync(
                    $"ALTER SEQUENCE {metaData!.GetSchema()}.{metaData!.GetTableName()}_id_seq RESTART WITH {records.Count + 1}");
                await transaction.CommitAsync();
            }
            catch (Exception /*ex*/)
            {
                await transaction.RollbackAsync();
            }
        }
        );
    }

    static async Task ProcessInsertProcAsync<TEntityProc>(
        PpasDbContext context,
        DbSet<TEntityProc> table,
        List<TEntityProc> records) where TEntityProc : BaseEntityProduct
    {
        if (await table.AnyAsync())
        {
            return;
        }
        await table.AddRangeAsync(records);
        await context.SaveChangesAsync();
    }

    private static async Task<List<FileCounter>> InitializeFileCounterDataAsync(PpasDbContext context,
                                                                                List<L0Product> l0List,
                                                                                List<L1Product> l1List)
    {
        IL0ProductRepo repoL0 = new L0ProductRepo(context);
        List<FileCounter> fileCounters = new();

        for (int i = 0; i < 6; i++)
        {
            var item = l0List[i];
            var product = await repoL0.FindAsync(item.ProcId);
            if (product == null)
                continue;

            var passNavigation = await repoL0.FindPassNavigationAsync(product);
            if (product.PrecProcNavigation == null || passNavigation == null)
                continue;

            var filename = EarthObservationConverter.GetBaseFileNameL0(product);
            FileCounter fileCounter = new() { Id = i + 1, FileName = filename[..^1], Version = i + 1 };
            fileCounters.Add(fileCounter);
        }

        IL1ProductRepo repoL1 = new L1ProductRepo(context);
        for (int i = 0; i < 6; i++)
        {
            var item = l1List[i];
            var l1Product = await repoL1.FindAsync(item.ProcId);
            if (l1Product == null)
                continue;

            var passNavigation = await repoL1.FindPassNavigationAsync(l1Product);
            if (l1Product.PrecProcNavigation == null || l1Product.PrecProcNavigation.PrecProcNavigation == null || passNavigation == null)
                continue;

            var filename = EarthObservationConverter.GetBaseFileNameL1(l1Product);
            FileCounter fileCounter = new() { Id = i + 7, FileName = filename[..^1], Version = i + 1 };
            fileCounters.Add(fileCounter);
        }
        return fileCounters;
    }
}
