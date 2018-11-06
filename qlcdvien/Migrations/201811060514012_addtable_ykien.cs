namespace qlcdvien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtable_ykien : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ykien",
                c => new
                    {
                        Hoatdong_Id = c.Int(nullable: false, identity: true),
                        NoiDung = c.String(storeType: "ntext"),
                        Tieude = c.String(storeType: "ntext"),
                        ngaydang = c.DateTime(),
                        nguoidang_id = c.String(maxLength: 128),
                        daDuyet = c.Boolean(),
                    })
                .PrimaryKey(t => t.Hoatdong_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.nguoidang_id)
                .Index(t => t.nguoidang_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ykien", "nguoidang_id", "dbo.AspNetUsers");
            DropIndex("dbo.Ykien", new[] { "nguoidang_id" });
            DropTable("dbo.Ykien");
        }
    }
}
