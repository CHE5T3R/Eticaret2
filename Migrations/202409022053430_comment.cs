namespace Eticaret2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Product_Id", c => c.Int());
            CreateIndex("dbo.Comments", "Product_Id");
            AddForeignKey("dbo.Comments", "Product_Id", "dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Product_Id", "dbo.Products");
            DropIndex("dbo.Comments", new[] { "Product_Id" });
            DropColumn("dbo.Comments", "Product_Id");
        }
    }
}
