using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docklly.Models
{
    /// <summary>
    /// Maintains version history of documents
    /// </summary>
    [Table("DocumentVersions")]
    public class DocumentVersion
    {
        [Key]
        public Guid VersionId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid DocumentId { get; set; }

        public int VersionNumber { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Content { get; set; } = string.Empty;

        public Guid ChangedBy { get; set; }

        [Column("ChangedAt")]
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        [StringLength(500)]
        public string? ChangeNotes { get; set; }

        [StringLength(100)]
        public string? ChangeType { get; set; } // Create, Update, Review, Approve

        // Foreign key
        [ForeignKey(nameof(DocumentId))]
        public Document? Document { get; set; }
    }
}
