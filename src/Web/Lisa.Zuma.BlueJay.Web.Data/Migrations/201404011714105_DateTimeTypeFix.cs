namespace Lisa.Zuma.BlueJay.Web.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeTypeFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notes", "DateCreated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notes", "DateCreated", c => c.DateTime(nullable: false));
        }
    }
}
