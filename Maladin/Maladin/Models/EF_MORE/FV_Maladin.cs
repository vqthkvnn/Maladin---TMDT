namespace Maladin.Models.EF_MORE
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FV_Maladin : DbContext
    {
        public FV_Maladin()
            : base("name=FV_MaladinDbContext")
        {
        }

        public virtual DbSet<ACC_PRODUCT> ACC_PRODUCT { get; set; }
        public virtual DbSet<ACCOUNT> ACCOUNTs { get; set; }
        public virtual DbSet<FAVORITE_PRODUCT> FAVORITE_PRODUCT { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ACC_PRODUCT>()
                .Property(e => e.ID_ACC_PRODUCT)
                .IsUnicode(false);

            modelBuilder.Entity<ACC_PRODUCT>()
                .Property(e => e.USER_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<ACC_PRODUCT>()
                .Property(e => e.ID_PRODUCT)
                .IsUnicode(false);

            modelBuilder.Entity<ACC_PRODUCT>()
                .HasMany(e => e.FAVORITE_PRODUCT)
                .WithRequired(e => e.ACC_PRODUCT)
                .HasForeignKey(e => e.ID_ACC_PRODUCT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACC_PRODUCT>()
                .HasMany(e => e.FAVORITE_PRODUCT1)
                .WithRequired(e => e.ACC_PRODUCT1)
                .HasForeignKey(e => e.ID_ACC_PRODUCT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACCOUNT>()
                .Property(e => e.USER_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<ACCOUNT>()
                .Property(e => e.PASSWORD_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<ACCOUNT>()
                .Property(e => e.ID_TYPE_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.ACC_PRODUCT)
                .WithRequired(e => e.ACCOUNT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.FAVORITE_PRODUCT)
                .WithRequired(e => e.ACCOUNT)
                .HasForeignKey(e => e.USER_ACC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.FAVORITE_PRODUCT1)
                .WithRequired(e => e.ACCOUNT1)
                .HasForeignKey(e => e.USER_ACC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FAVORITE_PRODUCT>()
                .Property(e => e.ID_ACC_PRODUCT)
                .IsUnicode(false);

            modelBuilder.Entity<FAVORITE_PRODUCT>()
                .Property(e => e.USER_ACC)
                .IsUnicode(false);
        }
    }
}
