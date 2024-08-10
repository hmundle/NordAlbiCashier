﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nac.Dal.EfStructures;
using Nac.Models.Entities;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Nac.Dal.Migrations
{
    [DbContext(typeof(NacDbContext))]
    partial class NacDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "payment_type", new[] { "undefined", "pending", "cash", "card", "pay_pal" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "product_category", new[] { "undefined", "code", "quantity", "price", "weight" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "product_group", new[] { "undefined", "getraenke", "milchprodukte", "wurst", "brot", "trockennahrung", "kaffee_tee", "fruehstueck", "suessigkeiten", "obst", "gemuese", "dosen_glaeser_tetra", "hygiene_reinigung", "sonstiges", "specials", "pfand" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "sync_status", new[] { "local", "server" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Nac.Models.Entities.CashFlow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime?>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<SyncStatus>("IsSychronized")
                        .HasColumnType("sync_status")
                        .HasColumnName("is_sychronized");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified");

                    b.Property<string>("Operator")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasDefaultValue("unknown")
                        .HasColumnName("operator");

                    b.Property<string>("Till")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("till");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.Property<double>("_001")
                        .HasColumnType("double precision")
                        .HasColumnName("_001");

                    b.Property<double>("_002")
                        .HasColumnType("double precision")
                        .HasColumnName("_002");

                    b.Property<double>("_005")
                        .HasColumnType("double precision")
                        .HasColumnName("_005");

                    b.Property<double>("_010")
                        .HasColumnType("double precision")
                        .HasColumnName("_010");

                    b.Property<double>("_020")
                        .HasColumnType("double precision")
                        .HasColumnName("_020");

                    b.Property<double>("_050")
                        .HasColumnType("double precision")
                        .HasColumnName("_050");

                    b.Property<double>("_1")
                        .HasColumnType("double precision")
                        .HasColumnName("_1");

                    b.Property<double>("_10")
                        .HasColumnType("double precision")
                        .HasColumnName("_10");

                    b.Property<double>("_100")
                        .HasColumnType("double precision")
                        .HasColumnName("_100");

                    b.Property<double>("_2")
                        .HasColumnType("double precision")
                        .HasColumnName("_2");

                    b.Property<double>("_20")
                        .HasColumnType("double precision")
                        .HasColumnName("_20");

                    b.Property<double>("_200")
                        .HasColumnType("double precision")
                        .HasColumnName("_200");

                    b.Property<double>("_5")
                        .HasColumnType("double precision")
                        .HasColumnName("_5");

                    b.Property<double>("_50")
                        .HasColumnType("double precision")
                        .HasColumnName("_50");

                    b.Property<double>("_500")
                        .HasColumnType("double precision")
                        .HasColumnName("_500");

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_cash_flow");

                    b.HasIndex("Created")
                        .HasDatabaseName("ix_cash_flow_created");

                    b.ToTable("cash_flow", null, t =>
                        {
                            t.HasCheckConstraint("CK_cash_flow_operator_MinLength", "LENGTH(operator) >= 1");
                        });
                });

            modelBuilder.Entity("Nac.Models.Entities.CashStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime?>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<SyncStatus>("IsSychronized")
                        .HasColumnType("sync_status")
                        .HasColumnName("is_sychronized");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified");

                    b.Property<string>("Operator")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasDefaultValue("unknown")
                        .HasColumnName("operator");

                    b.Property<string>("Till")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("till");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.Property<double>("_001")
                        .HasColumnType("double precision")
                        .HasColumnName("_001");

                    b.Property<double>("_002")
                        .HasColumnType("double precision")
                        .HasColumnName("_002");

                    b.Property<double>("_005")
                        .HasColumnType("double precision")
                        .HasColumnName("_005");

                    b.Property<double>("_010")
                        .HasColumnType("double precision")
                        .HasColumnName("_010");

                    b.Property<double>("_020")
                        .HasColumnType("double precision")
                        .HasColumnName("_020");

                    b.Property<double>("_050")
                        .HasColumnType("double precision")
                        .HasColumnName("_050");

                    b.Property<double>("_1")
                        .HasColumnType("double precision")
                        .HasColumnName("_1");

                    b.Property<double>("_10")
                        .HasColumnType("double precision")
                        .HasColumnName("_10");

                    b.Property<double>("_100")
                        .HasColumnType("double precision")
                        .HasColumnName("_100");

                    b.Property<double>("_2")
                        .HasColumnType("double precision")
                        .HasColumnName("_2");

                    b.Property<double>("_20")
                        .HasColumnType("double precision")
                        .HasColumnName("_20");

                    b.Property<double>("_200")
                        .HasColumnType("double precision")
                        .HasColumnName("_200");

                    b.Property<double>("_5")
                        .HasColumnType("double precision")
                        .HasColumnName("_5");

                    b.Property<double>("_50")
                        .HasColumnType("double precision")
                        .HasColumnName("_50");

                    b.Property<double>("_500")
                        .HasColumnType("double precision")
                        .HasColumnName("_500");

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_cash_status");

                    b.HasIndex("Created")
                        .HasDatabaseName("ix_cash_status_created");

                    b.ToTable("cash_status", null, t =>
                        {
                            t.HasCheckConstraint("CK_cash_status_operator_MinLength", "LENGTH(operator) >= 1");
                        });
                });

            modelBuilder.Entity("Nac.Models.Entities.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Comment")
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<DateTime?>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created")
                        .HasDefaultValueSql("now()");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<SyncStatus>("IsSychronized")
                        .HasColumnType("sync_status")
                        .HasColumnName("is_sychronized");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified");

                    b.Property<string>("Operator")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasDefaultValue("unknown")
                        .HasColumnName("operator");

                    b.Property<PaymentType>("Type")
                        .HasColumnType("payment_type")
                        .HasColumnName("type");

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_invoices");

                    b.HasIndex("Created")
                        .HasDatabaseName("ix_invoices_created");

                    b.ToTable("invoices", null, t =>
                        {
                            t.HasCheckConstraint("CK_invoices_operator_MinLength", "LENGTH(operator) >= 1");
                        });
                });

            modelBuilder.Entity("Nac.Models.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("BarCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("bar_code");

                    b.Property<ProductCategory>("Category")
                        .HasColumnType("product_category")
                        .HasColumnName("category");

                    b.Property<string>("Comment")
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<DateTime?>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("Delivered")
                        .HasColumnType("integer")
                        .HasColumnName("delivered");

                    b.Property<ProductGroup?>("Group")
                        .HasColumnType("product_group")
                        .HasColumnName("group");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<SyncStatus>("IsSychronized")
                        .HasColumnType("sync_status")
                        .HasColumnName("is_sychronized");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Operator")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasDefaultValue("unknown")
                        .HasColumnName("operator");

                    b.Property<double>("Price")
                        .HasColumnType("double precision")
                        .HasColumnName("price");

                    b.Property<double?>("PriceReduced")
                        .HasColumnType("double precision")
                        .HasColumnName("price_reduced");

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.HasIndex("BarCode")
                        .IsUnique()
                        .HasDatabaseName("ix_products_bar_code");

                    b.HasIndex("Created")
                        .HasDatabaseName("ix_products_created");

                    b.ToTable("products", null, t =>
                        {
                            t.HasCheckConstraint("CK_products_bar_code_MinLength", "LENGTH(bar_code) >= 1");

                            t.HasCheckConstraint("CK_products_operator_MinLength", "LENGTH(operator) >= 1");
                        });
                });

            modelBuilder.Entity("Nac.Models.Entities.Selling", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime?>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created")
                        .HasDefaultValueSql("now()");

                    b.Property<double>("FinalPrice")
                        .HasColumnType("double precision")
                        .HasColumnName("final_price");

                    b.Property<Guid?>("InvoiceId")
                        .HasColumnType("uuid")
                        .HasColumnName("invoice_id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<SyncStatus>("IsSychronized")
                        .HasColumnType("sync_status")
                        .HasColumnName("is_sychronized");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified");

                    b.Property<string>("Operator")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasDefaultValue("unknown")
                        .HasColumnName("operator");

                    b.Property<double>("PriceManual")
                        .HasColumnType("double precision")
                        .HasColumnName("price_manual");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_sellings");

                    b.HasIndex("Created")
                        .HasDatabaseName("ix_sellings_created");

                    b.HasIndex("InvoiceId")
                        .HasDatabaseName("ix_sellings_invoice_id");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_sellings_product_id");

                    b.ToTable("sellings", null, t =>
                        {
                            t.HasCheckConstraint("CK_sellings_operator_MinLength", "LENGTH(operator) >= 1");
                        });
                });

            modelBuilder.Entity("Nac.Models.Entities.SeriLogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ApplicationName")
                        .HasColumnType("text")
                        .HasColumnName("application_name");

                    b.Property<DateTime?>("DbTimestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("db_timestamp");

                    b.Property<string>("Exception")
                        .HasColumnType("text")
                        .HasColumnName("exception");

                    b.Property<string>("FilePath")
                        .HasColumnType("text")
                        .HasColumnName("file_path");

                    b.Property<string>("Level")
                        .HasColumnType("text")
                        .HasColumnName("level");

                    b.Property<int?>("LineNumber")
                        .HasColumnType("integer")
                        .HasColumnName("line_number");

                    b.Property<string>("MachineName")
                        .HasColumnType("text")
                        .HasColumnName("machine_name");

                    b.Property<string>("MemberName")
                        .HasColumnType("text")
                        .HasColumnName("member_name");

                    b.Property<string>("Message")
                        .HasColumnType("text")
                        .HasColumnName("message");

                    b.Property<string>("Properties")
                        .HasColumnType("jsonb")
                        .HasColumnName("properties");

                    b.Property<DateTime?>("RaiseDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("raise_date");

                    b.HasKey("Id")
                        .HasName("pk_seri_logs");

                    b.ToTable("seri_logs", "logging");
                });

            modelBuilder.Entity("Nac.Models.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime?>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created")
                        .HasDefaultValueSql("now()");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<SyncStatus>("IsSychronized")
                        .HasColumnType("sync_status")
                        .HasColumnName("is_sychronized");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<string>("Operator")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasDefaultValue("unknown")
                        .HasColumnName("operator");

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Created")
                        .HasDatabaseName("ix_users_created");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_users_name");

                    b.ToTable("users", null, t =>
                        {
                            t.HasCheckConstraint("CK_users_name_MinLength", "LENGTH(name) >= 3");

                            t.HasCheckConstraint("CK_users_operator_MinLength", "LENGTH(operator) >= 1");
                        });
                });

            modelBuilder.Entity("Nac.Models.EntitiesView.SellingsV", b =>
                {
                    b.Property<string>("BarCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("bar_code");

                    b.Property<double>("BasePrice")
                        .HasColumnType("double precision")
                        .HasColumnName("base_price");

                    b.Property<double?>("BasePriceReduced")
                        .HasColumnType("double precision")
                        .HasColumnName("base_price_reduced");

                    b.Property<ProductCategory>("Category")
                        .HasColumnType("product_category")
                        .HasColumnName("category");

                    b.Property<double>("FinalPrice")
                        .HasColumnType("double precision")
                        .HasColumnName("final_price");

                    b.Property<ProductGroup?>("Group")
                        .HasColumnType("product_group")
                        .HasColumnName("group");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<double>("PriceManual")
                        .HasColumnType("double precision")
                        .HasColumnName("price_manual");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.Property<DateTime?>("SellingCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("selling_created");

                    b.Property<Guid?>("SellingId")
                        .HasColumnType("uuid")
                        .HasColumnName("selling_id");

                    b.Property<DateTime?>("SellingModified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("selling_modified");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.ToTable((string)null);

                    b.ToView("sellings_v", (string)null);
                });

            modelBuilder.Entity("Nac.Models.Entities.Selling", b =>
                {
                    b.HasOne("Nac.Models.Entities.Invoice", "InvoiceNavigation")
                        .WithMany("Sellings")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_sellings_invoices_invoice_id");

                    b.HasOne("Nac.Models.Entities.Product", "ProductNavigation")
                        .WithMany("Sellings")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_sellings_products_product_id");

                    b.Navigation("InvoiceNavigation");

                    b.Navigation("ProductNavigation");
                });

            modelBuilder.Entity("Nac.Models.Entities.Invoice", b =>
                {
                    b.Navigation("Sellings");
                });

            modelBuilder.Entity("Nac.Models.Entities.Product", b =>
                {
                    b.Navigation("Sellings");
                });
#pragma warning restore 612, 618
        }
    }
}
