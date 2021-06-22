namespace GeneralStoreMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionReAddedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "DateOfTransaction", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "DateOfTransaction");
        }
    }
}
