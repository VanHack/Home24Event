namespace SleekSoftMVCFramework.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_newapp_tbl : DbMigration
    {
        public override void Up()
        {
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
                "dbo.EmailAttachments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EmailLogID = c.Long(nullable: false),
                        FilePath = c.String(nullable: false),
                        FileTitle = c.String(maxLength: 50),
                        DateCreated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmailLogs", t => t.EmailLogID, cascadeDelete: true)
                .Index(t => t.EmailLogID);
            
            CreateTable(
                "dbo.EmailLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Sender = c.String(nullable: false, maxLength: 1000),
                        Receiver = c.String(nullable: false, maxLength: 1000),
                        CC = c.String(nullable: false, maxLength: 1000),
                        BCC = c.String(),
                        Subject = c.String(nullable: false),
                        MessageBody = c.String(nullable: false),
                        HasAttachment = c.Boolean(nullable: false),
                        Retires = c.Int(nullable: false),
                        IsSent = c.Boolean(nullable: false),
                        DateSent = c.DateTime(),
                        DateToSend = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmailTemplates",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        Body = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmailTokens",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EmailCode = c.String(),
                        Token = c.String(),
                        PreviewText = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
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
            
            AddColumn("dbo.AspNetUsers", "MembershipCategory", c => c.Long());
            AddColumn("dbo.AspNetUsers", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "InvitationSource", c => c.Long());
            AddColumn("dbo.AspNetUsers", "InterestedInJoining", c => c.Int());
            AddColumn("dbo.AspNetUsers", "MatrialStatus", c => c.Int());
            AddColumn("dbo.AspNetUsers", "MarriedDate", c => c.String());
            AddColumn("dbo.AspNetUsers", "Joined", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "NearestBusstop", c => c.String());
            AddColumn("dbo.AspNetUsers", "Occupation", c => c.String());
            AddColumn("dbo.AspNetUsers", "ServiceAttended", c => c.Long());
            AddColumn("dbo.AspNetUsers", "CanVisit", c => c.Int());
            AddColumn("dbo.AspNetUsers", "ContactMedium", c => c.Int());
            AddColumn("dbo.AspNetUsers", "AssignedFollowUpUserId", c => c.Long());
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmailAttachments", "EmailLogID", "dbo.EmailLogs");
            DropIndex("dbo.EmailAttachments", new[] { "EmailLogID" });
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false));
            DropColumn("dbo.AspNetUsers", "AssignedFollowUpUserId");
            DropColumn("dbo.AspNetUsers", "ContactMedium");
            DropColumn("dbo.AspNetUsers", "CanVisit");
            DropColumn("dbo.AspNetUsers", "ServiceAttended");
            DropColumn("dbo.AspNetUsers", "Occupation");
            DropColumn("dbo.AspNetUsers", "NearestBusstop");
            DropColumn("dbo.AspNetUsers", "Joined");
            DropColumn("dbo.AspNetUsers", "MarriedDate");
            DropColumn("dbo.AspNetUsers", "MatrialStatus");
            DropColumn("dbo.AspNetUsers", "InterestedInJoining");
            DropColumn("dbo.AspNetUsers", "InvitationSource");
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "MembershipCategory");
            DropTable("dbo.MembershipCategories");
            DropTable("dbo.MemberHousefellowships");
            DropTable("dbo.MemberGroups");
            DropTable("dbo.Housefellowships");
            DropTable("dbo.HousefellowshipChangeHistories");
            DropTable("dbo.FollowUpStatus");
            DropTable("dbo.EmailTokens");
            DropTable("dbo.EmailTemplates");
            DropTable("dbo.EmailLogs");
            DropTable("dbo.EmailAttachments");
            DropTable("dbo.ChurchGroups");
            DropTable("dbo.AttendanceCategories");
        }
    }
}
