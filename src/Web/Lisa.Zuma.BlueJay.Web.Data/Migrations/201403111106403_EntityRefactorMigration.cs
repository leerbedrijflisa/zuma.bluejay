namespace Lisa.Zuma.BlueJay.Web.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntityRefactorMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DossierDetails", "DossierId", "dbo.Dossiers");
            DropForeignKey("dbo.Notes", "DossierId", "dbo.Dossiers");
            DropForeignKey("dbo.NoteMedias", "NoteId", "dbo.Notes");
            DropForeignKey("dbo.UserDossiers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserDossiers", "Dossier_Id", "dbo.Dossiers");
            DropIndex("dbo.DossierDetails", new[] { "DossierId" });
            DropIndex("dbo.Notes", new[] { "DossierId" });
            DropIndex("dbo.NoteMedias", new[] { "NoteId" });
            DropIndex("dbo.UserDossiers", new[] { "User_Id" });
            DropIndex("dbo.UserDossiers", new[] { "Dossier_Id" });
            DropTable("dbo.DossierDetails");
            DropTable("dbo.Dossiers");
            DropTable("dbo.Notes");
            DropTable("dbo.NoteMedias");
            DropTable("dbo.Users");
            DropTable("dbo.UserDossiers");
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
                        UserId = c.Int(nullable: false),
                        DossierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.DossierId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Dossiers", t => t.DossierId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.DossierId);           
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDossiers", "DossierId", "dbo.Dossiers");
            DropForeignKey("dbo.UserDossiers", "UserId", "dbo.Users");
            DropForeignKey("dbo.NoteMedias", "NoteId", "dbo.Notes");
            DropForeignKey("dbo.Notes", "DossierId", "dbo.Dossiers");
            DropForeignKey("dbo.DossierDetails", "DossierId", "dbo.Dossiers");
            DropIndex("dbo.UserDossiers", new[] { "DossierId" });
            DropIndex("dbo.UserDossiers", new[] { "UserId" });
            DropIndex("dbo.NoteMedias", new[] { "NoteId" });
            DropIndex("dbo.Notes", new[] { "DossierId" });
            DropIndex("dbo.DossierDetails", new[] { "DossierId" });
            DropTable("dbo.UserDossiers");
            DropTable("dbo.Users");
            DropTable("dbo.NoteMedias");
            DropTable("dbo.Notes");
            DropTable("dbo.Dossiers");
            DropTable("dbo.DossierDetails");

            CreateTable(
                "dbo.UserDossiers",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Dossier_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Dossier_Id });
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NoteMedias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        MediaLocation = c.String(nullable: false),
                        NoteId = c.Int(nullable: false),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Dossiers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DossierDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(nullable: false),
                        Contents = c.String(nullable: false),
                        DossierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateIndex("dbo.UserDossiers", "Dossier_Id");
            CreateIndex("dbo.UserDossiers", "User_Id");
            CreateIndex("dbo.NoteMedias", "NoteId");
            CreateIndex("dbo.Notes", "DossierId");
            CreateIndex("dbo.DossierDetails", "DossierId");
            AddForeignKey("dbo.UserDossiers", "Dossier_Id", "dbo.Dossiers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserDossiers", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.NoteMedias", "NoteId", "dbo.Notes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Notes", "DossierId", "dbo.Dossiers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DossierDetails", "DossierId", "dbo.Dossiers", "Id", cascadeDelete: true);
        }
    }
}
