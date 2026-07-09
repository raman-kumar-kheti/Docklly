using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Docklly.Models
{
    /// <summary>
    /// Represents a legal document with metadata and AI processing
    /// </summary>
    [Index(nameof(DocumentType), Name = "IX_Documents_Type")]
    [Index(nameof(Status), Name = "IX_Documents_Status")]
    [Index(nameof(CreatedAt), Name = "IX_Documents_CreatedAt")]
    [Table("Documents")]
    public class Document
    {
        [Key]
        public Guid DocumentId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(500)]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string Content { get; set; } = string.Empty;

        [StringLength(100)]
        public string DocumentType { get; set; } = string.Empty; // Contract, Agreement, NDA, etc.

        [StringLength(50)]
        public string Status { get; set; } = "Draft"; // Draft, Review, Approved, Archived

        [Column(TypeName = "nvarchar(max)")]
        public string? Metadata { get; set; } // JSON metadata

        [StringLength(500)]
        public string? Tags { get; set; }

        public Guid CreatedBy { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("UpdatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Guid? LastModifiedBy { get; set; }

        [Column("IsEncrypted")]
        public bool IsEncrypted { get; set; } = false;

        [StringLength(100)]
        public string? FileHash { get; set; }

        public int VersionNumber { get; set; } = 1;

        // Navigation properties
        public ICollection<DocumentVersion>? Versions { get; set; } = new List<DocumentVersion>();
        public ICollection<ComplianceCheck>? ComplianceChecks { get; set; } = new List<ComplianceCheck>();
        public ICollection<AiAnalysis>? AiAnalyses { get; set; } = new List<AiAnalysis>();
        public ICollection<DocumentAccess>? AccessControls { get; set; } = new List<DocumentAccess>();
    }
}
