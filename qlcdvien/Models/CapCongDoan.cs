﻿namespace qlcdvien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CapCongDoan")]
    public partial class CapCongDoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CapCongDoan()
        {
            ApplicationUsers = new HashSet<ApplicationUser>();
            Khenthuongtapthes = new HashSet<Khenthuongtapthe>();
        }

        [Key]
        public int Capcongdoan_id { get; set; }

        [Required]
        [StringLength(500)]
        [DisplayName("Công đoàn")]
        public string name { get; set; }

        public int? parent { get; set; }
        public string motaphancap { get; set; }
        public string namephancap { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Khenthuongtapthe> Khenthuongtapthes { get; set; }
    }
}
