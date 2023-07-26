using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nac.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddOperator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_users_name_MinLength",
                table: "users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                table: "users",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "users",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "operator",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "unknown");

            migrationBuilder.AddColumn<string>(
                name: "operator",
                table: "sellings",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "unknown");

            migrationBuilder.AlterColumn<string>(
                name: "bar_code",
                table: "products",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "operator",
                table: "products",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "unknown");

            migrationBuilder.AddColumn<string>(
                name: "operator",
                table: "invoices",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "unknown");

            migrationBuilder.AddCheckConstraint(
                name: "CK_users_name_MinLength",
                table: "users",
                sql: "LENGTH(name) >= 3");

            migrationBuilder.AddCheckConstraint(
                name: "CK_users_operator_MinLength",
                table: "users",
                sql: "LENGTH(operator) >= 1");

            migrationBuilder.AddCheckConstraint(
                name: "CK_sellings_operator_MinLength",
                table: "sellings",
                sql: "LENGTH(operator) >= 1");

            migrationBuilder.AddCheckConstraint(
                name: "CK_products_bar_code_MinLength",
                table: "products",
                sql: "LENGTH(bar_code) >= 1");

            migrationBuilder.AddCheckConstraint(
                name: "CK_products_operator_MinLength",
                table: "products",
                sql: "LENGTH(operator) >= 1");

            migrationBuilder.AddCheckConstraint(
                name: "CK_invoices_operator_MinLength",
                table: "invoices",
                sql: "LENGTH(operator) >= 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_users_name_MinLength",
                table: "users");

            migrationBuilder.DropCheckConstraint(
                name: "CK_users_operator_MinLength",
                table: "users");

            migrationBuilder.DropCheckConstraint(
                name: "CK_sellings_operator_MinLength",
                table: "sellings");

            migrationBuilder.DropCheckConstraint(
                name: "CK_products_bar_code_MinLength",
                table: "products");

            migrationBuilder.DropCheckConstraint(
                name: "CK_products_operator_MinLength",
                table: "products");

            migrationBuilder.DropCheckConstraint(
                name: "CK_invoices_operator_MinLength",
                table: "invoices");

            migrationBuilder.DropColumn(
                name: "operator",
                table: "users");

            migrationBuilder.DropColumn(
                name: "operator",
                table: "sellings");

            migrationBuilder.DropColumn(
                name: "operator",
                table: "products");

            migrationBuilder.DropColumn(
                name: "operator",
                table: "invoices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created",
                table: "users",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "users",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AlterColumn<string>(
                name: "bar_code",
                table: "products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddCheckConstraint(
                name: "CK_users_name_MinLength",
                table: "users",
                sql: "LENGTH(name) >= 0");
        }
    }
}
