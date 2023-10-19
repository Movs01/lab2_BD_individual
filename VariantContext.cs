using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lab2_BD_individual;

public partial class VariantContext : DbContext
{
    public VariantContext()
    {
    }

    public VariantContext(DbContextOptions<VariantContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BorrowedBook> BorrowedBooks { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\Users\\emil\\variant.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BorrowedBook>(entity =>
        {
            entity.HasKey(e => e.BookCode);

            entity.Property(e => e.BookCode).ValueGeneratedNever();
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            entity.HasOne(d => d.Employee).WithMany(p => p.BorrowedBooks).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            entity.HasOne(d => d.PositionCodeNavigation).WithMany(p => p.Employees).HasForeignKey(d => d.PositionCode);
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionsCode);

            entity.Property(e => e.PositionsCode).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
