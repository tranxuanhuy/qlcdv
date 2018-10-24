namespace qlcdvien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ngaydangHoatdongCongdoandatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HoatdongCongdoan", "ngaydang", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HoatdongCongdoan", "ngaydang", c => c.DateTime(storeType: "date"));
        }
    }
}
