using Microsoft.EntityFrameworkCore.Migrations;

namespace Nac.Dal.EfStructures;

public static class MigrationHelpers
{
    public static void DropAllDbTypes(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .OldAnnotation("Npgsql:Enum:payment_type", "undefined,pending,cash,card,pay_pal")
            .OldAnnotation("Npgsql:Enum:product_category", "undefined,code,quantity,price,weight")
            .OldAnnotation("Npgsql:Enum:sync_status", "local,server");
    }

    private static string FormatDeleteAllDataSql(string TableName)
    {
        return $"DELETE FROM ppas.\"{TableName}\";";
    }





}
