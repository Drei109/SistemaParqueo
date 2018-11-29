namespace SistemaParqueo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CantidadEspaciosAddedToCochera : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cochera", "CantidadEspacios", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cochera", "CantidadEspacios");
        }
    }
}
