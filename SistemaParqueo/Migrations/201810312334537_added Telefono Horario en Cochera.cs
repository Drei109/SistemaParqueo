namespace SistemaParqueo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTelefonoHorarioenCochera : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cochera", "Telefono", c => c.String(maxLength: 30));
            AddColumn("dbo.Cochera", "HorarioAtencion", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cochera", "HorarioAtencion");
            DropColumn("dbo.Cochera", "Telefono");
        }
    }
}
