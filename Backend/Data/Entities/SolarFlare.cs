using System.ComponentModel.DataAnnotations;

namespace Backend.Data.Entities;

public class SolarFlare
{
    [Key]
    public string FlrID { get; set; } = string.Empty;
    public string? Catalog { get; set; }
    public string? Instruments { get; set; } // JSON
    public DateTime? BeginTime { get; set; }
    public DateTime? PeakTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? ClassType { get; set; }
    public string? SourceLocation { get; set; }
    public int? ActiveRegionNum { get; set; }
    public string? Note { get; set; }
    public string? Link { get; set; }
    public string? LinkedEvents { get; set; } // JSON
    public string? SentNotifications { get; set; }
    public DateTime? SubmissionTime { get; set; }
    public int? VersionId { get; set; }
}
