namespace qlcdvien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CapCongDoan")]
    public partial class CapCongDoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CapCongDoan()
        {
            AspNetUsers = new HashSet<AspNetUser>();
            Khenthuongs = new HashSet<Khenthuong>();
        }

        [Key]
        public int Capcongdoan_id { get; set; }

        [Required]
        [StringLength(500)]
        public string name { get; set; }

        public int? parent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Khenthuong> Khenthuongs { get; set; }
    }
}
