using Nac.Dal.Exceptions;
using Nac.Models.EntitiesView;
using Npgsql;
using System.Data.Common;

namespace Nac.Dal.EfStructures;

public partial class NacDbContext : /*Identity*/DbContext
{
    public static DbDataSource BuildDataSource(string connectionString)
    {
        // Create a data source with the configuration you want:
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder.MapEnum<SyncStatus>("sync_status");
        dataSourceBuilder.MapEnum<PaymentType>("payment_type");
        dataSourceBuilder.MapEnum<ProductCategory>("product_category");
        dataSourceBuilder.MapEnum<ProductGroup>("product_group");

        return dataSourceBuilder.Build();
    }

    public NacDbContext(DbContextOptions<NacDbContext> options)
        : base(options)
    {
        base.SavingChanges += (sender, args) =>
        {
            //Console.WriteLine($"Saving changes for {((PpasDbContext)sender!).Database.GetConnectionString()}");
            Console.WriteLine($"Saving changes for context type {((NacDbContext)sender!)}");
        };
        base.SavedChanges += (sender, args) =>
        {
            //Console.WriteLine($"Saved {args!.EntitiesSavedCount} changes for {((PpasDbContext)sender!).Database.GetConnectionString()}");
            Console.WriteLine($"Saved {args!.EntitiesSavedCount} changes for context type {((NacDbContext)sender!)}");
        };
        base.SaveChangesFailed += (sender, args) =>
        {
            Console.WriteLine($"An exception occurred! {args.Exception.Message} entities");
        };

        //ChangeTracker.Tracked += ChangeTracker_Tracked;
        //ChangeTracker.StateChanged += ChangeTracker_StateChanged;
    }

    public virtual DbSet<Product>? Products { get; set; }
    public virtual DbSet<Selling>? Sellings { get; set; }
    public virtual DbSet<SellingsV> SellingsV { get; set; } = null!;
    public virtual DbSet<Invoice>? Invoices { get; set; }

    public virtual DbSet<CashStatus>? CashStatus { get; set; }
    public virtual DbSet<CashFlow>? CashFlow { get; set; }

    public virtual DbSet<User>? Users { get; set; }

    public DbSet<SeriLogEntry>? LogEntries { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            //A concurrency error occurred
            //Should log and handle intelligently
            throw new CustomConcurrencyException("A concurrency error happened.", ex);
        }
        catch (RetryLimitExceededException ex)
        {
            //DbResiliency retry limit exceeded
            //Should log and handle intelligently
            throw new CustomRetryLimitExceededException("There is a problem with SQl Server.", ex);
        }
        catch (DbUpdateException ex)
        {
            //Should log and handle intelligently
            throw new CustomDbUpdateException("An error occurred updating the database", ex);
        }
        catch (Exception ex)
        {
            //Should log and handle intelligently
            throw new CustomException("An error occurred updating the database", ex);
        }
    }

    public override int SaveChanges()
    {
        throw new NotSupportedException("SaveChanges() is not supported, use SaveChangesAsync() instead!");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //// for IdentityDbContext relations/tables
        //base.OnModelCreating(modelBuilder);

        //// do identity stuff into users schema, all ppas stuff will use explicit schema
        //modelBuilder.HasDefaultSchema("users");

        // Fluent API calls go here
        modelBuilder
            .HasPostgresEnum<SyncStatus>()
            .HasPostgresEnum<PaymentType>()
            .HasPostgresEnum<ProductCategory>()
            .HasPostgresEnum<ProductGroup>()
            ;

        modelBuilder
            .HasDbFunction(typeof(NacDbContext).GetMethod(nameof(DateTrunc))!)
            .HasName("date_trunc");
        modelBuilder
            .HasDbFunction(typeof(NacDbContext).GetMethod(nameof(DatePart))!)
            .HasName("date_part");

        // foreign keys one-to-many
        CreateForeignKeysOneToMany(modelBuilder);

        // foreign keys one-to-one
        CreateForeignKeysOneToOne(modelBuilder);

        // global query filters 
        CreateGlobalQueryFiltersAndTimestamps(modelBuilder);

        // configure views
        AddViewsToModel(modelBuilder);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DateTime DateTrunc(string datePart, DateTime date) => throw new Exception();
    public double DatePart(string datePart, DateTime date) => throw new Exception();

    private static void CreateForeignKeysOneToMany(ModelBuilder modelBuilder)
    {
        // main chain
        modelBuilder.Entity<Selling>(entity =>
        {
            entity
                .HasOne(d => d.InvoiceNavigation)
                .WithMany(p => p.Sellings)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Selling>(entity =>
        {
            entity
                .HasOne(d => d.ProductNavigation)
                .WithMany(p => p.Sellings)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void CreateForeignKeysOneToOne(ModelBuilder modelBuilder)
    {
    }

    private static void CreateGlobalQueryFiltersAndTimestamps(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
            entity.Property(e => e.Operator).HasDefaultValue("unknown");
        });
        modelBuilder.Entity<Selling>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
            entity.Property(e => e.Operator).HasDefaultValue("unknown");
        });
        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
            entity.Property(e => e.Operator).HasDefaultValue("unknown");
        });
        modelBuilder.Entity<CashStatus>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
            entity.Property(e => e.Operator).HasDefaultValue("unknown");
        });
        modelBuilder.Entity<CashFlow>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
            entity.Property(e => e.Operator).HasDefaultValue("unknown");
        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
            entity.Property(e => e.Operator).HasDefaultValue("unknown");
        });
    }

    private static void AddViewsToModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SellingsV>(entity =>
        {
            entity.ToView("sellings_v");
        });
    }

}
