using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace qlcdvien.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        [StringLength(50)]
        public string name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DOB { get; set; }

        public bool? sex { get; set; }

        [StringLength(1000)]
        public string noisinh { get; set; }

        [StringLength(1000)]
        public string quequan { get; set; }

        [StringLength(1000)]
        public string HKTT { get; set; }

        [StringLength(1000)]
        public string tamtru { get; set; }

        [StringLength(1000)]
        public string chucvuChinhquyen { get; set; }

        [StringLength(1000)]
        public string chucvuDoanthe { get; set; }

        [StringLength(300)]
        public string vanhoa { get; set; }

        [StringLength(300)]
        public string chuyenmon { get; set; }

        [StringLength(300)]
        public string hocvi { get; set; }

        [StringLength(300)]
        public string hocham { get; set; }

        [StringLength(300)]
        public string tinhoc { get; set; }

        [StringLength(300)]
        public string ngoaingu { get; set; }

        public string imageurl { get; set; }

        [StringLength(300)]
        public string tongiao { get; set; }

        [StringLength(300)]
        public string dantoc { get; set; }

        [StringLength(50)]
        public string cmnd { get; set; }

        [StringLength(300)]
        public string noicapcmnd { get; set; }

        [StringLength(300)]
        public string ngaycapcmnd { get; set; }

        public bool? truongcongdoanbophan { get; set; }

        [StringLength(300)]
        public string truonglopdaotao { get; set; }

        [StringLength(300)]
        public string nangkhieu { get; set; }

        [StringLength(300)]
        public string hanche { get; set; }

        public int capcongdoan_id { get; set; }

      

        public virtual CapCongDoan CapCongDoan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoatdongCongdoan> HoatdongCongdoans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Khenthuong> Khenthuongs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log> Logs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuaTrinhChuyenCongDoan> QuaTrinhChuyenCongDoans { get; set; }

    }
 
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        
        public virtual DbSet<CapCongDoan> CapCongDoans { get; set; }
        public virtual DbSet<HoatdongCongdoan> HoatdongCongdoans { get; set; }
        public virtual DbSet<Khenthuong> Khenthuongs { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<QuaTrinhChuyenCongDoan> QuaTrinhChuyenCongDoans { get; set; }

        public ApplicationDbContext()
            : base("Model1", throwIfV1Schema: false)
        {
        }
       
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<ApplicationUser>()
            //    .HasMany(e => e.ApplicationUserClaims)
            //    .WithRequired(e => e.ApplicationUser)
            //    .HasForeignKey(e => e.UserId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasMany(e => e.ApplicationUserLogins)
            //    .WithRequired(e => e.ApplicationUser)
            //    .HasForeignKey(e => e.UserId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.HoatdongCongdoans)
                .WithOptional(e => e.ApplicationUser)
                .HasForeignKey(e => e.nguoidang_id);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Khenthuongs)
                .WithOptional(e => e.ApplicationUser)
                .HasForeignKey(e => e.cdv_id);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Logs)
                .WithOptional(e => e.ApplicationUser)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.QuaTrinhChuyenCongDoans)
                .WithOptional(e => e.ApplicationUser)
                .HasForeignKey(e => e.cdv_ID);

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasMany(e => e.AspNetRoles)
            //    .WithMany(e =>e.ApplicationUsers)
            //    .Map(m => m.ToTable("ApplicationUserRoles").MapLeftKey("UserId").MapRightKey("RoleId"));

            modelBuilder.Entity<CapCongDoan>()
                .HasMany(e =>e.ApplicationUsers)
                .WithRequired(e => e.CapCongDoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CapCongDoan>()
                .HasMany(e => e.Khenthuongs)
                .WithOptional(e => e.CapCongDoan)
                .HasForeignKey(e => e.tochuc_id);
        }

        //public System.Data.Entity.DbSet<qlcdvien.Models.ApplicationUser> ApplicationUsers { get; set; }


    }
}