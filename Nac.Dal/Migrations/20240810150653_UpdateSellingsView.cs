using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nac.Dal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSellingsView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            MigrationHelpersViews.UpSellingsViewV2(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            MigrationHelpersViews.DownSellingsViewV2(migrationBuilder);
        }
    }
}
