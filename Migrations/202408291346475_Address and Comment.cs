namespace Eticaret2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressandComment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        AdresBasligi = c.String(),
                        Adres = c.String(),
                        Sehir = c.String(),
                        Semt = c.String(),
                        Mahalle = c.String(),
                        PostaKodu = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Comments");
            DropTable("dbo.Addresses");
        }
    }
}
