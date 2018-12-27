namespace qlcdvien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_Motangan_hoatdongCd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HoatdongCongdoan", "Motangan", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HoatdongCongdoan", "Motangan");
        }
    }
}
