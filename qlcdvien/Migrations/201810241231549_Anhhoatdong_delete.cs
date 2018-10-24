namespace qlcdvien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Anhhoatdong_delete : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.HoatdongCongdoan", "Anhhoatdong");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HoatdongCongdoan", "Anhhoatdong", c => c.Binary());
        }
    }
}
