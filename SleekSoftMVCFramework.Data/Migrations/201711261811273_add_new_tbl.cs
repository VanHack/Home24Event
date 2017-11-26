namespace SleekSoftMVCFramework.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_new_tbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PicturePath", c => c.String());
            AddColumn("dbo.AspNetUsers", "FacebookURL", c => c.String());
            AlterColumn("dbo.PasswordHistory", "CreatedBy", c => c.Long());
            AlterColumn("dbo.PasswordHistory", "UpdatedBy", c => c.Long());
            DropColumn("dbo.AspNetUsers", "MembershipCategory");
            DropColumn("dbo.AspNetUsers", "InvitationSource");
            DropColumn("dbo.AspNetUsers", "InterestedInJoining");
            DropColumn("dbo.AspNetUsers", "MatrialStatus");
            DropColumn("dbo.AspNetUsers", "MarriedDate");
            DropColumn("dbo.AspNetUsers", "Joined");
            DropColumn("dbo.AspNetUsers", "NearestBusstop");
            DropColumn("dbo.AspNetUsers", "Occupation");
            DropColumn("dbo.AspNetUsers", "ServiceAttended");
            DropColumn("dbo.AspNetUsers", "CanVisit");
            DropColumn("dbo.AspNetUsers", "ContactMedium");
            DropColumn("dbo.AspNetUsers", "AssignedFollowUpUserId");
            DropTable("dbo.AttendanceCategories");
            DropTable("dbo.ChurchGroups");
            DropTable("dbo.FollowUpStatus");
            DropTable("dbo.HousefellowshipChangeHistories");
            DropTable("dbo.Housefellowships");
            DropTable("dbo.MemberGroups");
            DropTable("dbo.MemberHousefellowships");
            DropTable("dbo.MembershipCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MembershipCategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        UpdatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MemberHousefellowships",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        HousefellowshipId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        UpdatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MemberGroups",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        GroupId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        UpdatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Housefellowships",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        StartTime = c.String(),
                        CordinatorUserId = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        UpdatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HousefellowshipChangeHistories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        HousefellowshipId = c.Long(nullable: false),
                        NewCordinatorUserId = c.Long(nullable: false),
                        OldCordinatorUserId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        UpdatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FollowUpStatus",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        UpdatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChurchGroups",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        UpdatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AttendanceCategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        UpdatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "AssignedFollowUpUserId", c => c.Long());
            AddColumn("dbo.AspNetUsers", "ContactMedium", c => c.Int());
            AddColumn("dbo.AspNetUsers", "CanVisit", c => c.Int());
            AddColumn("dbo.AspNetUsers", "ServiceAttended", c => c.Long());
            AddColumn("dbo.AspNetUsers", "Occupation", c => c.String());
            AddColumn("dbo.AspNetUsers", "NearestBusstop", c => c.String());
            AddColumn("dbo.AspNetUsers", "Joined", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "MarriedDate", c => c.String());
            AddColumn("dbo.AspNetUsers", "MatrialStatus", c => c.Int());
            AddColumn("dbo.AspNetUsers", "InterestedInJoining", c => c.Int());
            AddColumn("dbo.AspNetUsers", "InvitationSource", c => c.Long());
            AddColumn("dbo.AspNetUsers", "MembershipCategory", c => c.Long());
            AlterColumn("dbo.PasswordHistory", "UpdatedBy", c => c.Long(nullable: false));
            AlterColumn("dbo.PasswordHistory", "CreatedBy", c => c.Long(nullable: false));
            DropColumn("dbo.AspNetUsers", "FacebookURL");
            DropColumn("dbo.AspNetUsers", "PicturePath");
        }
    }
}
