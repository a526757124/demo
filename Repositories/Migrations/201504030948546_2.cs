namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(unicode: false),
                        CompanyContact = c.String(unicode: false),
                        CompanyPhone = c.String(unicode: false),
                        CompanyAddress = c.String(unicode: false),
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
                        Code = c.String(unicode: false),
                        MnemonicCode = c.String(unicode: false),
                        Name = c.String(unicode: false),
                        BalanceDirection = c.Int(nullable: false),
                        Remark = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        RowVersion = c.DateTime(nullable: false, precision: 0),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubjectType", t => t.Type_Id)
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
                "dbo.Voucher",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DebtorTotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditTotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BillsTotal = c.Int(nullable: false),
                        VoucherCode = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        RowVersion = c.DateTime(nullable: false, precision: 0),
                        BelongCompany_Id = c.Int(),
                        CreateUser_Id = c.Int(),
                        ModifUser_Id = c.Int(),
                        Word_Id = c.Int(),
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
                        Digest = c.String(unicode: false),
                        DebtorAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remark = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        RowVersion = c.DateTime(nullable: false, precision: 0),
                        Subject_Id = c.Int(),
                        Voucher_Id = c.Int(),
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
                        Name = c.String(unicode: false),
                        Remark = c.String(unicode: false),
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
            DropIndex("dbo.VoucherDetail", new[] { "Voucher_Id" });
            DropIndex("dbo.VoucherDetail", new[] { "Subject_Id" });
            DropIndex("dbo.Voucher", new[] { "Word_Id" });
            DropIndex("dbo.Voucher", new[] { "ModifUser_Id" });
            DropIndex("dbo.Voucher", new[] { "CreateUser_Id" });
            DropIndex("dbo.Voucher", new[] { "BelongCompany_Id" });
            DropIndex("dbo.Subject", new[] { "Type_Id" });
            DropTable("dbo.VoucherWord");
            DropTable("dbo.VoucherDetail");
            DropTable("dbo.Voucher");
            DropTable("dbo.SubjectType");
            DropTable("dbo.Subject");
            DropTable("dbo.Company");
        }
    }
}
