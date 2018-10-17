namespace qlcdvien.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<CapCongDoan> CapCongDoans { get; set; }
        public virtual DbSet<HoatdongCongdoan> HoatdongCongdoans { get; set; }
        public virtual DbSet<Khenthuong> Khenthuongs { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<QuaTrinhChuyenCongDoan> QuaTrinhChuyenCongDoans { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.HoatdongCongdoans)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.nguoidang_id);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Khenthuongs)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.cdv_id);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Logs)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.QuaTrinhChuyenCongDoans)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.cdv_ID);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetRoles)
                .WithMany(e => e.AspNetUsers)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("UserId").MapRightKey("RoleId"));

            modelBuilder.Entity<CapCongDoan>()
                .HasMany(e => e.AspNetUsers)
                .WithRequired(e => e.CapCongDoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CapCongDoan>()
                .HasMany(e => e.Khenthuongs)
                .WithOptional(e => e.CapCongDoan)
                .HasForeignKey(e => e.tochuc_id);
        }
    }
}
