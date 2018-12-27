namespace qlcdvien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete_Motangan_hoatdongCd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.HoatdongCongdoan", "Motangan");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HoatdongCongdoan", "Motangan", c => c.String(storeType: "ntext"));
        }
    }
}
