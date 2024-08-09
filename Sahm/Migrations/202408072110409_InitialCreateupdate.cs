namespace Sahm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreateupdate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "QRCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "QRCode", c => c.String());
        }
    }
}
