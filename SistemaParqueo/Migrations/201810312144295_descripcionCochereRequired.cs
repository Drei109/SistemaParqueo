namespace SistemaParqueo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class descripcionCochereRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cochera", "Descripcion", c => c.String(nullable: false, maxLength: 250, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cochera", "Descripcion", c => c.String(maxLength: 250, unicode: false));
        }
    }
}
