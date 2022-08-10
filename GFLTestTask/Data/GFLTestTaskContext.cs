using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GFLTestTask.Models;

namespace GFLTestTask.Data
{
    public class GFLTestTaskContext : DbContext
    {
        public GFLTestTaskContext (DbContextOptions<GFLTestTaskContext> options)
            : base(options)
        {
        }

        public DbSet<GFLTestTask.Models.FileSystemModel> FileSystemModel { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileSystemModel>()
                .HasMany(f => f.Subfolders)
                .WithOne(f => f.Parent)
                .HasForeignKey(f => f.ParentId);
        }
    }
}
