namespace CTS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourierCompany",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourierName = c.String(maxLength: 200, storeType: "nvarchar"),
                        CourierCode = c.String(maxLength: 50, storeType: "nvarchar"),
                        ContactName = c.String(maxLength: 30, storeType: "nvarchar"),
                        ContactPhone = c.String(maxLength: 12, storeType: "nvarchar"),
                        ContactMobilePhone = c.String(maxLength: 12, storeType: "nvarchar"),
                        IsDefault = c.Boolean(nullable: false),
                        Remark = c.String(maxLength: 200, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        RowVersion = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(maxLength: 30, storeType: "nvarchar"),
                        CustomerPhone = c.String(maxLength: 12, storeType: "nvarchar"),
                        CustomerAddress = c.String(maxLength: 200, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 200, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        RowVersion = c.DateTime(nullable: false, precision: 0),
                        CustomerOftenCompany_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourierCompany", t => t.CustomerOftenCompany_Id)
                .Index(t => t.CustomerOftenCompany_Id);
            
            CreateTable(
                "dbo.Receipt",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourierNumber = c.String(maxLength: 20, storeType: "nvarchar"),
                        CustomerName = c.String(maxLength: 30, storeType: "nvarchar"),
                        CustomerPhone = c.String(maxLength: 12, storeType: "nvarchar"),
                        CustomerAddress = c.String(maxLength: 200, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 200, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        RowVersion = c.DateTime(nullable: false, precision: 0),
                        BelongCompany_Id = c.Int(nullable: false),
                        TakeInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourierCompany", t => t.BelongCompany_Id, cascadeDelete: true)
                .ForeignKey("dbo.TakeInfo", t => t.TakeInfo_Id)
                .Index(t => t.BelongCompany_Id)
                .Index(t => t.TakeInfo_Id);
            
            CreateTable(
                "dbo.TakeInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Remark = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        RowVersion = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Send",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourierNumber = c.String(maxLength: 20, storeType: "nvarchar"),
                        CustomerName = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        CustomerPhone = c.String(nullable: false, maxLength: 12, storeType: "nvarchar"),
                        CustomerAddress = c.String(maxLength: 200, storeType: "nvarchar"),
                        RecipientName = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        RecipientPhone = c.String(nullable: false, maxLength: 12, storeType: "nvarchar"),
                        RecipientAddress = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Weight = c.Decimal(nullable: false, precision: 12, scale: 4),
                        Price = c.Decimal(nullable: false, precision: 12, scale: 2),
                        CostAmount = c.Decimal(nullable: false, precision: 12, scale: 2),
                        IsSendOut = c.Boolean(nullable: false),
                        Remark = c.String(maxLength: 200, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        RowVersion = c.DateTime(nullable: false, precision: 0),
                        BelongCompany_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourierCompany", t => t.BelongCompany_Id)
                .Index(t => t.BelongCompany_Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Send", "BelongCompany_Id", "dbo.CourierCompany");
            DropForeignKey("dbo.Receipt", "TakeInfo_Id", "dbo.TakeInfo");
            DropForeignKey("dbo.Receipt", "BelongCompany_Id", "dbo.CourierCompany");
            DropForeignKey("dbo.Customer", "CustomerOftenCompany_Id", "dbo.CourierCompany");
            DropIndex("dbo.Send", new[] { "BelongCompany_Id" });
            DropIndex("dbo.Receipt", new[] { "TakeInfo_Id" });
            DropIndex("dbo.Receipt", new[] { "BelongCompany_Id" });
            DropIndex("dbo.Customer", new[] { "CustomerOftenCompany_Id" });
            DropTable("dbo.Send");
            DropTable("dbo.TakeInfo");
            DropTable("dbo.Receipt");
            DropTable("dbo.Customer");
            DropTable("dbo.CourierCompany");
        }
    }
}
