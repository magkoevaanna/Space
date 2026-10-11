using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Entities;

public class SolarFlare
{
    [Key]
    [Column("flrID")] 
    public string FlrID { get; set; } = string.Empty;

    [Column("catalog")]
    public string? Catalog { get; set; }

    [Column("instruments")]
    public string? Instruments { get; set; }

    [Column("beginTime")]
    public DateTime? BeginTime { get; set; }

    [Column("peakTime")]
    public DateTime? PeakTime { get; set; }

    [Column("endTime")]
    public DateTime? EndTime { get; set; }

    [Column("classType")]
    public string? ClassType { get; set; }

    [Column("sourceLocation")]
    public string? SourceLocation { get; set; }

    [Column("activeRegionNum")]
    public int? ActiveRegionNum { get; set; }

    [Column("note")]
    public string? Note { get; set; }

    [Column("link")]
    public string? Link { get; set; }

    [Column("linkedEvents")]
    public string? LinkedEvents { get; set; }

    [Column("sentNotifications")]
    public string? SentNotifications { get; set; }

    [Column("submissionTime")]
    public DateTime? SubmissionTime { get; set; }

    [Column("versionId")]
    public int? VersionId { get; set; }
}
