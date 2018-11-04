namespace qlcdvien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_namephancap : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CapCongDoan", "namephancap", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CapCongDoan", "namephancap");
        }
    }
}
