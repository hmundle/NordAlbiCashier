using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nac.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddSellingsView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            MigrationHelpersViews.CreateSellingsView(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            MigrationHelpersViews.DropSellingsView(migrationBuilder);
        }
    }
}
