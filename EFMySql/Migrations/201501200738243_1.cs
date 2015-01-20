namespace EFMySql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(unicode: false),
                        MnemonicCode = c.String(maxLength: 8, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 12, storeType: "nvarchar"),
                        BalanceDirection = c.Int(nullable: false),
                        Remark = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        Timestamp = c.DateTime(nullable: false, precision: 0),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubjectTypes", t => t.Type_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.SubjectTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 12, storeType: "nvarchar"),
                        Remark = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        Timestamp = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vouchers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Digest = c.String(unicode: false),
                        DebtorAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remark = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        Timestamp = c.DateTime(nullable: false, precision: 0),
                        Subject_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .Index(t => t.Subject_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vouchers", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "Type_Id", "dbo.SubjectTypes");
            DropIndex("dbo.Vouchers", new[] { "Subject_Id" });
            DropIndex("dbo.Subjects", new[] { "Type_Id" });
            DropTable("dbo.Vouchers");
            DropTable("dbo.SubjectTypes");
            DropTable("dbo.Subjects");
        }
    }
}
