namespace GmailAPIDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mail", "Date", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mail", "Date");
        }
    }
}
