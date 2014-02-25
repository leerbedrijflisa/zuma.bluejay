namespace Lisa.Zuma.BlueJay.Web.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dossiers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        DossierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dossiers", t => t.DossierId, cascadeDelete: true)
                .Index(t => t.DossierId);
            
            CreateTable(
                "dbo.NoteMedias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        MediaLocation = c.String(nullable: false),
                        NoteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notes", t => t.NoteId, cascadeDelete: true)
                .Index(t => t.NoteId);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dossiers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserDossiers",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Dossier_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Dossier_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Dossiers", t => t.Dossier_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Dossier_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDossiers", "Dossier_Id", "dbo.Dossiers");
            DropForeignKey("dbo.UserDossiers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Profiles", "Id", "dbo.Dossiers");
            DropForeignKey("dbo.NoteMedias", "NoteId", "dbo.Notes");
            DropForeignKey("dbo.Notes", "DossierId", "dbo.Dossiers");
            DropIndex("dbo.UserDossiers", new[] { "Dossier_Id" });
            DropIndex("dbo.UserDossiers", new[] { "User_Id" });
            DropIndex("dbo.Profiles", new[] { "Id" });
            DropIndex("dbo.NoteMedias", new[] { "NoteId" });
            DropIndex("dbo.Notes", new[] { "DossierId" });
            DropTable("dbo.UserDossiers");
            DropTable("dbo.Users");
            DropTable("dbo.Profiles");
            DropTable("dbo.NoteMedias");
            DropTable("dbo.Notes");
            DropTable("dbo.Dossiers");
        }
    }
}
