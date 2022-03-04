using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TestDBNotification.MySqlPart.Models.Resources
{
    public partial class resourceContext : DbContext
    {
        public resourceContext()
        {
        }

        public resourceContext(DbContextOptions<resourceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Definition> Definitions { get; set; }
        public virtual DbSet<EduRelated> EduRelateds { get; set; }
        public virtual DbSet<QuestionAttr> QuestionAttrs { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=35.236.136.171;port=3306;user id=nani-back-end;password=exbnXQy?D#h5DzHK;database=resource", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.18-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Definition>(entity =>
            {
                entity.HasKey(e => new { e.Type, e.Code })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("Definition");

                entity.HasIndex(e => new { e.Type, e.Code }, "typeCode")
                    .IsUnique();

                entity.Property(e => e.Type)
                    .HasMaxLength(15)
                    .HasColumnName("type")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .HasColumnName("code")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime");

                entity.Property(e => e.Maintainer)
                    .HasMaxLength(20)
                    .HasColumnName("maintainer");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.Orderby)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("orderby");

                entity.Property(e => e.ParentType)
                    .HasMaxLength(20)
                    .HasColumnName("parentType");

                entity.Property(e => e.Remark)
                    .HasMaxLength(300)
                    .HasColumnName("remark");

                entity.Property(e => e.SystemRemark)
                    .HasMaxLength(100)
                    .HasColumnName("systemRemark");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("updateTime");
            });

            modelBuilder.Entity<EduRelated>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("EduRelated");

                entity.HasIndex(e => new { e.Code, e.EduCode }, "TypeCode")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .HasMaxLength(3)
                    .HasColumnName("code");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime");

                entity.Property(e => e.EduCode)
                    .HasMaxLength(10)
                    .HasColumnName("eduCode");

                entity.Property(e => e.Maintainer)
                    .HasMaxLength(20)
                    .HasColumnName("maintainer");

                entity.Property(e => e.Orderby)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("orderby");

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .HasColumnName("type");

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("UID")
                    .HasDefaultValueSql("uuid()");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("updateTime");
            });

            modelBuilder.Entity<QuestionAttr>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("QuestionAttr");

                entity.HasIndex(e => new { e.Code, e.Type }, "TypeCode")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("code");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime");

                entity.Property(e => e.Maintainer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("maintainer");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("type");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("updateTime");
            });

            modelBuilder.Entity<QuestionType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("QuestionType");

                entity.HasIndex(e => e.Code, "code")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("code")
                    .IsFixedLength(true);

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime");

                entity.Property(e => e.GroupCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasColumnName("groupCode")
                    .IsFixedLength(true);

                entity.Property(e => e.IsListen).HasColumnName("isListen");

                entity.Property(e => e.Maintainer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("maintainer");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.PrintCode)
                    .HasMaxLength(2)
                    .HasColumnName("printCode")
                    .IsFixedLength(true);

                entity.Property(e => e.Remark)
                    .HasMaxLength(300)
                    .HasColumnName("remark");

                entity.Property(e => e.Title)
                    .HasMaxLength(20)
                    .HasColumnName("title");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("updateTime");
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Source");

                entity.HasIndex(e => e.Uid, "UID")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("code")
                    .IsFixedLength(true);

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime");

                entity.Property(e => e.Maintainer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("maintainer");

                entity.Property(e => e.MetaType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("metaType");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");

                entity.Property(e => e.ParentUid)
                    .HasMaxLength(38)
                    .HasColumnName("parentUID")
                    .IsFixedLength(true);

                entity.Property(e => e.Remark)
                    .HasMaxLength(300)
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

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => new { e.Code, e.EduCode })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("Subject");

                entity.HasIndex(e => new { e.EduCode, e.Code }, "EduCode")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .HasMaxLength(2)
                    .HasColumnName("code")
                    .HasDefaultValueSql("''")
                    .IsFixedLength(true);

                entity.Property(e => e.EduCode)
                    .HasMaxLength(1)
                    .HasColumnName("eduCode")
                    .HasDefaultValueSql("''")
                    .IsFixedLength(true);

                entity.Property(e => e.Attribute)
                    .HasMaxLength(20)
                    .HasColumnName("attribute");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime");

                entity.Property(e => e.Grade)
                    .HasMaxLength(1)
                    .HasColumnName("grade")
                    .IsFixedLength(true);

                entity.Property(e => e.Maintainer)
                    .HasMaxLength(20)
                    .HasColumnName("maintainer");

                entity.Property(e => e.Orderby)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("orderby");

                entity.Property(e => e.ParentCode)
                    .HasMaxLength(2)
                    .HasColumnName("parentCode");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("updateTime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
