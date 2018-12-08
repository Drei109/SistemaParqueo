namespace SistemaParqueo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FechaAddedToReserva : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reserva", "Fecha", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reserva", "Fecha");
        }
    }
}
