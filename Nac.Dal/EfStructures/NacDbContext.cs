using Npgsql;

using Nac.Dal.Exceptions;
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
    public virtual DbSet<Invoice>? Invoices { get; set; }

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
        modelBuilder.Entity<Pass>(entity =>
        {
            entity
                .HasOne(d => d.TapeNavigation)
                .WithMany(p => p!.Passes)
                .HasForeignKey(d => d.TapeId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<TProduct>(entity =>
        {
            entity
                .HasOne(d => d.PassNavigation)
                .WithMany(p => p!.TProducts)
                .HasForeignKey(d => d.PassId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L0Product>(entity =>
        {
            entity
                .HasOne(d => d.PrecProcNavigation)
                .WithMany(p => p!.L0Products)
                .HasForeignKey(d => d.PrecProcId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L1Product>(entity =>
        {
            entity
                .HasOne(d => d.PrecProcNavigation)
                .WithMany(p => p!.L1Products)
                .HasForeignKey(d => d.PrecProcId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L2Product>(entity =>
        {
            entity
                .HasOne(d => d.PrecProcNavigation)
                .WithMany(p => p!.L2Products)
                .HasForeignKey(d => d.PrecProcId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // processing error code
        modelBuilder.Entity<ProcError>(entity =>
        {
            entity
                .HasOne(d => d.ErrorCodeNavigation)
                .WithMany(p => p!.ProcErrors)
                .HasForeignKey(d => d.ErrorCodeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // L0 product sub-processings
        modelBuilder.Entity<L0Sip>(entity =>
        {
            entity
                .Property(e => e.L0ProductProcId)
                .IsRequired(true);
            entity
                .HasOne(d => d.L0ProductNavigation)
                .WithMany(p => p!.L0Sips)
                .HasForeignKey(d => d.L0ProductProcId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L0QualityControl>(entity =>
        {
            entity
                .Property(e => e.L0ProductProcId)
                .IsRequired(true);
            entity
                .HasOne(d => d.L0ProductNavigation)
                .WithMany(p => p!.L0QualityControls)
                .HasForeignKey(d => d.L0ProductProcId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L0Delivery>(entity =>
        {
            entity
                .Property(e => e.L0ProductProcId)
                .IsRequired(true);
            entity
                .HasOne(d => d.L0ProductNavigation)
                .WithMany(p => p!.L0Deliveries)
                .HasForeignKey(d => d.L0ProductProcId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L0Archiving>(entity =>
        {
            entity
                .Property(e => e.L0ProductProcId)
                .IsRequired(true);
            entity
                .HasOne(d => d.L0ProductNavigation)
                .WithMany(p => p!.L0Archivings)
                .HasForeignKey(d => d.L0ProductProcId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        // L1 product sub-processings
        modelBuilder.Entity<L1Sip>(entity =>
        {
            entity
                .Property(e => e.L1ProductProcId)
                .IsRequired(true);
            entity
                .HasOne(d => d.L1ProductNavigation)
                .WithMany(p => p!.L1Sips)
                .HasForeignKey(d => d.L1ProductProcId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L1QualityControl>(entity =>
        {
            entity
                .Property(e => e.L1ProductProcId)
                .IsRequired(true);
            entity
                .HasOne(d => d.L1ProductNavigation)
                .WithMany(p => p!.L1QualityControls)
                .HasForeignKey(d => d.L1ProductProcId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L1Delivery>(entity =>
        {
            entity
                .Property(e => e.L1ProductProcId)
                .IsRequired(true);
            entity
                .HasOne(d => d.L1ProductNavigation)
                .WithMany(p => p!.L1Deliveries)
                .HasForeignKey(d => d.L1ProductProcId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L1Archiving>(entity =>
        {
            entity
                .Property(e => e.L1ProductProcId)
                .IsRequired(true);
            entity
                .HasOne(d => d.L1ProductNavigation)
                .WithMany(p => p!.L1Archivings)
                .HasForeignKey(d => d.L1ProductProcId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
    private static void CreateForeignKeysOneToOne(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TProduct>(entity =>
        {
            entity
                .Property(e => e.ProcErrorId)
                .IsRequired(false);
            //entity
            //    .HasIndex(e => e.ProcErrorId)
            //    .IsUnique();
            entity
                .HasOne(d => d.ProcErrorNavigation)
                .WithOne(p => p.TProductNavigation)
                .HasForeignKey<TProduct>(d => d.ProcErrorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L0Product>(entity =>
        {
            entity
                .Property(e => e.ProcErrorId)
                .IsRequired(false);
            //entity
            //    .HasIndex(e => e.ProcErrorId)
            //    .IsUnique();
            entity
                .HasOne(d => d.ProcErrorNavigation)
                .WithOne(p => p.L0ProductNavigation)
                .HasForeignKey<L0Product>(d => d.ProcErrorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L1Product>(entity =>
        {
            entity
                .Property(e => e.ProcErrorId)
                .IsRequired(false);
            //entity
            //    .HasIndex(e => e.ProcErrorId)
            //    .IsUnique();
            entity
                .HasOne(d => d.ProcErrorNavigation)
                .WithOne(p => p.L1ProductNavigation)
                .HasForeignKey<L1Product>(d => d.ProcErrorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L2Product>(entity =>
        {
            entity
                .Property(e => e.ProcErrorId)
                .IsRequired(false);
            //entity
            //    .HasIndex(e => e.ProcErrorId)
            //    .IsUnique();
            entity
                .HasOne(d => d.ProcErrorNavigation)
                .WithOne(p => p.L2ProductNavigation)
                .HasForeignKey<L2Product>(d => d.ProcErrorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L0Sip>(entity =>
        {
            entity
                .Property(e => e.ProcErrorId)
                .IsRequired(false);
            entity
                .HasOne(d => d.ProcErrorNavigation)
                .WithOne(p => p.L0SipNavigation)
                .HasForeignKey<L0Sip>(d => d.ProcErrorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L0QualityControl>(entity =>
        {
            entity
                .Property(e => e.ProcErrorId)
                .IsRequired(false);
            entity
                .HasOne(d => d.ProcErrorNavigation)
                .WithOne(p => p.L0QualityControlNavigation)
                .HasForeignKey<L0QualityControl>(d => d.ProcErrorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L0Delivery>(entity =>
        {
            entity
                .Property(e => e.ProcErrorId)
                .IsRequired(false);
            entity
                .HasOne(d => d.ProcErrorNavigation)
                .WithOne(p => p.L0DeliveryNavigation)
                .HasForeignKey<L0Delivery>(d => d.ProcErrorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L0Archiving>(entity =>
        {
            entity
                .Property(e => e.ProcErrorId)
                .IsRequired(false);
            entity
                .HasOne(d => d.ProcErrorNavigation)
                .WithOne(p => p.L0ArchivingNavigation)
                .HasForeignKey<L0Archiving>(d => d.ProcErrorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L1Sip>(entity =>
        {
            entity
                .Property(e => e.ProcErrorId)
                .IsRequired(false);
            entity
                .HasOne(d => d.ProcErrorNavigation)
                .WithOne(p => p.L1SipNavigation)
                .HasForeignKey<L1Sip>(d => d.ProcErrorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L1QualityControl>(entity =>
        {
            entity
                .Property(e => e.ProcErrorId)
                .IsRequired(false);
            entity
                .HasOne(d => d.ProcErrorNavigation)
                .WithOne(p => p.L1QualityControlNavigation)
                .HasForeignKey<L1QualityControl>(d => d.ProcErrorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L1Delivery>(entity =>
        {
            entity
                .Property(e => e.ProcErrorId)
                .IsRequired(false);
            entity
                .HasOne(d => d.ProcErrorNavigation)
                .WithOne(p => p.L1DeliveryNavigation)
                .HasForeignKey<L1Delivery>(d => d.ProcErrorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<L1Archiving>(entity =>
        {
            entity
                .Property(e => e.ProcErrorId)
                .IsRequired(false);
            entity
                .HasOne(d => d.ProcErrorNavigation)
                .WithOne(p => p.L1ArchivingNavigation)
                .HasForeignKey<L1Archiving>(d => d.ProcErrorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void CreateGlobalQueryFiltersAndTimestamps(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ErrorCode>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<L0Archiving>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<L0Delivery>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<L0Product>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<L0QualityControl>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<L0Sip>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<L1Archiving>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<L1Delivery>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<L1Product>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<L1QualityControl>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<L1Sip>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<L2Product>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<Pass>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<ProcError>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<WorkflowStatus>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<QualityControlCounter>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<Tape>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<TProduct>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<SeriLogEntry>(entity =>
        {
            entity.Property(e => e.DbTimestamp).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<FileCounter>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<StatisticInfo>(entity =>
        {
            entity.HasQueryFilter(e => e.IsDeleted == false);
            entity.Property(e => e.Created).HasDefaultValueSql("now()");
        });
    }

    private static void AddViewsToModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VL0QualityControl>(entity =>
        {
            entity.ToView("v_l0_quality_controls", "ppas");
        });
        modelBuilder.Entity<VL1QualityControl>(entity =>
        {
            entity.ToView("v_l1_quality_controls", "ppas");
        });
        modelBuilder.Entity<VTProduct>(entity =>
        {
            entity.ToView("v_t_products", "ppas");
        });
        modelBuilder.Entity<VL0Product>(entity =>
        {
            entity.ToView("v_l0_products", "ppas");
        });
        modelBuilder.Entity<VL1Product>(entity =>
        {
            entity.ToView("v_l1_products", "ppas");
        });
    }

}
