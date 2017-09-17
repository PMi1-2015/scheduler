namespace LNU.Scheduler.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lectures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        WeekDay = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId)
                .Index(t => t.GroupId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TeacherId = c.Int(nullable: false),
                        NumberPerWeek = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lectures", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Lectures", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Lectures", "GroupId", "dbo.Groups");
            DropIndex("dbo.Subjects", new[] { "TeacherId" });
            DropIndex("dbo.Lectures", new[] { "RoomId" });
            DropIndex("dbo.Lectures", new[] { "GroupId" });
            DropIndex("dbo.Lectures", new[] { "SubjectId" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Subjects");
            DropTable("dbo.Rooms");
            DropTable("dbo.Lectures");
            DropTable("dbo.Groups");
        }
    }
}
