namespace qlcdvien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class del_doituongkhenthuong : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Khenthuong", "doituongKhenthuong");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Khenthuong", "doituongKhenthuong", c => c.Boolean());
        }
    }
}
