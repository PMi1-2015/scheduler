namespace LNU.Scheduler.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Stablemodels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Subjects", new[] { "TeacherId" });
            CreateTable(
                "dbo.SubjectPerWeeks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        Hours = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.TeacherSubjects",
                c => new
                    {
                        Teacher_Id = c.Int(nullable: false),
                        Subject_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Teacher_Id, t.Subject_Id })
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id, cascadeDelete: true)
                .Index(t => t.Teacher_Id)
                .Index(t => t.Subject_Id);
            
            AddColumn("dbo.Lectures", "TeacherId", c => c.Int(nullable: false));
            AddColumn("dbo.Lectures", "Number", c => c.Int(nullable: false));
            AddColumn("dbo.Teachers", "FirstName", c => c.String());
            AddColumn("dbo.Teachers", "LastName", c => c.String());
            CreateIndex("dbo.Lectures", "TeacherId");
            AddForeignKey("dbo.Lectures", "TeacherId", "dbo.Teachers", "Id", cascadeDelete: true);
            DropColumn("dbo.Subjects", "TeacherId");
            DropColumn("dbo.Subjects", "NumberPerWeek");
            DropColumn("dbo.Teachers", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teachers", "Name", c => c.String());
            AddColumn("dbo.Subjects", "NumberPerWeek", c => c.Int(nullable: false));
            AddColumn("dbo.Subjects", "TeacherId", c => c.Int(nullable: false));
            DropForeignKey("dbo.SubjectPerWeeks", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.SubjectPerWeeks", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.TeacherSubjects", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.TeacherSubjects", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.Lectures", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.TeacherSubjects", new[] { "Subject_Id" });
            DropIndex("dbo.TeacherSubjects", new[] { "Teacher_Id" });
            DropIndex("dbo.SubjectPerWeeks", new[] { "GroupId" });
            DropIndex("dbo.SubjectPerWeeks", new[] { "SubjectId" });
            DropIndex("dbo.Lectures", new[] { "TeacherId" });
            DropColumn("dbo.Teachers", "LastName");
            DropColumn("dbo.Teachers", "FirstName");
            DropColumn("dbo.Lectures", "Number");
            DropColumn("dbo.Lectures", "TeacherId");
            DropTable("dbo.TeacherSubjects");
            DropTable("dbo.SubjectPerWeeks");
            CreateIndex("dbo.Subjects", "TeacherId");
            AddForeignKey("dbo.Subjects", "TeacherId", "dbo.Teachers", "Id", cascadeDelete: true);
        }
    }
}
