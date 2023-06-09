using Microsoft.EntityFrameworkCore.Design;

namespace Nac.Dal.EfStructures;

public class PpasDbContextFactory : IDesignTimeDbContextFactory<NacDbContext>
{
    public NacDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<NacDbContext>();
        var connectionString = @"Host=localhost;Username=postgres;Password=PpasP@ssw0rd;Database=TranscriptionDB";
        /*using*/
        var dataSource = NacDbContext.BuildDataSource(connectionString);
        optionsBuilder.UseNpgsql(dataSource, x => x.MigrationsHistoryTable("ef_migrations_history", "migration"))
            .UseSnakeCaseNamingConvention();
        optionsBuilder.UseValidationCheckConstraints();
        Console.WriteLine($"The connection string is: {connectionString}");
        return new NacDbContext(optionsBuilder.Options);
    }
}
