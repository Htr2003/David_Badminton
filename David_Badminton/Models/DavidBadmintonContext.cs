using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace David_Badminton.Models;

public partial class DavidBadmintonContext : DbContext
{
    public DavidBadmintonContext()  
    {
    }

    public DavidBadmintonContext(DbContextOptions<DavidBadmintonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Coach> Coachs { get; set; }

    public virtual DbSet<Facility> Facilitys { get; set; }

    public virtual DbSet<LearningProcess> LearningProcesses { get; set; }

    public virtual DbSet<ModuleAccess> ModuleAccesses { get; set; }

    public virtual DbSet<RollCall> RollCalls { get; set; }

    public virtual DbSet<RollCallCoach> RollCallCoachs { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Time> Times { get; set; }

    public virtual DbSet<TypeUser> TypeUsers { get; set; }

    public virtual DbSet<UserModule> UserModules { get; set; }

    //-------------------------------
    public virtual DbSet<Tuitions> Tuitions { get; set; }
    //-------------------------------

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=BADMINTON;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coach>(entity =>
        {
            entity.Property(e => e.CoachId).HasColumnName("CoachID");
            entity.Property(e => e.BankName).HasMaxLength(500);
            entity.Property(e => e.BankNumber).HasMaxLength(500);
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.Cccd)
                .HasMaxLength(500)
                .HasColumnName("CCCD");
            entity.Property(e => e.CoachName).HasMaxLength(500);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(500);
            entity.Property(e => e.Experience).HasMaxLength(500);
            entity.Property(e => e.FacilityId).HasColumnName("FacilityID");
            entity.Property(e => e.GenderId).HasColumnName("GenderID");
            entity.Property(e => e.HealthCondition).HasMaxLength(500);
            entity.Property(e => e.Images).HasMaxLength(500);
            entity.Property(e => e.NamePerson).HasMaxLength(500);
            entity.Property(e => e.Password).HasMaxLength(500);
            entity.Property(e => e.Phone).HasMaxLength(500);
            entity.Property(e => e.PhoneNumber).HasMaxLength(500);
            entity.Property(e => e.PlaceOfOrigin).HasMaxLength(500);
            entity.Property(e => e.PlaceOfResidence).HasMaxLength(500);
            entity.Property(e => e.Relationship).HasMaxLength(500);
            entity.Property(e => e.Salary).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.TaxCode).HasMaxLength(500);
            entity.Property(e => e.TypeUserId).HasColumnName("TypeUserID");
            entity.Property(e => e.UserCreated).HasMaxLength(500);
            entity.Property(e => e.UserUpdated).HasMaxLength(500);
            entity.Property(e => e.WorkingStart).HasColumnType("datetime");

            entity.HasOne(d => d.Facility).WithMany(p => p.Coaches)
                .HasForeignKey(d => d.FacilityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Coachs_Facilitys");

            entity.HasOne(d => d.TypeUser).WithMany(p => p.Coaches)
                .HasForeignKey(d => d.TypeUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Coachs_TypeUsers");
        });

        modelBuilder.Entity<Facility>(entity =>
        {
            entity.Property(e => e.FacilityId).HasColumnName("FacilityID");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.FacilityName).HasMaxLength(500);
            entity.Property(e => e.Latitude).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.Longtitude).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.UserCreated).HasMaxLength(500);
            entity.Property(e => e.UserUpdated).HasMaxLength(500);
        });

        modelBuilder.Entity<LearningProcess>(entity =>
        {
            entity.ToTable("LearningProcess");

            entity.Property(e => e.LearningProcessId).HasColumnName("LearningProcessID");
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.ImagesPath).HasMaxLength(500);
            entity.Property(e => e.ImagesThumb).HasMaxLength(500);
            entity.Property(e => e.LinkWebsite).HasMaxLength(500);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Title).HasMaxLength(500);
        });

        modelBuilder.Entity<ModuleAccess>(entity =>
        {
            entity.HasKey(e => e.ModuleId);

            entity.ToTable("ModuleAccess");

            entity.Property(e => e.ModuleId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ModuleID");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.UserCreated).HasMaxLength(500);
            entity.Property(e => e.UserUpdated).HasMaxLength(500);
        });

        modelBuilder.Entity<RollCall>(entity =>
        {
            entity.HasKey(e => e.RollCallId).HasName("PK_RollCall");

            entity.Property(e => e.RollCallId).HasColumnName("RollCallID");
            entity.Property(e => e.CoachId).HasColumnName("CoachID");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.IsCheck).HasColumnName("isCheck");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.UserCreated).HasMaxLength(500);
            entity.Property(e => e.UserUpdated).HasMaxLength(500);

            entity.HasOne(d => d.Coach).WithMany(p => p.RollCalls)
                .HasForeignKey(d => d.CoachId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RollCalls_Coachs");

            entity.HasOne(d => d.Student).WithMany(p => p.RollCalls)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RollCall_Students");
        });

        modelBuilder.Entity<RollCallCoach>(entity =>
        {
            entity.Property(e => e.RollCallCoachId).HasColumnName("RollCallCoachID");
            entity.Property(e => e.CoachId).HasColumnName("CoachID");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.IsCheck).HasColumnName("isCheck");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.UserCreated).HasMaxLength(500);
            entity.Property(e => e.UserUpdated).HasMaxLength(500);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.CoachId).HasColumnName("CoachID");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(500);
            entity.Property(e => e.FacilityId).HasColumnName("FacilityID");
            entity.Property(e => e.GenderId).HasColumnName("GenderID");
            entity.Property(e => e.GuardianName).HasMaxLength(500);
            entity.Property(e => e.GuardianPhone).HasMaxLength(500);
            entity.Property(e => e.HealthCondition).HasMaxLength(500);
            entity.Property(e => e.Height).HasMaxLength(500);
            entity.Property(e => e.Images).HasMaxLength(500);
            entity.Property(e => e.Password).HasMaxLength(500);
            entity.Property(e => e.Phone).HasMaxLength(500);
            entity.Property(e => e.Relationship).HasMaxLength(500);
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StudentName).HasMaxLength(500);
            entity.Property(e => e.StudyStart).HasColumnType("datetime");
            entity.Property(e => e.TimeId).HasColumnName("TimeID");
            entity.Property(e => e.TypeUserId).HasColumnName("TypeUserID");
            entity.Property(e => e.UserCreated).HasMaxLength(500);
            entity.Property(e => e.UserUpdated).HasMaxLength(500);
            entity.Property(e => e.Weight).HasMaxLength(500);

            entity.HasOne(d => d.Time).WithMany(p => p.Students)
                .HasForeignKey(d => d.TimeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_Time");

            entity.HasOne(d => d.TypeUser).WithMany(p => p.Students)
                .HasForeignKey(d => d.TypeUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_TypeUsers");
        });

        modelBuilder.Entity<Time>(entity =>
        {
            entity.ToTable("Time");

            entity.Property(e => e.TimeId).HasColumnName("TimeID");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.TimeName).HasMaxLength(500);
            entity.Property(e => e.UserCreated).HasMaxLength(500);
            entity.Property(e => e.UserUpdated).HasMaxLength(500);
        });

        modelBuilder.Entity<TypeUser>(entity =>
        {
            entity.Property(e => e.TypeUserId).HasColumnName("TypeUserID");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.UserName).HasMaxLength(500);
        });

        //----------------------------------------------
        modelBuilder.Entity<Tuitions>(entity =>
        {
            entity.HasKey(e => e.TuitionId);

            entity.Property(e => e.TuitionId).HasColumnName("TuitionID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.IsCheck).HasColumnName("isCheck");
            entity.Property(e => e.IsNull).HasColumnName("isNull");
            entity.Property(e => e.UserCreated).HasMaxLength(500);
            entity.Property(e => e.UserUpdated).HasMaxLength(500);
        });
        //----------------------------------------------

        modelBuilder.Entity<UserModule>(entity =>
        {
            entity.Property(e => e.UserModuleId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("UserModuleID");
            entity.Property(e => e.CoachId).HasColumnName("CoachID");
            entity.Property(e => e.ModuleId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ModuleID");

            entity.HasOne(d => d.Coach).WithMany(p => p.UserModules)
                .HasForeignKey(d => d.CoachId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserModules_Coachs");

            entity.HasOne(d => d.Module).WithMany(p => p.UserModules)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserModules_ModuleAccess");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
