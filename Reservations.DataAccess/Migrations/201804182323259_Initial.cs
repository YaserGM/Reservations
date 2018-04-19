namespace Reservations.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        PhoneNumber = c.String(nullable: false),
                        Birthdate = c.DateTime(nullable: false),
                        Contacttype = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Descriptions = c.String(nullable: false),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "ContactId", "dbo.Contacts");
            DropIndex("dbo.Reservations", new[] { "ContactId" });
            DropTable("dbo.Reservations");
            DropTable("dbo.Contacts");
        }
    }
}
