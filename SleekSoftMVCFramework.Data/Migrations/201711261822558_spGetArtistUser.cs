namespace SleekSoftMVCFramework.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class spGetArtistUser : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("dbo.spGetArtistUser",
            body:
            @"SELECT a.[AspNetUserId]  Id
                  , a.[FirstName]
                  , a.[MiddleName]
                  , a.[LastName]
                  , CONVERT(VARCHAR(12),a.[DOB], 113) DOB 
                  , a.[MobileNumber]
                  , a.[Address]
                  , a.[Email]
                  , a.[PhoneNumber]


                  , a.[UserName]
                  , a.[Gender]
                  , a.[PicturePath]
                  , a.[FacebookURL]
              FROM [AspNetUsers] a
              inner join[AspNetUserRole] ur on a.[AspNetUserId]= ur.AspNetUserId
              inner join [dbo].[AspNetRole] r on ur.AspNetRoleId= r.AspNetRoleId
              where r.AspNetRoleId= 2"
           );
        }
  
        public override void Down()
        {
            DropStoredProcedure("dbo.spGetArtistUser");
        }
    }
}
