namespace Lisa.Zuma.BlueJay.Web.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DossierNameMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dossiers", "Name", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dossiers", "Name");
        }
    }
}
