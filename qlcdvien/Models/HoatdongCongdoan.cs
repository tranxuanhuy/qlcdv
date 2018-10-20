namespace qlcdvien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoatdongCongdoan")]
    public partial class HoatdongCongdoan
    {
        [Key]
        public int Hoatdong_Id { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }

        [Column(TypeName = "ntext")]
        public string Tieude { get; set; }

        public byte[] Anhhoatdong { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngaydang { get; set; }

        [StringLength(128)]
        public string nguoidang_id { get; set; }

        public bool? daDuyet { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
