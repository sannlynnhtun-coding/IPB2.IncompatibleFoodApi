using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IPB2.IncompatibleFoodApi.Database.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblA> TblAs { get; set; }

    public virtual DbSet<TblAccount> TblAccounts { get; set; }

    public virtual DbSet<TblB> TblBs { get; set; }

    public virtual DbSet<TblIncompatibleFood> TblIncompatibleFoods { get; set; }

    public virtual DbSet<TblStaff> TblStaffs { get; set; }

    public virtual DbSet<TblStudent> TblStudents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=InPersonBatch2;User Id=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblA>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_A");
        });

        modelBuilder.Entity<TblAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId);

            entity.ToTable("Tbl_Account");

            entity.Property(e => e.AccountId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Balance).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblB>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_B");
        });

        modelBuilder.Entity<TblIncompatibleFood>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Inco__3214EC07F5B2AEA1");

            entity.ToTable("Tbl_IncompatibleFood");

            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.FoodA).HasMaxLength(200);
            entity.Property(e => e.FoodB).HasMaxLength(200);
        });

        modelBuilder.Entity<TblStaff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Tbl_Staf__96D4AB170349F8FE");

            entity.ToTable("Tbl_Staff");

            entity.Property(e => e.StaffId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Department).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.JoinDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PhoneNo).HasMaxLength(20);
            entity.Property(e => e.Position).HasMaxLength(100);
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StaffCode).HasMaxLength(50);
            entity.Property(e => e.StaffName).HasMaxLength(150);
        });

        modelBuilder.Entity<TblStudent>(entity =>
        {
            entity.HasKey(e => e.StudentId);

            entity.ToTable("Tbl_Student");

            entity.Property(e => e.ClassNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fees).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ParentName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StudentName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
