using Nac.Models.Utilities;

namespace Nac.Models.Entities.Base;

public abstract class BaseEntity
{
    [Required]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    [Required]
    [JsonConverter(typeof(UtcDateTimeConverter))]
    public DateTime? Created { get; set; }

    [JsonConverter(typeof(UtcDateTimeConverter))]
    public DateTime? Modified { get; set; }

    [Required]
    [DisplayName("Is Synchronized")]
    public SyncStatus IsSychronized { get; set; } = SyncStatus.Local;

    [ConcurrencyCheck]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Column("xmin", TypeName = "xid")]
    public uint xmin { get; set; }

    [Required]
    [DisplayName("Is Deleted")]
    public bool IsDeleted { get; set; } = false;

}
