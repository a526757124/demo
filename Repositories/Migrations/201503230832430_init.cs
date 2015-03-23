namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(unicode: false),
                        LoginName = c.String(unicode: false),
                        Password = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        RowVersion = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
        }
    }
}
