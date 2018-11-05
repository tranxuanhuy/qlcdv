namespace qlcdvien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Khenthuong")]
    public partial class Khenthuong
    {
        [Key]
        public int Khenthuong_id { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Ngày khen thưởng")]
        public DateTime? ngaykhenthuong { get; set; }

        public string hinhthuc { get; set; }

        public string soquyetdinh { get; set; }

        public string capkhenthuong { get; set; }

        [StringLength(128)]
        public string cdv_id { get; set; }

        public int? tochuc_id { get; set; }

        public string scanurl { get; set; }

        public bool? doituongKhenthuong { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual CapCongDoan CapCongDoan { get; set; }
    }
}
