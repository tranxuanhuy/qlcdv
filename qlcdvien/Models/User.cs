using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace qlcdvien.Models
{
    public class User
    {
        [Key]
        [StringLength(50)]
        public string Id { get; set; }

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

        
        public virtual CapCongDoan CapCongDoan { get; set; }
    }
}