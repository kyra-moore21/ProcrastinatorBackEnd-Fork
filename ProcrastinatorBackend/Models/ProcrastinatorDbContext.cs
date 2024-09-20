using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ProcrastinatorBackend.Models;

public partial class ProcrastinatorDbContext : DbContext
{
    public ProcrastinatorDbContext()
    {
    }

    public ProcrastinatorDbContext(DbContextOptions<ProcrastinatorDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Mealplanner> MealPlanners { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string? connectionString = Environment.GetEnvironmentVariable("connectionString")
                 ?? Secret.connectionString;
            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null. Please ensure the 'connectionString' environment variable is set.");
            }

            // Parse the connection string to ensure SSL is required
            var builder = new NpgsqlConnectionStringBuilder(connectionString)
            {
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };

            optionsBuilder.UseNpgsql(builder.ToString());
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mealplanner>(entity =>
        {
            entity.HasKey(e => e.Mealid).HasName("PK__MealPlan__ACF6A65D9E434BFF");

            entity.ToTable("mealplanner");

            entity.Property(e => e.Mealid).HasColumnName("mealid");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Url).HasMaxLength(255);
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Like).HasColumnName("Like");
            entity.Property(e => e.Iscompleted).HasColumnName("iscompleted");

            entity.HasOne(d => d.User).WithMany(p => p.Mealplanners)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MealPlann__UserI__4E88ABD4");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Taskid).HasName("PK__Task__7C6949D173E98E7B");

            entity.ToTable("task");

            entity.Property(e => e.Taskid).HasColumnName("taskid");
            entity.Property(e => e.Details).HasMaxLength(255);
            entity.Property(e => e.Task1)
                .HasMaxLength(255)
                .HasColumnName("task1");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Task__UserID__4BAC3F29");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("PK__User__1788CCACA98C3BF4");

            entity.ToTable("User");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Display).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Firstname).HasMaxLength(255);
            entity.Property(e => e.Lastname).HasMaxLength(255);
            entity.Property(e => e.Photourl).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
