namespace qlcdvien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AspNetUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AspNetUser()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            HoatdongCongdoans = new HashSet<HoatdongCongdoan>();
            Khenthuongs = new HashSet<Khenthuong>();
            Logs = new HashSet<Log>();
            QuaTrinhChuyenCongDoans = new HashSet<QuaTrinhChuyenCongDoan>();
            AspNetRoles = new HashSet<AspNetRole>();
        }

        public string Id { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }

        public virtual CapCongDoan CapCongDoan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoatdongCongdoan> HoatdongCongdoans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Khenthuong> Khenthuongs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log> Logs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuaTrinhChuyenCongDoan> QuaTrinhChuyenCongDoans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
    }
}
