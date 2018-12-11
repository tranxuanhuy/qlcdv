namespace qlcdvien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [TrackChanges]
    [Table("Khenthuong")]
    public partial class Khenthuong
    {
        [Key]
        public int Khenthuong_id { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Ngày khen thưởng")]
        public DateTime? ngaykhenthuong { get; set; }

        [DisplayName("Hình thức")]
        public string hinhthuc { get; set; }

        [DisplayName("Số quyết định")]
        public string soquyetdinh { get; set; }

        [DisplayName("Cấp khen thưởng")]
        public string capkhenthuong { get; set; }

        [StringLength(128)]
        public string cdv_id { get; set; }

        //public int? tochuc_id { get; set; }

        [DisplayName("Bản scan")]
        public string scanurl { get; set; }

       

        public virtual ApplicationUser ApplicationUser { get; set; }

        //public virtual CapCongDoan CapCongDoan { get; set; }
    }
}
