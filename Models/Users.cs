using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Docklly.Models
{
    /// <summary>
    /// Represents a user in the Docklly system with role-based access
    /// </summary>
    [Index(nameof(Email), Name = "IX_Users_Email", IsUnique = true)]
    [Index(nameof(Id), Name = "IX_Users_Id")]
    [Table("Users")]
    public class Users
    {
        [Key]
        public Guid Id { get; set; }

        [EmailAddress]
        [Required]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column("PasswordHash")]
        [StringLength(512)]
        public string PasswordHash { get; set; } = string.Empty;

        [Column("Role")]
        [StringLength(50)]
        public string Role { get; set; } = "Viewer"; // Admin, Attorney, Paralegal, Reviewer, Viewer

        [Column("Department")]
        [StringLength(100)]
        public string? Department { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; } = true;

        [Column("EmailVerified")]
        public bool EmailVerified { get; set; } = false;

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("UpdatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("LastLoginAt")]
        public DateTime? LastLoginAt { get; set; }

        // Navigation properties
        public ICollection<AuditLog>? AuditLogs { get; set; } = new List<AuditLog>();
        public ICollection<DocumentAccess>? DocumentAccesses { get; set; } = new List<DocumentAccess>();
    }
}
