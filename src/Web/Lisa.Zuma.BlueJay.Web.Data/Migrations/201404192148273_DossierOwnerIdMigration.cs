namespace Lisa.Zuma.BlueJay.Web.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DossierOwnerIdMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dossiers", "OwnerId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dossiers", "OwnerId");
        }
    }
}
