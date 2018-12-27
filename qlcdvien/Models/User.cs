using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace qlcdvien.Models
{
    [TrackChanges]
    public class User
    {
        [Key]
        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(50)]
        [DisplayName("Họ tên")]
        public string name { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Ngày sinh")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DOB { get; set; }

        [DisplayName("Giới tính")]
        public bool? sex { get; set; }

        [StringLength(1000)]
        [DisplayName("Nơi sinh")]
        public string noisinh { get; set; }

        [StringLength(1000)]
        [DisplayName("Quê quán")]
        public string quequan { get; set; }

        [StringLength(1000)]
        [DisplayName("Hộ khẩu thường trú")]
        public string HKTT { get; set; }

        [StringLength(1000)]
        [DisplayName("Tạm trú")]
        public string tamtru { get; set; }

        [StringLength(1000)]
        [DisplayName("Chức vụ chính quyền")]
        public string chucvuChinhquyen { get; set; }

        [StringLength(1000)]
        [DisplayName("Chức vụ đoàn thể")]
        public string chucvuDoanthe { get; set; }

        [StringLength(300)]
        [DisplayName("Văn hóa")]
        public string vanhoa { get; set; }

        [StringLength(300)]
        [DisplayName("Chuyên môn")]
        public string chuyenmon { get; set; }

        [StringLength(300)]
        [DisplayName("Học vị")]
        public string hocvi { get; set; }

        [StringLength(300)]
        [DisplayName("Học hàm")]
        public string hocham { get; set; }

        [StringLength(300)]
        [DisplayName("Trình độ tin học")]
        public string tinhoc { get; set; }

        [StringLength(300)]
        [DisplayName("Trình độ ngoại ngữ")]
        public string ngoaingu { get; set; }

        [DisplayName("Ảnh đại diện")]
        public string imageurl { get; set; }

        [StringLength(300)]
        [DisplayName("Tôn giáo")]
        public string tongiao { get; set; }

        [StringLength(300)]
        [DisplayName("Dân tộc")]
        public string dantoc { get; set; }

        [StringLength(50)]
        [DisplayName("Số CMND")]
        public string cmnd { get; set; }

        [StringLength(300)]
        [DisplayName("Nơi cấp CMND")]
        public string noicapcmnd { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Ngày cấp CMND")]
        public DateTime? ngaycapcmnd { get; set; }


        [DisplayName("Trưởng công đoàn bộ phận")]
        public bool? truongcongdoanbophan { get; set; }

        [StringLength(300)]
        [DisplayName("Trường lớp đào tạo")]
        public string truonglopdaotao { get; set; }

        [StringLength(300)]
        [DisplayName("Năng khiếu")]
        public string nangkhieu { get; set; }

        [StringLength(300)]
        [DisplayName("Hạn chế")]
        public string hanche { get; set; }

        
        public virtual CapCongDoan CapCongDoan { get; set; }
    }
}