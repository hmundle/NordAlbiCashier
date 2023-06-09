//using System.Text.Json;

using Nac.Models.Utilities;

namespace Nac.Models.Entities;

[Table("seri_logs", Schema = "logging")]
public class SeriLogEntry
{
    //private const int VarCharLimit = 50;
    // we could use     [StringLength(VarCharLimit)] property to limit varchar,
    // but there is no performance benefit and no direct user input here.
    // But SeriLog PostgreSQL writer would fail, if a value is longer than limit.
    // In that case there won't be a log entry in DB.
    // Therefore we are avoiding varchar in this table

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? ApplicationName { get; set; }
    public string? Message { get; set; }
    public string? Level { get; set; }
    [JsonConverter(typeof(UtcDateTimeConverter))]
    public DateTime? RaiseDate { get; set; }
    [JsonConverter(typeof(UtcDateTimeConverter))]
    public DateTime? DbTimestamp { get; set; }
    public string? Exception { get; set; }
    [Column(TypeName = "jsonb")]
    public string? Properties { get; set; }
    public string? MachineName { get; set; }
    public string? FilePath { get; set; }
    public string? MemberName { get; set; }
    public int? LineNumber { get; set; }
    // enable the following line, when we start using this entity object in code
    //[NotMapped]
    //public JsonDocument? PropertiesJson => (Properties != null) ? JsonDocument.Parse(Properties) : null;

}
