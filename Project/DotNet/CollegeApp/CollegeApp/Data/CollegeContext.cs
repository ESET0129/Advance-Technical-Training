using System;
using System.Collections.Generic;
using CollegeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data;

public partial class CollegeContext : DbContext
{
    public CollegeContext()
    {
    }

    public CollegeContext(DbContextOptions<CollegeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-2FHF9IC9;Initial Catalog=collegeproject;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Course__C92D71A7B95C0CA1");

            entity.ToTable("Course");

            entity.HasIndex(e => e.CourseCode, "UQ__Course__FC00E00001241466").IsUnique();

            entity.Property(e => e.CourseCode).HasMaxLength(20);
            entity.Property(e => e.CourseName).HasMaxLength(100);
            entity.Property(e => e.Department).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52B99A5D007BB");

            entity.ToTable("Student");

            entity.HasIndex(e => e.RollNumber, "UQ__Student__E9F06F1677D64849").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.RollNumber).HasMaxLength(20);

            entity.HasOne(d => d.Course).WithMany(p => p.Students)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Student__CourseI__4D94879B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C1A179CD7");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534F9E5CC40").IsUnique();

            entity.Property(e => e.UserId).HasMaxLength(128);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValue("User");
            entity.Property(e => e.UserName).HasMaxLength(128);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
