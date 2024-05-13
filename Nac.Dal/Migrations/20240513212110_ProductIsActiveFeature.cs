using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nac.Dal.Migrations
{
    /// <inheritdoc />
    public partial class ProductIsActiveFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "products",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "products");
        }
    }
}
