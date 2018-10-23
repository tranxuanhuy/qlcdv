namespace qlcdvien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "capcongdoan_id", "dbo.CapCongDoan");
            DropColumn("dbo.HoatdongCongdoan", "nguoidang_id");
            DropColumn("dbo.Khenthuong", "tochuc_id");
            DropColumn("dbo.Khenthuong", "cdv_id");
            DropColumn("dbo.Log", "CreatedBy");
            DropColumn("dbo.QuaTrinhChuyenCongDoan", "cdv_ID");
            RenameColumn(table: "dbo.Khenthuong", name: "CapCongDoan_Capcongdoan_id", newName: "tochuc_id");
            RenameColumn(table: "dbo.HoatdongCongdoan", name: "ApplicationUser_Id", newName: "nguoidang_id");
            RenameColumn(table: "dbo.Khenthuong", name: "ApplicationUser_Id", newName: "cdv_id");
            RenameColumn(table: "dbo.Log", name: "ApplicationUser_Id", newName: "CreatedBy");
            RenameColumn(table: "dbo.QuaTrinhChuyenCongDoan", name: "ApplicationUser_Id", newName: "cdv_ID");
            RenameIndex(table: "dbo.HoatdongCongdoan", name: "IX_ApplicationUser_Id", newName: "IX_nguoidang_id");
            RenameIndex(table: "dbo.Khenthuong", name: "IX_ApplicationUser_Id", newName: "IX_cdv_id");
            RenameIndex(table: "dbo.Khenthuong", name: "IX_CapCongDoan_Capcongdoan_id", newName: "IX_tochuc_id");
            RenameIndex(table: "dbo.Log", name: "IX_ApplicationUser_Id", newName: "IX_CreatedBy");
            RenameIndex(table: "dbo.QuaTrinhChuyenCongDoan", name: "IX_ApplicationUser_Id", newName: "IX_cdv_ID");
            AddForeignKey("dbo.AspNetUsers", "capcongdoan_id", "dbo.CapCongDoan", "Capcongdoan_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "capcongdoan_id", "dbo.CapCongDoan");
            RenameIndex(table: "dbo.QuaTrinhChuyenCongDoan", name: "IX_cdv_ID", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Log", name: "IX_CreatedBy", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Khenthuong", name: "IX_tochuc_id", newName: "IX_CapCongDoan_Capcongdoan_id");
            RenameIndex(table: "dbo.Khenthuong", name: "IX_cdv_id", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.HoatdongCongdoan", name: "IX_nguoidang_id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.QuaTrinhChuyenCongDoan", name: "cdv_ID", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Log", name: "CreatedBy", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Khenthuong", name: "cdv_id", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.HoatdongCongdoan", name: "nguoidang_id", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Khenthuong", name: "tochuc_id", newName: "CapCongDoan_Capcongdoan_id");
            AddColumn("dbo.QuaTrinhChuyenCongDoan", "cdv_ID", c => c.String(maxLength: 128));
            AddColumn("dbo.Log", "CreatedBy", c => c.String(maxLength: 128));
            AddColumn("dbo.Khenthuong", "cdv_id", c => c.String(maxLength: 128));
            AddColumn("dbo.Khenthuong", "tochuc_id", c => c.Int());
            AddColumn("dbo.HoatdongCongdoan", "nguoidang_id", c => c.String(maxLength: 128));
            AddForeignKey("dbo.AspNetUsers", "capcongdoan_id", "dbo.CapCongDoan", "Capcongdoan_id", cascadeDelete: true);
        }
    }
}
