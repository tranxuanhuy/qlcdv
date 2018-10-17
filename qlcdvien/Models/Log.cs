namespace qlcdvien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log")]
    public partial class Log
    {
        public Guid LogID { get; set; }

        public string EventType { get; set; }

        [StringLength(100)]
        public string TableName { get; set; }

        [StringLength(100)]
        public string ActionID { get; set; }

        [StringLength(256)]
        public string RecordID { get; set; }

        [StringLength(256)]
        public string ColumnName { get; set; }

        public string OriginalValue { get; set; }

        public string NewValue { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
