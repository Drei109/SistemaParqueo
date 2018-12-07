namespace SistemaParqueo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FavoritosAddedAgain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favoritos",
                c => new
                    {
                        ClienteId = c.Int(nullable: false),
                        CocheraId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClienteId, t.CocheraId })
                .ForeignKey("dbo.Cliente", t => t.ClienteId, cascadeDelete: true)
                .ForeignKey("dbo.Cochera", t => t.CocheraId, cascadeDelete: true)
                .Index(t => t.ClienteId)
                .Index(t => t.CocheraId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favoritos", "CocheraId", "dbo.Cochera");
            DropForeignKey("dbo.Favoritos", "ClienteId", "dbo.Cliente");
            DropIndex("dbo.Favoritos", new[] { "CocheraId" });
            DropIndex("dbo.Favoritos", new[] { "ClienteId" });
            DropTable("dbo.Favoritos");
        }
    }
}
