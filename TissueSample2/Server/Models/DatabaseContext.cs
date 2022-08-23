using TissueSample2.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace TissueSample2.Server.Models
{

    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        public DbSet<Collection> Collections { get; set; } = null!;
        public DbSet<Sample> Samples { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Collection>(entity =>
            {
                entity.ToTable("collections");
                entity.Property(e => e.c_id).HasColumnName("c_id");
                entity.Property(e => e.disease_term)
                    .HasMaxLength(500)
                    .HasColumnName("disease_term")
                    .IsUnicode(false);
                entity.Property(e => e.title)
                    .HasMaxLength(500)
                    .HasColumnName("title")
                    .IsUnicode(false);
                entity.Property(e => e.date)
                    .IsRequired()
                    .HasColumnName("date")
                    .HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<Sample>(entity =>
            {
                entity.ToTable("samples");
                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.c_id)
                    .HasColumnName("c_id")
                    .IsUnicode(false);
                entity.Property(e => e.donor_count)
                    .HasColumnName("donor_count")
                    .IsUnicode(false);
                entity.Property(e => e.mat_type)
                    .HasMaxLength(500)
                    .HasColumnName("mat_type")
                    .IsUnicode(false);
                entity.Property(e => e.date)
                    .IsRequired()
                    .HasColumnName("date")
                    .HasDefaultValueSql("GETDATE()");

            });


            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}