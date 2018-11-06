namespace qlcdvien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ykien")]
    public partial class Ykien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        [Key]
        public int Hoatdong_Id { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }

        [Column(TypeName = "ntext")]
        public string Tieude { get; set; }

        

        [Column(TypeName = "datetime")]
        public DateTime? ngaydang { get; set; }

        [StringLength(128)]
        public string nguoidang_id { get; set; }

        public bool? daDuyet { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        
        
    }
}
