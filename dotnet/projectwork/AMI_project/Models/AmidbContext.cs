using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AMI_project.Models;

public partial class AmidbContext : DbContext
{
    public AmidbContext()
    {
    }

    public AmidbContext(DbContextOptions<AmidbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Consumer> Consumers { get; set; }

    public virtual DbSet<DailyReading> DailyReadings { get; set; }

    public virtual DbSet<Meter> Meters { get; set; }

    public virtual DbSet<MonthlyBill> MonthlyBills { get; set; }

    public virtual DbSet<OrgUnit> OrgUnits { get; set; }

    public virtual DbSet<Tariff> Tariffs { get; set; }

    public virtual DbSet<TariffSlab> TariffSlabs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-2FHF9IC9;Initial Catalog=AMIdb;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Consumer>(entity =>
        {
            entity.HasKey(e => e.ConsumerId).HasName("PK__Consumer__63BBE9BA35B2C1EE");

            entity.ToTable("Consumer", "ami");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Active");
            entity.Property(e => e.UpdatedAt).HasPrecision(3);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ConsumerCreatedByNavigations)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Consumer__Create__49C3F6B7");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ConsumerUpdatedByNavigations)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK__Consumer__Update__4AB81AF0");
        });

        modelBuilder.Entity<DailyReading>(entity =>
        {
            entity.HasKey(e => e.ReadingId).HasName("PK__DailyRea__C80F9C4EAF8002CD");

            entity.ToTable("DailyReading", "ami");

            entity.HasIndex(e => e.MeterSerialNo, "IX_DailyReading_MeterSerialNo");

            entity.HasIndex(e => e.ReadingDate, "IX_DailyReading_ReadingDate");

            entity.HasIndex(e => new { e.MeterSerialNo, e.ReadingDate }, "UQ_Meter_Date").IsUnique();

            entity.Property(e => e.MeterSerialNo).HasMaxLength(50);
            entity.Property(e => e.ReadingKwh).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.MeterSerialNoNavigation).WithMany(p => p.DailyReadings)
                .HasForeignKey(d => d.MeterSerialNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DailyRead__Meter__571DF1D5");
        });

        modelBuilder.Entity<Meter>(entity =>
        {
            entity.HasKey(e => e.MeterSerialNo).HasName("PK__Meter__5C498B0FBEC7D390");

            entity.ToTable("Meter", "ami");

            entity.HasIndex(e => e.ConsumerId, "IX_Meter_Consumer");

            entity.HasIndex(e => e.OrgUnitId, "IX_Meter_OrgUnit");

            entity.HasIndex(e => e.TariffId, "IX_Meter_Tariff");

            entity.HasIndex(e => e.Iccid, "UQ__Meter__8A69BC4CF3BAE595").IsUnique();

            entity.HasIndex(e => e.Imsi, "UQ__Meter__8DF3A70E6A2224E2").IsUnique();

            entity.Property(e => e.MeterSerialNo).HasMaxLength(50);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Firmware).HasMaxLength(50);
            entity.Property(e => e.Iccid)
                .HasMaxLength(30)
                .HasColumnName("ICCID");
            entity.Property(e => e.Imsi)
                .HasMaxLength(30)
                .HasColumnName("IMSI");
            entity.Property(e => e.InstallTsUtc).HasPrecision(3);
            entity.Property(e => e.IpAddress).HasMaxLength(45);
            entity.Property(e => e.Manufacturer).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Active");

            entity.HasOne(d => d.Consumer).WithMany(p => p.Meters)
                .HasForeignKey(d => d.ConsumerId)
                .HasConstraintName("FK__Meter__ConsumerI__5165187F");

            entity.HasOne(d => d.OrgUnit).WithMany(p => p.Meters)
                .HasForeignKey(d => d.OrgUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Meter__OrgUnitId__52593CB8");

            entity.HasOne(d => d.Tariff).WithMany(p => p.Meters)
                .HasForeignKey(d => d.TariffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Meter__TariffId__534D60F1");
        });

        modelBuilder.Entity<MonthlyBill>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("PK__MonthlyB__11F2FC6AF74A1579");

            entity.ToTable("MonthlyBill", "ami");

            entity.Property(e => e.BaseRateApplied).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.GeneratedAt)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.MeterSerialNo).HasMaxLength(50);
            entity.Property(e => e.SlabCharge).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Unpaid");
            entity.Property(e => e.TotalBillAmount).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.TotalKwh).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.MeterSerialNoNavigation).WithMany(p => p.MonthlyBills)
                .HasForeignKey(d => d.MeterSerialNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MonthlyBi__Meter__59FA5E80");
        });

        modelBuilder.Entity<OrgUnit>(entity =>
        {
            entity.HasKey(e => e.OrgUnitId).HasName("PK__OrgUnit__4A793BEE73B2D6BA");

            entity.ToTable("OrgUnit", "ami");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_OrgUnit_Parent");
        });

        modelBuilder.Entity<Tariff>(entity =>
        {
            entity.HasKey(e => e.TariffId).HasName("PK__Tariff__EBAF9DB340C093E2");

            entity.ToTable("Tariff", "ami");

            entity.Property(e => e.BaseRate).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.TaxRate).HasColumnType("decimal(18, 4)");
        });

        modelBuilder.Entity<TariffSlab>(entity =>
        {
            entity.HasKey(e => e.TariffSlabId).HasName("PK__TariffSl__64EAAA229892BE4D");

            entity.ToTable("TariffSlab", "ami");

            entity.Property(e => e.FromKwh).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.RatePerKwh).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.ToKwh).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.Tariff).WithMany(p => p.TariffSlabs)
                .HasForeignKey(d => d.TariffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TariffSla__Tarif__4316F928");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC4DE8006A");

            entity.ToTable("Users", "ami");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E41F6C5678").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105346FCD4103").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.DisplayName).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastLogin).HasPrecision(3);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
