using Microsoft.EntityFrameworkCore.Migrations;

namespace Nac.Dal.EfStructures;

public static class MigrationHelpersViews
{
    public static void CreateL0QualityControlView(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql($@"
CREATE VIEW nac.v_l0_quality_controls AS
SELECT 
qc.id, 
qc.created, 
qc.description, 
qc.proc_result_status, 
qc.proc_time_start, 
qc.proc_time_end, 
qc.proc_error_id, 
qc.proc_dest_path,
qc.qc01_l0browse_status, 
qc.qc01_l0browse_description, 
qc.qc01_l0browse_operator, 
qc.qc01_l0browse_time, 
qc.qc02_l0sip_meta_status, 
qc.qc02_l0sip_meta_description, 
qc.qc02_l0sip_meta_operator, 
qc.qc02_l0sip_meta_time, 
qc.qc09_l0auto_all_files_status, 
qc.qc09_l0auto_all_files_description, 
qc.qc09_l0auto_all_files_operator, 
qc.qc09_l0auto_all_files_time, 
qc.qc10_l0auto_not_zero_status, 
qc.qc10_l0auto_not_zero_description, 
qc.qc10_l0auto_not_zero_operator, 
qc.qc10_l0auto_not_zero_time,
qc.l0_product_proc_id, 
l0.proc_result_status as l0_proc_result_status,
l0.proc_time_start as l0_proc_time_start,
l0.proc_time_end as l0_proc_time_end,
l0.proc_dest_path as l0_proc_dest_path,
l0.prec_proc_id as t_product_proc_id,
l0.path,
t.proc_result_status as t_proc_result_status,
t.pass_id,
p.mission,
p.stream,
p.acquisition_date,
p.tape_id,
p.orbit_number,
ta.tape_label
FROM ppas.l0_quality_controls AS qc
INNER JOIN (
    SELECT *
    FROM ppas.l0_products AS l
    WHERE NOT (l.is_deleted) --AND l.proc_result_status = 'success'::ppas.processing_result
) AS l0 ON qc.l0_product_proc_id = l0.proc_id
LEFT JOIN (
    SELECT *
    FROM ppas.t_products AS t1
    WHERE NOT (t1.is_deleted) --AND t1.proc_result_status = 'success'::ppas.processing_result
) AS t ON l0.prec_proc_id = t.proc_id
LEFT JOIN (
    SELECT *
    FROM ppas.passes AS p1
    WHERE NOT (p1.is_deleted)
) AS p ON t.pass_id = p.id
LEFT JOIN (
    SELECT ta1.id, ta1.tape_label
    FROM ppas.tapes AS ta1
    WHERE NOT (ta1.is_deleted)
) AS ta ON p.tape_id = ta.id
WHERE NOT (qc.is_deleted) 
"
        );
    }

    public static void CreateL1QualityControlView(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql($@"
CREATE VIEW nac.v_l1_quality_controls AS
SELECT 
qc.id, 
qc.created, 
qc.description, 
qc.proc_result_status, 
qc.proc_time_start, 
qc.proc_time_end, 
qc.proc_error_id, 
qc.proc_dest_path,
qc.qc03_l1browse_loc_status,
qc.qc03_l1browse_loc_time,
qc.qc03_l1browse_loc_operator,
qc.qc03_l1browse_loc_description,
qc.qc04_l1browse_img_err_status,
qc.qc04_l1browse_img_err_time,
qc.qc04_l1browse_img_err_operator,
qc.qc04_l1browse_img_err_description,
qc.qc05_l1prod_full_res_status,
qc.qc05_l1prod_full_res_time,
qc.qc05_l1prod_full_res_operator,
qc.qc05_l1prod_full_res_description,
qc.qc06_l1prod_meta_status,
qc.qc06_l1prod_meta_time,
qc.qc06_l1prod_meta_operator,
qc.qc06_l1prod_meta_description,
qc.qc07_l1sip_image_status,
qc.qc07_l1sip_image_time,
qc.qc07_l1sip_image_operator,
qc.qc07_l1sip_image_description,
qc.qc08_l1sip_meta_status,
qc.qc08_l1sip_meta_time,
qc.qc08_l1sip_meta_operator,
qc.qc08_l1sip_meta_description,
qc.qc11_l1auto_all_files_status,
qc.qc11_l1auto_all_files_time,
qc.qc11_l1auto_all_files_operator,
qc.qc11_l1auto_all_files_description,
qc.qc12_l1auto_not_zero_status,
qc.qc12_l1auto_not_zero_time,
qc.qc12_l1auto_not_zero_operator,
qc.qc12_l1auto_not_zero_description,
qc.l1_product_proc_id, 
l1.proc_result_status as l1_proc_result_status,
l1.proc_time_start as l1_proc_time_start,
l1.proc_time_end as l1_proc_time_end,
l1.proc_dest_path as l1_proc_dest_path,
l1.prec_proc_id as l0_product_proc_id,
l1.in_param_l1_sensor,
l1.path,
l0.proc_result_status as l0_proc_result_status,
l0.prec_proc_id as t_product_proc_id,
t.proc_result_status as t_proc_result_status,
t.pass_id,
p.mission,
p.stream,
p.acquisition_date,
p.tape_id,
p.orbit_number,
ta.tape_label
FROM ppas.l1_quality_controls AS qc
INNER JOIN (
    SELECT *
    FROM ppas.l1_products AS l11
    WHERE NOT (l11.is_deleted) --AND l11.proc_result_status = 'success'::ppas.processing_result
) AS l1 ON qc.l1_product_proc_id = l1.proc_id
LEFT JOIN (
    SELECT *
    FROM ppas.l0_products AS l00
    WHERE NOT (l00.is_deleted) --AND l00.proc_result_status = 'success'::ppas.processing_result
) AS l0 ON l1.prec_proc_id = l0.proc_id
LEFT JOIN (
    SELECT *
    FROM ppas.t_products AS t1
    WHERE NOT (t1.is_deleted) --AND t1.proc_result_status = 'success'::ppas.processing_result
) AS t ON l0.prec_proc_id = t.proc_id
LEFT JOIN (
    SELECT *
    FROM ppas.passes AS p1
    WHERE NOT (p1.is_deleted)
) AS p ON t.pass_id = p.id
LEFT JOIN (
    SELECT ta1.id, ta1.tape_label
    FROM ppas.tapes AS ta1
    WHERE NOT (ta1.is_deleted)
) AS ta ON p.tape_id = ta.id
WHERE NOT (qc.is_deleted) 
"
        );
    }

    public static void DropL0QualityControlView(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("DROP VIEW ppas.v_l0_quality_controls");
    }

    public static void DropL1QualityControlView(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("DROP VIEW ppas.v_l1_quality_controls");
    }

    public static void CreateTProductView(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql($@"
CREATE VIEW ppas.v_t_products AS
SELECT 
t.proc_id,
t.proc_result_status,
t.proc_error_id,
t.proc_time_start,
t.proc_time_end,
t.pass_id,
p.mission,
p.stream,
p.acquisition_date,
p.tape_id,
p.orbit_number,
ta.tape_label
FROM ppas.t_products AS t 
LEFT JOIN (
    SELECT *
    FROM ppas.passes AS p1
    WHERE NOT (p1.is_deleted)
) AS p ON t.pass_id = p.id
LEFT JOIN (
    SELECT ta1.id, ta1.tape_label
    FROM ppas.tapes AS ta1
    WHERE NOT (ta1.is_deleted)
) AS ta ON p.tape_id = ta.id
WHERE NOT (t.is_deleted)   
"
        );
    }

    public static void DropTProductView(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("DROP VIEW ppas.v_t_products");
    }

    public static void CreateL0ProductView(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql($@"
CREATE VIEW ppas.v_l0_products AS
SELECT 
l0.proc_id,
l0.proc_result_status,
l0.proc_error_id,
l0.proc_time_start,
l0.proc_time_end,
l0.prec_proc_id as t_product_proc_id,
l0.path,
t.pass_id,
p.mission,
p.stream,
p.acquisition_date,
p.tape_id,
p.orbit_number,
ta.tape_label
FROM ppas.l0_products AS l0   
LEFT JOIN (
    SELECT *
    FROM ppas.t_products AS t1
    WHERE NOT (t1.is_deleted) 
) AS t ON l0.prec_proc_id = t.proc_id
LEFT JOIN (
    SELECT *
    FROM ppas.passes AS p1
    WHERE NOT (p1.is_deleted)
) AS p ON t.pass_id = p.id
LEFT JOIN (
    SELECT ta1.id, ta1.tape_label
    FROM ppas.tapes AS ta1
    WHERE NOT (ta1.is_deleted)
) AS ta ON p.tape_id = ta.id
WHERE NOT (l0.is_deleted)  
"
        );
    }

    public static void DropL0ProductView(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("DROP VIEW ppas.v_l0_products");
    }

    public static void CreateL1ProductView(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql($@"
CREATE VIEW ppas.v_l1_products AS
SELECT 
l1.proc_id,
l1.proc_result_status,
l1.proc_error_id,
l1.proc_time_start,
l1.proc_time_end,
l1.prec_proc_id as l0_product_proc_id,
l1.in_param_l1_sensor,
l1.path,
l0.prec_proc_id as t_product_proc_id,
t.pass_id,
p.mission,
p.stream,
p.acquisition_date,
p.tape_id,
p.orbit_number,
ta.tape_label
FROM ppas.l1_products AS l1
LEFT JOIN (
    SELECT *
    FROM ppas.l0_products AS l00
    WHERE NOT (l00.is_deleted) 
) AS l0 ON l1.prec_proc_id = l0.proc_id
LEFT JOIN (
    SELECT *
    FROM ppas.t_products AS t1
    WHERE NOT (t1.is_deleted) 
) AS t ON l0.prec_proc_id = t.proc_id
LEFT JOIN (
    SELECT *
    FROM ppas.passes AS p1
    WHERE NOT (p1.is_deleted)
) AS p ON t.pass_id = p.id
LEFT JOIN (
    SELECT ta1.id, ta1.tape_label
    FROM ppas.tapes AS ta1
    WHERE NOT (ta1.is_deleted)
) AS ta ON p.tape_id = ta.id
WHERE NOT (l1.is_deleted)
"
        );
    }
    public static void DropL1ProductView(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("DROP VIEW ppas.v_l1_products");
    }
}
