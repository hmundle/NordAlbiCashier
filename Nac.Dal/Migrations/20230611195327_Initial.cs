using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Nac.Models.Entities;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Nac.Dal.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "logging");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:payment_type", "undefined,pending,cash,card,pay_pal")
                .Annotation("Npgsql:Enum:product_category", "undefined,code,quantity,price,weight")
                .Annotation("Npgsql:Enum:sync_status", "local,server");

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    type = table.Column<PaymentType>(type: "payment_type", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_sychronized = table.Column<SyncStatus>(type: "sync_status", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_invoices", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    category = table.Column<ProductCategory>(type: "product_category", nullable: false),
                    bar_code = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    price_reduced = table.Column<double>(type: "double precision", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_sychronized = table.Column<SyncStatus>(type: "sync_status", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "seri_logs",
                schema: "logging",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    application_name = table.Column<string>(type: "text", nullable: true),
                    message = table.Column<string>(type: "text", nullable: true),
                    level = table.Column<string>(type: "text", nullable: true),
                    raise_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    db_timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    exception = table.Column<string>(type: "text", nullable: true),
                    properties = table.Column<string>(type: "jsonb", nullable: true),
                    machine_name = table.Column<string>(type: "text", nullable: true),
                    file_path = table.Column<string>(type: "text", nullable: true),
                    member_name = table.Column<string>(type: "text", nullable: true),
                    line_number = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_seri_logs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_sychronized = table.Column<SyncStatus>(type: "sync_status", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.CheckConstraint("CK_users_name_MinLength", "LENGTH(name) >= 0");
                });

            migrationBuilder.CreateTable(
                name: "sellings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    product_id = table.Column<Guid>(type: "uuid", nullable: true),
                    invoice_id = table.Column<Guid>(type: "uuid", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    price_manual = table.Column<double>(type: "double precision", nullable: false),
                    weight = table.Column<double>(type: "double precision", nullable: false),
                    final_price = table.Column<double>(type: "double precision", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_sychronized = table.Column<SyncStatus>(type: "sync_status", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sellings", x => x.id);
                    table.ForeignKey(
                        name: "fk_sellings_invoices_invoice_id",
                        column: x => x.invoice_id,
                        principalTable: "invoices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_sellings_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_invoices_created",
                table: "invoices",
                column: "created");

            migrationBuilder.CreateIndex(
                name: "ix_products_bar_code",
                table: "products",
                column: "bar_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_products_created",
                table: "products",
                column: "created");

            migrationBuilder.CreateIndex(
                name: "ix_sellings_created",
                table: "sellings",
                column: "created");

            migrationBuilder.CreateIndex(
                name: "ix_sellings_invoice_id",
                table: "sellings",
                column: "invoice_id");

            migrationBuilder.CreateIndex(
                name: "ix_sellings_product_id",
                table: "sellings",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_created",
                table: "users",
                column: "created");

            migrationBuilder.CreateIndex(
                name: "ix_users_name",
                table: "users",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sellings");

            migrationBuilder.DropTable(
                name: "seri_logs",
                schema: "logging");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
