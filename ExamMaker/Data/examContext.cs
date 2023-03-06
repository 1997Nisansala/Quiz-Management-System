using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using exam.Models;

namespace exam.Data;

public partial class examContext : DbContext
{
    public examContext(DbContextOptions<examContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<Examresult> Examresults { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.ExamId).HasName("PRIMARY");

            entity.ToTable("exam");

            entity.HasIndex(e => e.TeacherId, "TeacherID");

            entity.Property(e => e.ExamId)
                .HasColumnType("int(11)")
                .HasColumnName("ExamID");
            entity.Property(e => e.Duration).HasColumnType("int(11)");
            entity.Property(e => e.ExamName).HasMaxLength(255);
            entity.Property(e => e.QuestionCount).HasColumnType("int(11)");
            entity.Property(e => e.StartTime).HasColumnType("time");
            entity.Property(e => e.TeacherId)
                .HasMaxLength(5)
                .HasColumnName("TeacherID");

            
        });

        modelBuilder.Entity<Examresult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PRIMARY");

            entity.ToTable("examresult");

            entity.HasIndex(e => e.ExamId, "ExamID");

            entity.HasIndex(e => e.StudentId, "StudentID");

            entity.Property(e => e.ResultId)
                .HasColumnType("int(11)")
                .HasColumnName("ResultID");
            entity.Property(e => e.Errors).HasColumnType("text");
            entity.Property(e => e.ExamId)
                .HasColumnType("int(11)")
                .HasColumnName("ExamID");
            entity.Property(e => e.StudentId)
                .HasMaxLength(5)
                .HasColumnName("StudentID");

            entity.HasOne(d => d.Exam).WithMany(p => p.Examresults)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("examresult_ibfk_2");

            entity.HasOne(d => d.Student).WithMany(p => p.Examresults)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("examresult_ibfk_1");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PRIMARY");

            entity.ToTable("question");

            entity.HasIndex(e => e.ExamId, "ExamID");

            entity.Property(e => e.QuestionId)
                .HasColumnType("int(11)")
                .HasColumnName("QuestionID");
            entity.Property(e => e.CorrectOption).HasColumnType("tinyint(4)");
            entity.Property(e => e.ExamId)
                .HasColumnType("int(11)")
                .HasColumnName("ExamID");
            entity.Property(e => e.Option1).HasMaxLength(255);
            entity.Property(e => e.Option2).HasMaxLength(255);
            entity.Property(e => e.Option3).HasMaxLength(255);
            entity.Property(e => e.Option4).HasMaxLength(255);
            entity.Property(e => e.QuestionText).HasColumnType("text");

            entity.HasOne(d => d.Exam).WithMany(p => p.Questions)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("question_ibfk_1");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PRIMARY");

            entity.ToTable("student");

            entity.Property(e => e.StudentId)
                .HasMaxLength(5)
                .HasColumnName("StudentID");
            entity.Property(e => e.StudentName).HasMaxLength(255);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("PRIMARY");

            entity.ToTable("teacher");

            entity.Property(e => e.TeacherId)
                .HasMaxLength(5)
                .HasColumnName("TeacherID");
            entity.Property(e => e.TeacherName).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
