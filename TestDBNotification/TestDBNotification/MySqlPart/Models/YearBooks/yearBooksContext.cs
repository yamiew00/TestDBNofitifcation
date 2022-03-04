using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TestDBNotification.MySqlPart.Models.YearBooks
{
    public partial class yearBooksContext : DbContext
    {
        public yearBooksContext()
        {
        }

        public yearBooksContext(DbContextOptions<yearBooksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookList> BookLists { get; set; }
        public virtual DbSet<BookMeta109> BookMeta109s { get; set; }
        public virtual DbSet<BookMeta110> BookMeta110s { get; set; }
        public virtual DbSet<BookMeta111> BookMeta111s { get; set; }
        public virtual DbSet<Chapter109> Chapter109s { get; set; }
        public virtual DbSet<Chapter110> Chapter110s { get; set; }
        public virtual DbSet<Chapter111> Chapter111s { get; set; }
        public virtual DbSet<EduBook> EduBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=35.236.136.171;port=3306;user id=nani-back-end;password=exbnXQy?D#h5DzHK;database=yearBooks", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.18-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<BookList>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BookList");

                entity.HasIndex(e => e.BookId, "BookID")
                    .IsUnique();

                entity.Property(e => e.BookId)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("bookID")
                    .IsFixedLength(true);

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime");

                entity.Property(e => e.Curriculum)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("curriculum")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.EduSubject)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("eduSubject")
                    .IsFixedLength(true);

                entity.Property(e => e.IsLock)
                    .HasColumnType("bit(1)")
                    .HasColumnName("isLock")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.LockMaintainer)
                    .HasMaxLength(50)
                    .HasColumnName("lockMaintainer");

                entity.Property(e => e.Maintainer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("maintainer")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.MaintainerName)
                    .HasMaxLength(50)
                    .HasColumnName("maintainerName");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("updateTime");

                entity.Property(e => e.Year)
                    .HasColumnType("int(11)")
                    .HasColumnName("year");
            });

            modelBuilder.Entity<BookMeta109>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BookMeta109");

                entity.HasIndex(e => new { e.BookId, e.MetaUid }, "BookMeta")
                    .IsUnique();

                entity.HasIndex(e => e.Uid, "UID")
                    .IsUnique();

                entity.Property(e => e.BookId)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("bookID")
                    .IsFixedLength(true);

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime");

                entity.Property(e => e.EduSubject)
                    .HasMaxLength(3)
                    .HasColumnName("eduSubject")
                    .IsFixedLength(true);

                entity.Property(e => e.Maintainer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("maintainer");

                entity.Property(e => e.MetaType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("metaType");

                entity.Property(e => e.MetaUid)
                    .HasMaxLength(38)
                    .HasColumnName("metaUID")
                    .IsFixedLength(true)
                    .HasComment("知識向度uid");

                entity.Property(e => e.Orderby)
                    .HasColumnType("int(11)")
                    .HasColumnName("orderby")
                    .HasDefaultValueSql("'99'");

                entity.Property(e => e.ParentUid)
                    .HasMaxLength(38)
                    .HasColumnName("parentUID")
                    .IsFixedLength(true);

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasMaxLength(38)
                    .HasColumnName("UID")
                    .HasDefaultValueSql("uuid()")
                    .IsFixedLength(true);

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("updateTime");
            });

            modelBuilder.Entity<BookMeta110>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BookMeta110");

                entity.HasIndex(e => new { e.BookId, e.MetaUid }, "BookMeta")
                    .IsUnique();

                entity.HasIndex(e => e.Uid, "UID")
                    .IsUnique();

                entity.Property(e => e.BookId)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("bookID")
                    .IsFixedLength(true);

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime");

                entity.Property(e => e.EduSubject)
                    .HasMaxLength(3)
                    .HasColumnName("eduSubject")
                    .IsFixedLength(true);

                entity.Property(e => e.Maintainer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("maintainer");

                entity.Property(e => e.MetaType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("metaType");

                entity.Property(e => e.MetaUid)
                    .HasMaxLength(38)
                    .HasColumnName("metaUID")
                    .IsFixedLength(true)
                    .HasComment("知識向度uid");

                entity.Property(e => e.Orderby)
                    .HasColumnType("int(11)")
                    .HasColumnName("orderby")
                    .HasDefaultValueSql("'99'");

                entity.Property(e => e.ParentUid)
                    .HasMaxLength(38)
                    .HasColumnName("parentUID")
                    .IsFixedLength(true);

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasMaxLength(38)
                    .HasColumnName("UID")
                    .HasDefaultValueSql("uuid()")
                    .IsFixedLength(true);

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("updateTime");
            });

            modelBuilder.Entity<BookMeta111>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BookMeta111");

                entity.HasIndex(e => new { e.BookId, e.MetaUid }, "BookMeta")
                    .IsUnique();

                entity.HasIndex(e => e.Uid, "UID")
                    .IsUnique();

                entity.Property(e => e.BookId)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("bookID")
                    .IsFixedLength(true);

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime");

                entity.Property(e => e.EduSubject)
                    .HasMaxLength(3)
                    .HasColumnName("eduSubject")
                    .IsFixedLength(true);

                entity.Property(e => e.Maintainer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("maintainer");

                entity.Property(e => e.MetaType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("metaType");

                entity.Property(e => e.MetaUid)
                    .HasMaxLength(38)
                    .HasColumnName("metaUID")
                    .IsFixedLength(true);

                entity.Property(e => e.Orderby)
                    .HasColumnType("int(11)")
                    .HasColumnName("orderby")
                    .HasDefaultValueSql("'99'");

                entity.Property(e => e.ParentUid)
                    .HasMaxLength(38)
                    .HasColumnName("parentUID")
                    .IsFixedLength(true);

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasMaxLength(38)
                    .HasColumnName("UID")
                    .HasDefaultValueSql("uuid()")
                    .IsFixedLength(true);

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("updateTime");
            });

            modelBuilder.Entity<Chapter109>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Chapter109");

                entity.HasIndex(e => new { e.BookId, e.Code, e.ParentUid }, "Chapter")
                    .IsUnique();

                entity.HasIndex(e => e.Uid, "UID")
                    .IsUnique();

                entity.Property(e => e.BookId)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("bookID")
                    .IsFixedLength(true);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("code");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime");

                entity.Property(e => e.ExamType2)
                    .HasMaxLength(10)
                    .HasColumnName("examType2");

                entity.Property(e => e.ExamType3)
                    .HasMaxLength(10)
                    .HasColumnName("examType3");

                entity.Property(e => e.Maintainer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("maintainer");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");

                entity.Property(e => e.ParentUid)
                    .HasMaxLength(38)
                    .HasColumnName("parentUID")
                    .IsFixedLength(true);

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .HasColumnName("remark");

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasMaxLength(38)
                    .HasColumnName("UID")
                    .HasDefaultValueSql("uuid()")
                    .IsFixedLength(true);

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("updateTime");
            });

            modelBuilder.Entity<Chapter110>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Chapter110");

                entity.HasIndex(e => new { e.BookId, e.Code, e.ParentUid }, "Chapter")
                    .IsUnique();

                entity.HasIndex(e => e.Uid, "UID")
                    .IsUnique();

                entity.Property(e => e.BookId)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("bookID")
                    .IsFixedLength(true);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("code");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime");

                entity.Property(e => e.ExamType2)
                    .HasMaxLength(10)
                    .HasColumnName("examType2");

                entity.Property(e => e.ExamType3)
                    .HasMaxLength(10)
                    .HasColumnName("examType3");

                entity.Property(e => e.Maintainer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("maintainer");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");

                entity.Property(e => e.ParentUid)
                    .HasMaxLength(38)
                    .HasColumnName("parentUID")
                    .IsFixedLength(true);

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .HasColumnName("remark");

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasMaxLength(38)
                    .HasColumnName("UID")
                    .HasDefaultValueSql("uuid()")
                    .IsFixedLength(true);

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("updateTime");
            });

            modelBuilder.Entity<Chapter111>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Chapter111");

                entity.HasIndex(e => new { e.BookId, e.Code, e.ParentUid }, "Chapter")
                    .IsUnique();

                entity.HasIndex(e => e.Uid, "UID")
                    .IsUnique();

                entity.Property(e => e.BookId)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("bookID")
                    .IsFixedLength(true);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("code");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime");

                entity.Property(e => e.ExamType2)
                    .HasMaxLength(10)
                    .HasColumnName("examType2");

                entity.Property(e => e.ExamType3)
                    .HasMaxLength(10)
                    .HasColumnName("examType3");

                entity.Property(e => e.Maintainer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("maintainer");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");

                entity.Property(e => e.ParentUid)
                    .HasMaxLength(38)
                    .HasColumnName("parentUID")
                    .IsFixedLength(true);

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .HasColumnName("remark");

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasMaxLength(38)
                    .HasColumnName("UID")
                    .HasDefaultValueSql("uuid()")
                    .IsFixedLength(true);

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("updateTime");
            });

            modelBuilder.Entity<EduBook>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => new { e.Edu, e.BookCode }, "EduBook");

                entity.Property(e => e.BookCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("bookCode")
                    .IsFixedLength(true);

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Edu)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("edu")
                    .IsFixedLength(true);

                entity.Property(e => e.Maintainer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("maintainer");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");

                entity.Property(e => e.Subject)
                    .HasMaxLength(2)
                    .HasColumnName("subject")
                    .IsFixedLength(true);

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("updateTime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
