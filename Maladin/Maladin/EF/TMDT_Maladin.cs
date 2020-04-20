namespace Maladin.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TMDT_Maladin : DbContext
    {
        public TMDT_Maladin()
            : base("name=TMDT_MaladinDbContext")
        {
        }

        public virtual DbSet<ACC_PRODUCT> ACC_PRODUCT { get; set; }
        public virtual DbSet<ACCOUNT> ACCOUNTs { get; set; }
        public virtual DbSet<ACCOUNT_COMMENT> ACCOUNT_COMMENT { get; set; }
        public virtual DbSet<APPROVED_PRODUCT_WAIT> APPROVED_PRODUCT_WAIT { get; set; }
        public virtual DbSet<APPROVED_USER_WAIT> APPROVED_USER_WAIT { get; set; }
        public virtual DbSet<GROUP_CHAT> GROUP_CHAT { get; set; }
        public virtual DbSet<GUEST_QUESTION> GUEST_QUESTION { get; set; }
        public virtual DbSet<INFOMATION_ACCOUNT> INFOMATION_ACCOUNT { get; set; }
        public virtual DbSet<INFOMATION_GUEST> INFOMATION_GUEST { get; set; }
        public virtual DbSet<MEMBER_GROUP_CHAT> MEMBER_GROUP_CHAT { get; set; }
        public virtual DbSet<MESSAGE_SEND_TO> MESSAGE_SEND_TO { get; set; }
        public virtual DbSet<MESSAGE_SEND_TO_GR> MESSAGE_SEND_TO_GR { get; set; }
        public virtual DbSet<NOTI_ACC> NOTI_ACC { get; set; }
        public virtual DbSet<NOTIFICATION_> NOTIFICATION_ { get; set; }
        public virtual DbSet<ODER> ODERs { get; set; }
        public virtual DbSet<ORIGIN> ORIGINs { get; set; }
        public virtual DbSet<PRODUCER_INFO> PRODUCER_INFO { get; set; }
        public virtual DbSet<PRODUCT> PRODUCTs { get; set; }
        public virtual DbSet<PRODUCT_IMAGE> PRODUCT_IMAGE { get; set; }
        public virtual DbSet<TYPE_ACCOUNT> TYPE_ACCOUNT { get; set; }
        public virtual DbSet<TYPE_NOTIFICATION> TYPE_NOTIFICATION { get; set; }
        public virtual DbSet<TYPE_ODER> TYPE_ODER { get; set; }
        public virtual DbSet<TYPE_ODER_ACC_PRODUCT> TYPE_ODER_ACC_PRODUCT { get; set; }
        public virtual DbSet<TYPE_PRODUCT> TYPE_PRODUCT { get; set; }
        public virtual DbSet<USES_WAIT> USES_WAIT { get; set; }
        public virtual DbSet<VOCHER> VOCHERs { get; set; }
        public virtual DbSet<WAIT_PRODUCT> WAIT_PRODUCT { get; set; }
        public virtual DbSet<WATCHED_PRODUCT> WATCHED_PRODUCT { get; set; }
        public virtual DbSet<VOCHER_AREA> VOCHER_AREA { get; set; }

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
                .HasMany(e => e.ACCOUNT_COMMENT)
                .WithRequired(e => e.ACC_PRODUCT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACC_PRODUCT>()
                .HasMany(e => e.GUEST_QUESTION)
                .WithRequired(e => e.ACC_PRODUCT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACC_PRODUCT>()
                .HasMany(e => e.ODERs)
                .WithRequired(e => e.ACC_PRODUCT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACC_PRODUCT>()
                .HasMany(e => e.TYPE_ODER_ACC_PRODUCT)
                .WithRequired(e => e.ACC_PRODUCT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACC_PRODUCT>()
                .HasMany(e => e.WATCHED_PRODUCT)
                .WithRequired(e => e.ACC_PRODUCT)
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
                .HasMany(e => e.ACCOUNT_COMMENT)
                .WithRequired(e => e.ACCOUNT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.APPROVED_PRODUCT_WAIT)
                .WithRequired(e => e.ACCOUNT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.APPROVED_USER_WAIT)
                .WithRequired(e => e.ACCOUNT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.MEMBER_GROUP_CHAT)
                .WithRequired(e => e.ACCOUNT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.MESSAGE_SEND_TO)
                .WithRequired(e => e.ACCOUNT)
                .HasForeignKey(e => e.FROM_ACC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.MESSAGE_SEND_TO1)
                .WithRequired(e => e.ACCOUNT1)
                .HasForeignKey(e => e.TO_ACC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.MESSAGE_SEND_TO_GR)
                .WithRequired(e => e.ACCOUNT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.NOTI_ACC)
                .WithRequired(e => e.ACCOUNT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.USES_WAIT)
                .WithRequired(e => e.ACCOUNT)
                .HasForeignKey(e => e.USER_ACC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.USES_WAIT1)
                .WithRequired(e => e.ACCOUNT1)
                .HasForeignKey(e => e.USER_ACC_WANT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.WATCHED_PRODUCT)
                .WithRequired(e => e.ACCOUNT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ACCOUNT_COMMENT>()
                .Property(e => e.USER_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<ACCOUNT_COMMENT>()
                .Property(e => e.ID_ACC_PRODUCT)
                .IsUnicode(false);

            modelBuilder.Entity<APPROVED_PRODUCT_WAIT>()
                .Property(e => e.USER_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<APPROVED_USER_WAIT>()
                .Property(e => e.USER_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<GROUP_CHAT>()
                .HasMany(e => e.MEMBER_GROUP_CHAT)
                .WithRequired(e => e.GROUP_CHAT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GROUP_CHAT>()
                .HasMany(e => e.MESSAGE_SEND_TO_GR)
                .WithRequired(e => e.GROUP_CHAT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GUEST_QUESTION>()
                .Property(e => e.ID_ACC_PRODUCT)
                .IsUnicode(false);

            modelBuilder.Entity<INFOMATION_ACCOUNT>()
                .Property(e => e.ID_INFO)
                .IsUnicode(false);

            modelBuilder.Entity<INFOMATION_ACCOUNT>()
                .Property(e => e.CMND_INFO)
                .IsUnicode(false);

            modelBuilder.Entity<INFOMATION_ACCOUNT>()
                .Property(e => e.USER_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<INFOMATION_ACCOUNT>()
                .Property(e => e.AVT_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<INFOMATION_GUEST>()
                .Property(e => e.PHONE_GUEST)
                .IsUnicode(false);

            modelBuilder.Entity<INFOMATION_GUEST>()
                .Property(e => e.AVT_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<INFOMATION_GUEST>()
                .Property(e => e.USER_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<INFOMATION_GUEST>()
                .Property(e => e.ID_TYPE_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<INFOMATION_GUEST>()
                .Property(e => e.EMAIL_GUEST)
                .IsUnicode(false);

            modelBuilder.Entity<INFOMATION_GUEST>()
                .HasMany(e => e.ODERs)
                .WithOptional(e => e.INFOMATION_GUEST)
                .HasForeignKey(e => e.ID_GUEST_NO_ACC);

            modelBuilder.Entity<MEMBER_GROUP_CHAT>()
                .Property(e => e.USER_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<MESSAGE_SEND_TO>()
                .Property(e => e.FROM_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<MESSAGE_SEND_TO>()
                .Property(e => e.TO_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<MESSAGE_SEND_TO_GR>()
                .Property(e => e.USER_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<NOTI_ACC>()
                .Property(e => e.USER_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<NOTI_ACC>()
                .Property(e => e.ID_NOTI)
                .IsUnicode(false);

            modelBuilder.Entity<NOTIFICATION_>()
                .Property(e => e.ID_NOTI)
                .IsUnicode(false);

            modelBuilder.Entity<NOTIFICATION_>()
                .HasMany(e => e.NOTI_ACC)
                .WithRequired(e => e.NOTIFICATION_)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ODER>()
                .Property(e => e.ID_ODER)
                .IsUnicode(false);

            modelBuilder.Entity<ODER>()
                .Property(e => e.USER_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<ODER>()
                .Property(e => e.ID_ACC_PRODUCT)
                .IsUnicode(false);

            modelBuilder.Entity<ODER>()
                .Property(e => e.ID_TYPE_ODER)
                .IsUnicode(false);

            modelBuilder.Entity<ODER>()
                .Property(e => e.ID_VOCHER)
                .IsUnicode(false);

            modelBuilder.Entity<ORIGIN>()
                .Property(e => e.ID_ORIGIN)
                .IsUnicode(false);

            modelBuilder.Entity<ORIGIN>()
                .HasMany(e => e.PRODUCTs)
                .WithRequired(e => e.ORIGIN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ORIGIN>()
                .HasMany(e => e.WAIT_PRODUCT)
                .WithRequired(e => e.ORIGIN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRODUCER_INFO>()
                .Property(e => e.ID_PRODUCER)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCER_INFO>()
                .Property(e => e.PHONE_PRODUCER)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCER_INFO>()
                .HasMany(e => e.PRODUCTs)
                .WithRequired(e => e.PRODUCER_INFO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRODUCER_INFO>()
                .HasMany(e => e.WAIT_PRODUCT)
                .WithRequired(e => e.PRODUCER_INFO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRODUCT>()
                .Property(e => e.ID_PRODUCT)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCT>()
                .Property(e => e.ID_PRODUCER)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCT>()
                .Property(e => e.ID_TYPE_PRODUCT)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCT>()
                .Property(e => e.ID_ORIGIN)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCT>()
                .Property(e => e.ID_INFO)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCT>()
                .Property(e => e.USER_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCT>()
                .HasMany(e => e.ACC_PRODUCT)
                .WithRequired(e => e.PRODUCT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRODUCT>()
                .HasMany(e => e.PRODUCT_IMAGE)
                .WithRequired(e => e.PRODUCT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRODUCT_IMAGE>()
                .Property(e => e.ID_PRODUCT)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCT_IMAGE>()
                .Property(e => e.IMAGE_PATH)
                .IsUnicode(false);

            modelBuilder.Entity<TYPE_ACCOUNT>()
                .Property(e => e.ID_TYPE_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<TYPE_ACCOUNT>()
                .HasMany(e => e.ACCOUNTs)
                .WithRequired(e => e.TYPE_ACCOUNT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TYPE_ACCOUNT>()
                .HasMany(e => e.INFOMATION_GUEST)
                .WithRequired(e => e.TYPE_ACCOUNT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TYPE_NOTIFICATION>()
                .Property(e => e.IMAGE_TYPE_NOTI)
                .IsUnicode(false);

            modelBuilder.Entity<TYPE_ODER>()
                .Property(e => e.ID_TYPE_ODER)
                .IsUnicode(false);

            modelBuilder.Entity<TYPE_ODER>()
                .HasMany(e => e.ODERs)
                .WithRequired(e => e.TYPE_ODER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TYPE_ODER>()
                .HasMany(e => e.TYPE_ODER_ACC_PRODUCT)
                .WithRequired(e => e.TYPE_ODER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TYPE_ODER_ACC_PRODUCT>()
                .Property(e => e.ID_ACC_PRODUCT)
                .IsUnicode(false);

            modelBuilder.Entity<TYPE_ODER_ACC_PRODUCT>()
                .Property(e => e.ID_TYPE_ODER)
                .IsUnicode(false);

            modelBuilder.Entity<TYPE_PRODUCT>()
                .Property(e => e.ID_TYPE_PRODUCT)
                .IsUnicode(false);

            modelBuilder.Entity<TYPE_PRODUCT>()
                .HasMany(e => e.PRODUCTs)
                .WithRequired(e => e.TYPE_PRODUCT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TYPE_PRODUCT>()
                .HasMany(e => e.WAIT_PRODUCT)
                .WithRequired(e => e.TYPE_PRODUCT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USES_WAIT>()
                .Property(e => e.USER_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<USES_WAIT>()
                .Property(e => e.USER_ACC_WANT)
                .IsUnicode(false);

            modelBuilder.Entity<USES_WAIT>()
                .HasMany(e => e.APPROVED_USER_WAIT)
                .WithRequired(e => e.USES_WAIT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VOCHER>()
                .Property(e => e.ID_VOCHER)
                .IsUnicode(false);

            modelBuilder.Entity<VOCHER>()
                .HasOptional(e => e.VOCHER_AREA)
                .WithRequired(e => e.VOCHER);

            modelBuilder.Entity<WAIT_PRODUCT>()
                .Property(e => e.ID_PRODUCER)
                .IsUnicode(false);

            modelBuilder.Entity<WAIT_PRODUCT>()
                .Property(e => e.ID_TYPE_PRODUCT)
                .IsUnicode(false);

            modelBuilder.Entity<WAIT_PRODUCT>()
                .Property(e => e.ID_ORIGIN)
                .IsUnicode(false);

            modelBuilder.Entity<WAIT_PRODUCT>()
                .Property(e => e.ID_INFO)
                .IsUnicode(false);

            modelBuilder.Entity<WAIT_PRODUCT>()
                .HasMany(e => e.APPROVED_PRODUCT_WAIT)
                .WithRequired(e => e.WAIT_PRODUCT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WATCHED_PRODUCT>()
                .Property(e => e.USER_ACC)
                .IsUnicode(false);

            modelBuilder.Entity<WATCHED_PRODUCT>()
                .Property(e => e.ID_ACC_PRODUCT)
                .IsUnicode(false);

            modelBuilder.Entity<VOCHER_AREA>()
                .Property(e => e.ID_VOCHER)
                .IsUnicode(false);

            modelBuilder.Entity<VOCHER_AREA>()
                .Property(e => e.ID_PRODUCT)
                .IsUnicode(false);

            modelBuilder.Entity<VOCHER_AREA>()
                .Property(e => e.ID_ACC_PRODUCT)
                .IsUnicode(false);
        }
    }
}
