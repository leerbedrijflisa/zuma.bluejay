namespace Lisa.Zuma.BlueJay.Web.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PosterIdMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notes", "PosterId", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notes", "PosterId");
        }
    }
}
