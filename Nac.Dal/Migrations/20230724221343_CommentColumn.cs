using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Nac.Models.Entities;

#nullable disable

namespace Nac.Dal.Migrations
{
    /// <inheritdoc />
    public partial class CommentColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:payment_type", "undefined,pending,cash,card,pay_pal")
                .Annotation("Npgsql:Enum:product_category", "undefined,code,quantity,price,weight")
                .Annotation("Npgsql:Enum:product_group", "undefined,getraenke,milchprodukte,wurst,brot,trockennahrung,kaffee_tee,fruehstueck,suessigkeiten,obst,gemuese,dosen_glaeser_tetra,hygiene_reinigung,sonstiges,specials,pfand")
                .Annotation("Npgsql:Enum:sync_status", "local,server")
                .OldAnnotation("Npgsql:Enum:payment_type", "undefined,pending,cash,card,pay_pal")
                .OldAnnotation("Npgsql:Enum:product_category", "undefined,code,quantity,price,weight")
                .OldAnnotation("Npgsql:Enum:sync_status", "local,server");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                table: "users",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                table: "sellings",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                table: "products",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AddColumn<string>(
                name: "comment",
                table: "products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "delivered",
                table: "products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<ProductGroup>(
                name: "group",
                table: "products",
                type: "product_group",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                table: "invoices",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AddColumn<string>(
                name: "comment",
                table: "invoices",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "comment",
                table: "products");

            migrationBuilder.DropColumn(
                name: "delivered",
                table: "products");

            migrationBuilder.DropColumn(
                name: "group",
                table: "products");

            migrationBuilder.DropColumn(
                name: "comment",
                table: "invoices");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:payment_type", "undefined,pending,cash,card,pay_pal")
                .Annotation("Npgsql:Enum:product_category", "undefined,code,quantity,price,weight")
                .Annotation("Npgsql:Enum:sync_status", "local,server")
                .OldAnnotation("Npgsql:Enum:payment_type", "undefined,pending,cash,card,pay_pal")
                .OldAnnotation("Npgsql:Enum:product_category", "undefined,code,quantity,price,weight")
                .OldAnnotation("Npgsql:Enum:product_group", "undefined,getraenke,milchprodukte,wurst,brot,trockennahrung,kaffee_tee,fruehstueck,suessigkeiten,obst,gemuese,dosen_glaeser_tetra,hygiene_reinigung,sonstiges,specials,pfand")
                .OldAnnotation("Npgsql:Enum:sync_status", "local,server");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                table: "sellings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                table: "products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                table: "invoices",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "now()");
        }
    }
}
