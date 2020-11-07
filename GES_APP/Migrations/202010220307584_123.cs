namespace GES_APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _123 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Enrollments", name: "TraineeId", newName: "StudentID");
            RenameColumn(table: "dbo.TeachingTopics", name: "TrainerId", newName: "LecturerID");
            RenameIndex(table: "dbo.Enrollments", name: "IX_TraineeId", newName: "IX_StudentID");
            RenameIndex(table: "dbo.TeachingTopics", name: "IX_TrainerId", newName: "IX_LecturerID");
            AddColumn("dbo.AspNetUsers", "WorkingPlace", c => c.String());
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Phone", c => c.Int(nullable: false));
            AddColumn("dbo.Lecturers", "Address", c => c.String());
            AddColumn("dbo.Lecturers", "Experience", c => c.String());
            AddColumn("dbo.Lecturers", "Achievements", c => c.String());
            AddColumn("dbo.Lecturers", "Nationality", c => c.String());
            AddColumn("dbo.Students", "Address", c => c.String());
            AddColumn("dbo.Students", "Nationality", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Nationality");
            DropColumn("dbo.Students", "Address");
            DropColumn("dbo.Lecturers", "Nationality");
            DropColumn("dbo.Lecturers", "Achievements");
            DropColumn("dbo.Lecturers", "Experience");
            DropColumn("dbo.Lecturers", "Address");
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.AspNetUsers", "Age");
            DropColumn("dbo.AspNetUsers", "Name");
            DropColumn("dbo.AspNetUsers", "WorkingPlace");
            RenameIndex(table: "dbo.TeachingTopics", name: "IX_LecturerID", newName: "IX_TrainerId");
            RenameIndex(table: "dbo.Enrollments", name: "IX_StudentID", newName: "IX_TraineeId");
            RenameColumn(table: "dbo.TeachingTopics", name: "LecturerID", newName: "TrainerId");
            RenameColumn(table: "dbo.Enrollments", name: "StudentID", newName: "TraineeId");
        }
    }
}
