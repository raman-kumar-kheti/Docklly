using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docklly.Models
{
    /// <summary>
    /// Stores compliance check results for documents
    /// </summary>
    [Table("ComplianceChecks")]
    public class ComplianceCheck
    {
        [Key]
        public Guid CheckId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid DocumentId { get; set; }

        [StringLength(100)]
        public string RuleType { get; set; } = string.Empty; // GDPR, HIPAA, SOX, etc.

        [StringLength(100)]
        public string Status { get; set; } = "Pending"; // Passed, Failed, Warning

        [Column(TypeName = "nvarchar(max)")]
        public string? Findings { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Recommendations { get; set; }

        public int ComplianceScore { get; set; } = 0; // 0-100

        [Column("CheckedAt")]
        public DateTime CheckedAt { get; set; } = DateTime.UtcNow;

        // Foreign key
        [ForeignKey(nameof(DocumentId))]
        public Document? Document { get; set; }
    }
}
