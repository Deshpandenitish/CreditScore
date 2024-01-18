using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CreditScore.Models.Entities;

public partial class CreditScoreContext : DbContext
{
    public CreditScoreContext()
    {
    }

    public CreditScoreContext(DbContextOptions<CreditScoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditTrail> AuditTrails { get; set; }

    public virtual DbSet<CreditScore> CreditScores { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<FinancialDetail> FinancialDetails { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=5400-TI11989\\MSSQLSERVER01;Initial Catalog=CreditScore;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditTrail>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK__AuditTra__A17F23B8E22932AD");

            entity.ToTable("AuditTrail");

            entity.Property(e => e.AuditId).HasColumnName("AuditID");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.AuditTrails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__AuditTrai__UserI__5070F446");
        });

        modelBuilder.Entity<CreditScore>(entity =>
        {
            entity.HasKey(e => e.CreditId).HasName("PK__CreditSc__ED5ED0BB2F180D49");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.CreditScore1).HasColumnName("CreditScore");
            entity.Property(e => e.DebtToIncomeRatio).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.CreditScores)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__CreditSco__UserI__4222D4EF");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF6FB0041C27");

            entity.Property(e => e.DocumentId).HasColumnName("DocumentID");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Documents)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Documents__UserI__4BAC3F29");
        });

        modelBuilder.Entity<FinancialDetail>(entity =>
        {
            entity.HasKey(e => e.Fid).HasName("PK__Financia__C1D1314ABFBA3B13");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.Expenses).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.Income).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.FinancialDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Financial__UserI__46E78A0C");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E12D627FE4D");

            entity.Property(e => e.NoteStatus)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NotificationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NotificationName).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__UserI__5535A963");
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PK__Tokens__658FEE8A6AA18A2F");

            entity.Property(e => e.TokenId).HasColumnName("TokenID");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.ExpiryDateTime).HasColumnType("datetime");
            entity.Property(e => e.TokenValue).HasMaxLength(255);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Tokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Tokens__UserID__3D5E1FD2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC33A8AC20");

            entity.HasIndex(e => e.Email, "UC_Users_Email").IsUnique();

            entity.HasIndex(e => e.Username, "UC_Users_Username").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
