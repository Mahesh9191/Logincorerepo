using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CorePractice.Models;

public partial class LogInDbContext : DbContext
{
    public LogInDbContext()
    {
    }

    public LogInDbContext(DbContextOptions<LogInDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LogIn> LogIns { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogIn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LogIn__3214EC27F62A7272");

            entity.ToTable("LogIn");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
