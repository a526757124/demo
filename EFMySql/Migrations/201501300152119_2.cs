namespace EFMySql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subjects", "Name", c => c.String(nullable: false, maxLength: 20, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subjects", "Name", c => c.String(nullable: false, maxLength: 12, storeType: "nvarchar"));
        }
    }
}
