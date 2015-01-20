namespace EFMySql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubjectTypes", "Remark", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubjectTypes", "Remark");
        }
    }
}
