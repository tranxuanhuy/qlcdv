namespace qlcdvien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtable_khenthuongtapthe : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Khenthuong", "CapCongDoan_Capcongdoan_id", "dbo.CapCongDoan");
            DropIndex("dbo.Khenthuong", new[] { "CapCongDoan_Capcongdoan_id" });
            CreateTable(
                "dbo.Khenthuongtapthe",
                c => new
                    {
                        Khenthuong_id = c.Int(nullable: false, identity: true),
                        ngaykhenthuong = c.DateTime(storeType: "date"),
                        hinhthuc = c.String(),
                        soquyetdinh = c.String(),
                        capkhenthuong = c.String(),
                        tochuc_id = c.Int(),
                        scanurl = c.String(),
                    })
                .PrimaryKey(t => t.Khenthuong_id)
                .ForeignKey("dbo.CapCongDoan", t => t.tochuc_id)
                .Index(t => t.tochuc_id);
            
            DropColumn("dbo.Khenthuong", "CapCongDoan_Capcongdoan_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Khenthuong", "CapCongDoan_Capcongdoan_id", c => c.Int());
            DropForeignKey("dbo.Khenthuongtapthe", "tochuc_id", "dbo.CapCongDoan");
            DropIndex("dbo.Khenthuongtapthe", new[] { "tochuc_id" });
            DropTable("dbo.Khenthuongtapthe");
            CreateIndex("dbo.Khenthuong", "CapCongDoan_Capcongdoan_id");
            AddForeignKey("dbo.Khenthuong", "CapCongDoan_Capcongdoan_id", "dbo.CapCongDoan", "Capcongdoan_id");
        }
    }
}
