using Microsoft.EntityFrameworkCore.Migrations;

namespace Nac.Dal.EfStructures;

public static class MigrationHelpers
{
    public static void DropAllDbTypes(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .OldAnnotation("Npgsql:Enum:ppas.delivery_type", "esa,nrsc")
            .OldAnnotation("Npgsql:Enum:ppas.mission_identifier", "irs_1c,irs_1d")
            .OldAnnotation("Npgsql:Enum:ppas.proc_error_status", "opened,reported,fixed")
            .OldAnnotation("Npgsql:Enum:ppas.processing_result", "undefined,success,failed,terminated")
            .OldAnnotation("Npgsql:Enum:ppas.processing_type", "undefined,transcription,l0,l1,l2,l0_qc,l0_sip,l0_qc_sip,l0_deliv,l0_arch,l1_qc,l1_sip,l1_qc_sip,l1_deliv,l1_arch")
            .OldAnnotation("Npgsql:Enum:ppas.product_category", "transcription,l0,l1,l2")
            .OldAnnotation("Npgsql:Enum:ppas.qc_id_system_sensor_type", "undefined,qc01_1c_p,qc01_1c_lw,qc01_1d_p,qc01_1d_lw,qc02_1c_p,qc02_1c_lw,qc02_1d_p,qc02_1d_lw,qc03_1c_p,qc03_1c_l,qc03_1c_w,qc03_1d_p,qc03_1d_l,qc03_1d_w,qc04_1c_p,qc04_1c_l,qc04_1c_w,qc04_1d_p,qc04_1d_l,qc04_1d_w,qc05_1c_p,qc05_1c_l,qc05_1c_w,qc05_1d_p,qc05_1d_l,qc05_1d_w,qc06_1c_p,qc06_1c_l,qc06_1c_w,qc06_1d_p,qc06_1d_l,qc06_1d_w,qc07_1c_p,qc07_1c_l,qc07_1c_w,qc07_1d_p,qc07_1d_l,qc07_1d_w,qc08_1c_p,qc08_1c_l,qc08_1c_w,qc08_1d_p,qc08_1d_l,qc08_1d_w,qc09_1c_p,qc09_1c_lw,qc09_1d_p,qc09_1d_lw,qc10_1c_p,qc10_1c_lw,qc10_1d_p,qc10_1d_lw,qc11_1c_p,qc11_1c_l,qc11_1c_w,qc11_1d_p,qc11_1d_l,qc11_1d_w,qc12_1c_p,qc12_1c_l,qc12_1c_w,qc12_1d_p,qc12_1d_l,qc12_1d_w")
            .OldAnnotation("Npgsql:Enum:ppas.quadrant_type", "undefined,a,b,c,d,_00")
            .OldAnnotation("Npgsql:Enum:ppas.quality_control_status", "undefined,open,in_progress,passed,failed,no_check,failed_clarified")
            .OldAnnotation("Npgsql:Enum:ppas.sensor_head_type", "undefined,a,b")
            .OldAnnotation("Npgsql:Enum:ppas.sensor_type", "pan,liss_iii,wifs")
            .OldAnnotation("Npgsql:Enum:ppas.stream_type", "pan,liss_iii_wifs")
            .OldAnnotation("Npgsql:Enum:ppas.sub_scene_type", "undefined,l,m,r")
            .OldAnnotation("Npgsql:Enum:ppas.system_sensor_type", "undefined,_1c_p,_1c_l,_1c_w,_1d_p,_1d_l,_1d_w");
    }

    private static string FormatDeleteAllDataSql(string TableName)
    {
        return $"DELETE FROM ppas.\"{TableName}\";";
    }





}
