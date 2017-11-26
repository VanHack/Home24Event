namespace SleekSoftMVCFramework.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_spGetEvents : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("dbo.spGetEvents",
         body:
         @"SELECT e.[Id]
      ,e.[EventName]
      ,e.[EventDescription]
      ,e.[Venue]
      ,e.[ArtistId]
	  ,a.FirstName + ' ' + a.LastName ArtistName
      ,e.[StartDate]
      ,e.[EndDate]
      ,e.[City]
      ,e.[Country]
  FROM  [Events] e
inner join [AspNetUsers] a  on e.[ArtistId]=e.AspNetUserId"
        );
        }
      
        public override void Down()
        {
            DropStoredProcedure("dbo.spGetEvents");
        }
    }
}
