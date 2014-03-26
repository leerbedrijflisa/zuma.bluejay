namespace Lisa.Zuma.BlueJay.Web.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoteDateCreated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notes", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notes", "DateCreated");
        }
    }
}
