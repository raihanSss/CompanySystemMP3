using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CompanySystem.Models;

public partial class CompanyContext : DbContext
{
    public CompanyContext()
    {
    }

    public CompanyContext(DbContextOptions<CompanyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departement> Departements { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Workson> Worksons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=PostgreSQLConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departement>(entity =>
        {
            entity.HasKey(e => e.Deptno).HasName("departement_pkey");

            entity.HasOne(d => d.MgrempnoNavigation).WithMany(p => p.Departements)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_mgrempno");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Empno).HasName("employee_pkey");

            entity.HasOne(d => d.DeptnoNavigation).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_deptno");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Projno).HasName("project_pkey");

            entity.HasOne(d => d.DeptnoNavigation).WithMany(p => p.Projects)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("project_deptno_fkey");
        });

        modelBuilder.Entity<Workson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workson_pkey");

            entity.HasOne(d => d.EmpnoNavigation).WithMany(p => p.Worksons)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workson_empno_fkey");

            entity.HasOne(d => d.ProjnoNavigation).WithMany(p => p.Worksons)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workson_projno_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
