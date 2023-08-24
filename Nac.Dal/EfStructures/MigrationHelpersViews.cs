using Microsoft.EntityFrameworkCore.Migrations;

namespace Nac.Dal.EfStructures;

public static class MigrationHelpersViews
{
    public static void CreateSellingsView(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql($@"
create view public.sellings_aggr_v as
select
	s.product_id,
	max(p.bar_code) bar_code,
	max(p.name) name,
	max(p.category) category, 
	max(p.group) ""group"",
	max(p.price) single_price,
	max(p.price_reduced) single_price_reduced,
	sum(s.quantity) sum_quantity,
	sum(s.price_manual) sum_price_manual,
	sum(s.weight) sum_weight,
	sum(s.final_price) sum_final_price,
	count(s.id) count
from
	public.sellings s
join 
	public.products p 
on
	s.product_id = p.id
group by
	s.product_id
;
"
        );
    }

    public static void DropSellingsView(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("drop view public.sellings_aggr_v;");
    }

}
