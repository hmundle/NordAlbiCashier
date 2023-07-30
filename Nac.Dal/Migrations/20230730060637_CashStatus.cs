using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Nac.Models.Entities;

#nullable disable

namespace Nac.Dal.Migrations
{
    /// <inheritdoc />
    public partial class CashStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cash_status",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    till = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    _500 = table.Column<double>(type: "double precision", nullable: false),
                    _200 = table.Column<double>(type: "double precision", nullable: false),
                    _100 = table.Column<double>(type: "double precision", nullable: false),
                    _50 = table.Column<double>(type: "double precision", nullable: false),
                    _20 = table.Column<double>(type: "double precision", nullable: false),
                    _10 = table.Column<double>(type: "double precision", nullable: false),
                    _5 = table.Column<double>(type: "double precision", nullable: false),
                    _2 = table.Column<double>(type: "double precision", nullable: false),
                    _1 = table.Column<double>(type: "double precision", nullable: false),
                    _050 = table.Column<double>(type: "double precision", nullable: false),
                    _020 = table.Column<double>(type: "double precision", nullable: false),
                    _010 = table.Column<double>(type: "double precision", nullable: false),
                    _005 = table.Column<double>(type: "double precision", nullable: false),
                    _002 = table.Column<double>(type: "double precision", nullable: false),
                    _001 = table.Column<double>(type: "double precision", nullable: false),
                    @operator = table.Column<string>(name: "operator", type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "unknown"),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_sychronized = table.Column<SyncStatus>(type: "sync_status", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cash_status", x => x.id);
                    table.CheckConstraint("CK_cash_status_operator_MinLength", "LENGTH(operator) >= 1");
                });

            migrationBuilder.CreateIndex(
                name: "ix_cash_status_created",
                table: "cash_status",
                column: "created");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cash_status");
        }
    }
}
