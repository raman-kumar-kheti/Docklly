using System;
using Docklly.Models;
using Microsoft.EntityFrameworkCore;

namespace Docklly.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
        public DbSet<Users> Users { get; set; }
    }
    
}