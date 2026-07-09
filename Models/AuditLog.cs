using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docklly.Models
{
    /// <summary>
    /// Maintains complete audit trail for compliance and security
    /// </summary>
    [Table("AuditLogs")]
    public class AuditLog
    {
        [Key]
        public Guid LogId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Action { get; set; } = string.Empty; // Create, View, Edit, Delete, Approve

        [StringLength(100)]
        public string EntityType { get; set; } = string.Empty; // Document, User, etc.

        public Guid? EntityId { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Details { get; set; }

        [StringLength(50)]
        public string? IpAddress { get; set; }

        [Column("Timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [StringLength(100)]
        public string Status { get; set; } = "Success"; // Success, Failure

        // Foreign key
        [ForeignKey(nameof(UserId))]
        public Users? User { get; set; }
    }
}
