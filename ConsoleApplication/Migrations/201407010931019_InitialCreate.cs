namespace ConsoleApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Des = c.String(),
                        Parent_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Parent_Id)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 8),
                        Dept_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Dept_Id)
                .Index(t => t.Dept_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Dept_Id", "dbo.Departments");
            DropForeignKey("dbo.Departments", "Parent_Id", "dbo.Departments");
            DropIndex("dbo.Users", new[] { "Dept_Id" });
            DropIndex("dbo.Departments", new[] { "Parent_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Departments");
        }
    }
}
