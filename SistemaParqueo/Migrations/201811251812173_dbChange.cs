namespace SistemaParqueo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cochera", "Direccion", c => c.String(nullable: false, maxLength: 250, unicode: false));
            AlterColumn("dbo.Cochera", "Longitud", c => c.String(nullable: false, maxLength: 250, unicode: false));
            AlterColumn("dbo.Cochera", "Latitud", c => c.String(nullable: false, maxLength: 250, unicode: false));
            DropColumn("dbo.Persona", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Persona", "Email", c => c.String(maxLength: 250, unicode: false));
            AlterColumn("dbo.Cochera", "Latitud", c => c.String(maxLength: 250, unicode: false));
            AlterColumn("dbo.Cochera", "Longitud", c => c.String(maxLength: 250, unicode: false));
            AlterColumn("dbo.Cochera", "Direccion", c => c.String(maxLength: 250, unicode: false));
        }
    }
}
