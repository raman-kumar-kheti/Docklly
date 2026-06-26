using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Docklly.Models
{
    [Index(nameof(Id), Name = "IX_Users_Id")]
    public class Users
    {
        [Key]
        public Guid Id { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string Name { get; set; }

        [Required]
        [Column("Password_Hash")]
        public string PasswordHash { get; set; }

        [Column("Is_Active")]
        public bool IsActive { get; set; }

        [Column("Create_At")]
        public DateTime CreatedAt { get; set; }

        [Column("Updated_At")]
        public DateTime UpdatedAt { get; set; }
    }
}