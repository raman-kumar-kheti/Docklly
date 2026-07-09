using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docklly.Models
{
    /// <summary>
    /// Controls document access permissions for users
    /// </summary>
    [Table("DocumentAccess")]
    public class DocumentAccess
    {
        [Key]
        public Guid AccessId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid DocumentId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [StringLength(50)]
        public string AccessLevel { get; set; } = "View"; // View, Edit, Approve, Admin

        [Column("GrantedAt")]
        public DateTime GrantedAt { get; set; } = DateTime.UtcNow;

        [Column("ExpiresAt")]
        public DateTime? ExpiresAt { get; set; }

        [StringLength(500)]
        public string? GrantReason { get; set; }

        // Foreign keys
        [ForeignKey(nameof(DocumentId))]
        public Document? Document { get; set; }

        [ForeignKey(nameof(UserId))]
        public Users? User { get; set; }
    }
}
