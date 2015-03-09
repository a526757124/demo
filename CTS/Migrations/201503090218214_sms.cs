namespace CTS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sms : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourierCompany", "IsSendSMS", c => c.Boolean(nullable: false));
            AddColumn("dbo.Receipt", "IsPay", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Receipt", "IsPay");
            DropColumn("dbo.CourierCompany", "IsSendSMS");
        }
    }
}
