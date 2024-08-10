using Microsoft.EntityFrameworkCore.Migrations;

namespace Nac.Dal.EfStructures;

public static class MigrationHelpersViews
{
    public static void CreateSellingsViewV1(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql($@"
create view public.sellings_v as
select
	s.id as selling_id,
	s.product_id,
	p.bar_code,
	p.name,
	p.category, 
	p.group,
	p.price base_price,
	p.price_reduced base_price_reduced,
	s.quantity,
	s.price_manual,
	s.weight,
	s.final_price,
	s.created as selling_created,
	s.modified as selling_modified
from
	public.sellings s
join 
	public.products p 
on
	s.product_id = p.id
;
"
        );
    }

    public static void DropSellingsView(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("drop view public.sellings_v;");
    }

    public static void UpSellingsViewV1(MigrationBuilder migrationBuilder)
    {
        CreateSellingsViewV1(migrationBuilder);
    }

    public static void DownSellingsViewV1(MigrationBuilder migrationBuilder)
    {
        DropSellingsView(migrationBuilder);
    }

    public static void CreateSellingsViewV2(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql($@"
create view public.sellings_v as
select
	s.id as selling_id,
	s.product_id,
	p.bar_code,
	p.name,
	p.category, 
	p.group,
	p.price base_price,
	p.price_reduced base_price_reduced,
	s.quantity,
	s.price_manual,
	s.weight,
	s.final_price,
	s.created as selling_created,
	s.modified as selling_modified,
	s.operator
from
	public.sellings s
join 
	public.products p 
on
	s.product_id = p.id
WHERE
    s.is_deleted IS NOT TRUE
    AND p.is_deleted IS NOT TRUE
;
"
        );
    }

    public static void UpSellingsViewV2(MigrationBuilder migrationBuilder)
    {
        DropSellingsView(migrationBuilder);
        CreateSellingsViewV2(migrationBuilder);
    }

    public static void DownSellingsViewV2(MigrationBuilder migrationBuilder)
    {
        DropSellingsView(migrationBuilder);
        CreateSellingsViewV1(migrationBuilder);
    }

}
