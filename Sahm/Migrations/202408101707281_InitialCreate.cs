namespace Sahm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarRequests",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Evaluations",
                c => new
                    {
                        EvaluationId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Comment = c.String(),
                        Emoji = c.String(),
                        EvaluationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EvaluationId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Evaluations", "UserId", "dbo.Users");
            DropForeignKey("dbo.CarRequests", "UserId", "dbo.Users");
            DropIndex("dbo.Payments", new[] { "UserId" });
            DropIndex("dbo.Evaluations", new[] { "UserId" });
            DropIndex("dbo.CarRequests", new[] { "UserId" });
            DropTable("dbo.Payments");
            DropTable("dbo.Evaluations");
            DropTable("dbo.Users");
            DropTable("dbo.CarRequests");
        }
    }
}
