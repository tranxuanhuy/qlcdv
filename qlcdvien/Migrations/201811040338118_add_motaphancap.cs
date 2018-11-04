namespace qlcdvien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_motaphancap : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CapCongDoan", "motaphancap", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CapCongDoan", "motaphancap");
        }
    }
}
