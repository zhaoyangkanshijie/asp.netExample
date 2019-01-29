namespace WebApplication1.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class context : DbContext
    {
        public context()
            : base("name=context")
        {
        }

        public virtual DbSet<r_admin> r_admin { get; set; }
        public virtual DbSet<r_article> r_article { get; set; }
        public virtual DbSet<r_user> r_user { get; set; }
        public virtual DbSet<ueditor> ueditor { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<r_admin>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<r_admin>()
                .Property(e => e.nickname)
                .IsUnicode(false);

            modelBuilder.Entity<r_admin>()
                .Property(e => e.latestLoginIP)
                .IsUnicode(false);

            modelBuilder.Entity<r_admin>()
                .Property(e => e.authority)
                .IsUnicode(false);

            modelBuilder.Entity<r_article>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<r_article>()
                .Property(e => e.subTitle)
                .IsUnicode(false);

            modelBuilder.Entity<r_article>()
                .Property(e => e.author)
                .IsUnicode(false);

            modelBuilder.Entity<r_article>()
                .Property(e => e.editor)
                .IsUnicode(false);

            modelBuilder.Entity<r_article>()
                .Property(e => e.authorDesc)
                .IsUnicode(false);

            modelBuilder.Entity<r_article>()
                .Property(e => e.license)
                .IsUnicode(false);

            modelBuilder.Entity<r_article>()
                .Property(e => e._abstract)
                .IsUnicode(false);

            modelBuilder.Entity<r_article>()
                .Property(e => e.keywords)
                .IsUnicode(false);

            modelBuilder.Entity<r_article>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<r_article>()
                .Property(e => e.reference)
                .IsUnicode(false);

            modelBuilder.Entity<r_article>()
                .Property(e => e.thumb)
                .IsUnicode(false);

            modelBuilder.Entity<r_user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<r_user>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<r_user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<r_user>()
                .Property(e => e.nickname)
                .IsUnicode(false);

            modelBuilder.Entity<r_user>()
                .Property(e => e.region)
                .IsUnicode(false);

            modelBuilder.Entity<r_user>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<r_user>()
                .Property(e => e.latestLoginIP)
                .IsUnicode(false);

            modelBuilder.Entity<ueditor>()
                .Property(e => e.content)
                .IsUnicode(false);
        }
    }
}
