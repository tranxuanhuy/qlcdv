namespace qlcdvien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuaTrinhChuyenCongDoan")]
    public partial class QuaTrinhChuyenCongDoan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Quatrinhchuyen_Id { get; set; }

        [StringLength(128)]
        public string cdv_ID { get; set; }

        public int? ChuyenTu { get; set; }

        public int? ChuyenDen { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ThoiGian { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
