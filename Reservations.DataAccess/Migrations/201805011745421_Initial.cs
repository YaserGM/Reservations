namespace Reservations.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(nullable: false, maxLength: 20),
                        Birthdate = c.DateTime(nullable: false),
                        ContactTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactType", t => t.ContactTypeId, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.ContactTypeId);
            
            CreateTable(
                "dbo.ContactType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Description, unique: true);
            
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RanKing = c.Double(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Descriptions = c.String(nullable: false),
                        Favorite = c.Boolean(nullable: false),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contact", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservation", "ContactId", "dbo.Contact");
            DropForeignKey("dbo.Contact", "ContactTypeId", "dbo.ContactType");
            DropIndex("dbo.Reservation", new[] { "ContactId" });
            DropIndex("dbo.ContactType", new[] { "Description" });
            DropIndex("dbo.Contact", new[] { "ContactTypeId" });
            DropIndex("dbo.Contact", new[] { "Name" });
            DropTable("dbo.Reservation");
            DropTable("dbo.ContactType");
            DropTable("dbo.Contact");
        }
    }
}
