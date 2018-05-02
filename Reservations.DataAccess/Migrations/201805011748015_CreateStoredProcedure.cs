namespace Reservations.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateStoredProcedure : DbMigration
    {
        public override void Up()
        {
            //Update Ranking
            CreateStoredProcedure("sp_Reservation_Update_RanKing",
                x => new { id = x.Int(), value = x.Double() },
                @"Update dbo.Reservation set RanKing = @value Where Id = @id");

            //Update Favorite
            CreateStoredProcedure("sp_Reservation_Update_Favorite",
                x => new { id = x.Int(), value = x.Boolean() },
                @"Update dbo.Reservation set Favorite = @value Where Id = @id");

        }

        public override void Down()
        {
            DropStoredProcedure("sp_Reservation_Update_RanKing");
            DropStoredProcedure("sp_Reservation_Update_Favorite");
        }
    }
}
