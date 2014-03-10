namespace Lisa.Zuma.BlueJay.Web.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DossierDetails : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Profiles", "Id", "dbo.Dossiers");
            DropIndex("dbo.Profiles", new[] { "Id" });
            CreateTable(
                "dbo.DossierDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(nullable: false),
                        Contents = c.String(nullable: false),
                        DossierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dossiers", t => t.DossierId, cascadeDelete: true)
                .Index(t => t.DossierId);
            
            DropTable("dbo.Profiles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.DossierDetails", "DossierId", "dbo.Dossiers");
            DropIndex("dbo.DossierDetails", new[] { "DossierId" });
            DropTable("dbo.DossierDetails");
            CreateIndex("dbo.Profiles", "Id");
            AddForeignKey("dbo.Profiles", "Id", "dbo.Dossiers", "Id");
        }
    }
}
