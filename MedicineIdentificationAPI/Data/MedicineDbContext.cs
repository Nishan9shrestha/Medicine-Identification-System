using System;
using System.Collections.Generic;
using MedicineIdentificationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicineIdentificationAPI.Data;

public partial class MedicineDbContext : DbContext
{
    public MedicineDbContext()
    {
    }

    public MedicineDbContext(DbContextOptions<MedicineDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DosageSchedule> DosageSchedules { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<MedicineImage> MedicineImages { get; set; }

    public virtual DbSet<Prediction> Predictions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=nishan\\SQLEXPRESS01;Initial Catalog=MedicineIdentificationSystem;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DosageSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__DosageSc__9C8A5B49CC790D38");

            entity.Property(e => e.ScheduleId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DosageAmount).HasMaxLength(50);
            entity.Property(e => e.Frequency).HasMaxLength(20);
            entity.Property(e => e.Notes).HasColumnType("text");

            entity.HasOne(d => d.Medicine).WithMany(p => p.DosageSchedules)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK__DosageSch__Medic__5812160E");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDD6022339EC");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Comments).HasColumnType("text");
            entity.Property(e => e.SubmittedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Prediction).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.PredictionId)
                .HasConstraintName("FK__Feedback__Predic__5165187F");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Feedback__UserId__52593CB8");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Logs__5E548648418E64BA");

            entity.Property(e => e.LogId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Action).HasMaxLength(255);
            entity.Property(e => e.Details).HasColumnType("text");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Logs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Logs__UserId__5BE2A6F2");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.MedicineId).HasName("PK__Medicine__4F212890AD6EB650");

            entity.Property(e => e.MedicineId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Dosage).HasMaxLength(50);
            entity.Property(e => e.Manufacturer).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UsageInstructions).HasColumnType("text");
        });

        modelBuilder.Entity<MedicineImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Medicine__7516F70C6E302A06");

            entity.Property(e => e.ImageId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.ThumbnailUrl).HasMaxLength(255);
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Medicine).WithMany(p => p.MedicineImages)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK__MedicineI__Medic__440B1D61");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.MedicineImages)
                .HasForeignKey(d => d.UploadedBy)
                .HasConstraintName("FK__MedicineI__Uploa__44FF419A");
        });

        modelBuilder.Entity<Prediction>(entity =>
        {
            entity.HasKey(e => e.PredictionId).HasName("PK__Predicti__BAE4C1A0339A7039");

            entity.Property(e => e.PredictionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.IsConfirmed).HasDefaultValue(false);
            entity.Property(e => e.PredictedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Image).WithMany(p => p.Predictions)
                .HasForeignKey(d => d.ImageId)
                .HasConstraintName("FK__Predictio__Image__4AB81AF0");

            entity.HasOne(d => d.PredictedMedicine).WithMany(p => p.Predictions)
                .HasForeignKey(d => d.PredictedMedicineId)
                .HasConstraintName("FK__Predictio__Predi__4BAC3F29");

            entity.HasOne(d => d.User).WithMany(p => p.Predictions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Predictio__UserI__49C3F6B7");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C8D8C8D08");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E47707C144").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053462FE26A6").IsUnique();

            entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
