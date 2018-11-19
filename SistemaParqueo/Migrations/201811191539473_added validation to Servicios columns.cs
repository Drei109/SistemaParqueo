namespace SistemaParqueo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedvalidationtoServicioscolumns : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Servicio", "Descripcion", c => c.String(nullable: false, maxLength: 250, unicode: false));
            AlterColumn("dbo.Servicio", "Costo", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Servicio", "Costo", c => c.Decimal(storeType: "money"));
            AlterColumn("dbo.Servicio", "Descripcion", c => c.String(maxLength: 250, unicode: false));
        }
    }
}
