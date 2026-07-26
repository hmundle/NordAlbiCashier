using Microsoft.EntityFrameworkCore.Design;
using Nac.Dal.Configuration;

namespace Nac.Dal.EfStructures;

public class NacDbContextFactory : IDesignTimeDbContextFactory<NacDbContext>
{
    public NacDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<NacDbContext>();
        var connectionString = @"Host=localhost;Username=postgres;Password=NacP@ssw0rd;Database=NacDB";
        DalConfiguration.ConfigureDbOptions(connectionString, optionsBuilder);

        Console.WriteLine($"The connection string is: {connectionString}");
        return new NacDbContext(optionsBuilder.Options);
    }
}
