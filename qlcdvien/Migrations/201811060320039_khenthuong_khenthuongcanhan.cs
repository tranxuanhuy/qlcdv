namespace qlcdvien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class khenthuong_khenthuongcanhan : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Khenthuong", name: "tochuc_id", newName: "CapCongDoan_Capcongdoan_id");
            RenameIndex(table: "dbo.Khenthuong", name: "IX_tochuc_id", newName: "IX_CapCongDoan_Capcongdoan_id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Khenthuong", name: "IX_CapCongDoan_Capcongdoan_id", newName: "IX_tochuc_id");
            RenameColumn(table: "dbo.Khenthuong", name: "CapCongDoan_Capcongdoan_id", newName: "tochuc_id");
        }
    }
}
