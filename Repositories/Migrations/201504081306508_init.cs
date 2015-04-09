namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(maxLength: 100, storeType: "nvarchar"),
                        CompanyContact = c.String(maxLength: 20, storeType: "nvarchar"),
                        CompanyPhone = c.String(maxLength: 20, storeType: "nvarchar"),
                        CompanyAddress = c.String(maxLength: 200, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 200, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        RowVersion = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubjectCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        BalanceDirection = c.Int(nullable: false),
                        Remark = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        RowVersion = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subject",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 20, storeType: "nvarchar"),
                        MnemonicCode = c.String(maxLength: 20, storeType: "nvarchar"),
                        Name = c.String(maxLength: 20, storeType: "nvarchar"),
                        BalanceDirection = c.Int(nullable: false),
                        Remark = c.String(maxLength: 200, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        RowVersion = c.DateTime(nullable: false, precision: 0),
                        Category_Id = c.Int(nullable: false),
                        ParentSubject_Id = c.Int(),
                        Type_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubjectCategory", t => t.Category_Id)
                .ForeignKey("dbo.Subject", t => t.ParentSubject_Id)
                .ForeignKey("dbo.SubjectType", t => t.Type_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.ParentSubject_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.SubjectType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        RowVersion = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 20, storeType: "nvarchar"),
                        LoginName = c.String(maxLength: 20, storeType: "nvarchar"),
                        Password = c.String(maxLength: 32, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 200, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        RowVersion = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Voucher",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillsTotal = c.Int(nullable: false),
                        VoucherCode = c.String(maxLength: 20, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 200, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        RowVersion = c.DateTime(nullable: false, precision: 0),
                        BelongCompany_Id = c.Int(nullable: false),
                        CreateUser_Id = c.Int(nullable: false),
                        ModifUser_Id = c.Int(nullable: false),
                        Word_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.BelongCompany_Id)
                .ForeignKey("dbo.User", t => t.CreateUser_Id)
                .ForeignKey("dbo.User", t => t.ModifUser_Id)
                .ForeignKey("dbo.VoucherWord", t => t.Word_Id)
                .Index(t => t.BelongCompany_Id)
                .Index(t => t.CreateUser_Id)
                .Index(t => t.ModifUser_Id)
                .Index(t => t.Word_Id);
            
            CreateTable(
                "dbo.VoucherDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Digest = c.String(maxLength: 200, storeType: "nvarchar"),
                        DebtorAmount = c.Decimal(nullable: false, precision: 18, scale: 4),
                        CreditAmount = c.Decimal(nullable: false, precision: 18, scale: 4),
                        Remark = c.String(maxLength: 200, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        RowVersion = c.DateTime(nullable: false, precision: 0),
                        Subject_Id = c.Int(nullable: false),
                        Voucher_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subject", t => t.Subject_Id)
                .ForeignKey("dbo.Voucher", t => t.Voucher_Id)
                .Index(t => t.Subject_Id)
                .Index(t => t.Voucher_Id);
            
            CreateTable(
                "dbo.VoucherWord",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 200, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        RowVersion = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Voucher", "Word_Id", "dbo.VoucherWord");
            DropForeignKey("dbo.VoucherDetail", "Voucher_Id", "dbo.Voucher");
            DropForeignKey("dbo.VoucherDetail", "Subject_Id", "dbo.Subject");
            DropForeignKey("dbo.Voucher", "ModifUser_Id", "dbo.User");
            DropForeignKey("dbo.Voucher", "CreateUser_Id", "dbo.User");
            DropForeignKey("dbo.Voucher", "BelongCompany_Id", "dbo.Company");
            DropForeignKey("dbo.Subject", "Type_Id", "dbo.SubjectType");
            DropForeignKey("dbo.Subject", "ParentSubject_Id", "dbo.Subject");
            DropForeignKey("dbo.Subject", "Category_Id", "dbo.SubjectCategory");
            DropIndex("dbo.VoucherDetail", new[] { "Voucher_Id" });
            DropIndex("dbo.VoucherDetail", new[] { "Subject_Id" });
            DropIndex("dbo.Voucher", new[] { "Word_Id" });
            DropIndex("dbo.Voucher", new[] { "ModifUser_Id" });
            DropIndex("dbo.Voucher", new[] { "CreateUser_Id" });
            DropIndex("dbo.Voucher", new[] { "BelongCompany_Id" });
            DropIndex("dbo.Subject", new[] { "Type_Id" });
            DropIndex("dbo.Subject", new[] { "ParentSubject_Id" });
            DropIndex("dbo.Subject", new[] { "Category_Id" });
            DropTable("dbo.VoucherWord");
            DropTable("dbo.VoucherDetail");
            DropTable("dbo.Voucher");
            DropTable("dbo.User");
            DropTable("dbo.SubjectType");
            DropTable("dbo.Subject");
            DropTable("dbo.SubjectCategory");
            DropTable("dbo.Company");
        }
    }
}
