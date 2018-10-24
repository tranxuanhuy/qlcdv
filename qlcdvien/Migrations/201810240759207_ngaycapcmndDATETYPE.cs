namespace qlcdvien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ngaycapcmndDATETYPE : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "ngaycapcmnd", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "ngaycapcmnd", c => c.String(maxLength: 300));
        }
    }
}
