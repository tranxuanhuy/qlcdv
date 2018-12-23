namespace qlcdvien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("Ykien")]
    public partial class Ykien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        [Key]
        public int Hoatdong_Id { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Nội dung")]
        public string NoiDung { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Tiêu đề")]
        public string Tieude { get; set; }

        

        [Column(TypeName = "datetime")]
        [DisplayName("Ngày đăng")]
        public DateTime? ngaydang { get; set; }

        [StringLength(128)]
        public string nguoidang_id { get; set; }

        [DisplayName("Đã duyệt")]
        public bool? daDuyet { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        
        
    }
}
