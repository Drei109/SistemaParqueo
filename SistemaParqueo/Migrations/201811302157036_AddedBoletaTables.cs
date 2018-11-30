namespace SistemaParqueo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBoletaTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoletaCabeceras",
                c => new
                    {
                        BoletaCabeceraId = c.Int(nullable: false, identity: true),
                        ReservaId = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false, storeType: "date"),
                        ClienteId = c.Int(nullable: false),
                        Estado = c.String(),
                        Subtotal = c.Decimal(nullable: false, storeType: "money"),
                        Total = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.BoletaCabeceraId)
                .ForeignKey("dbo.Cliente", t => t.ClienteId, cascadeDelete: true)
                .ForeignKey("dbo.Reserva", t => t.ReservaId, cascadeDelete: true)
                .Index(t => t.ReservaId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.BoletaDetalles",
                c => new
                    {
                        BoletaDetalleId = c.Int(nullable: false, identity: true),
                        BoletaCabeceraId = c.Int(nullable: false),
                        ServicioId = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        Total = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.BoletaDetalleId)
                .ForeignKey("dbo.BoletaCabeceras", t => t.BoletaCabeceraId, cascadeDelete: true)
                .ForeignKey("dbo.Servicio", t => t.ServicioId, cascadeDelete: true)
                .Index(t => t.BoletaCabeceraId)
                .Index(t => t.ServicioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoletaCabeceras", "ReservaId", "dbo.Reserva");
            DropForeignKey("dbo.BoletaCabeceras", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.BoletaDetalles", "ServicioId", "dbo.Servicio");
            DropForeignKey("dbo.BoletaDetalles", "BoletaCabeceraId", "dbo.BoletaCabeceras");
            DropIndex("dbo.BoletaDetalles", new[] { "ServicioId" });
            DropIndex("dbo.BoletaDetalles", new[] { "BoletaCabeceraId" });
            DropIndex("dbo.BoletaCabeceras", new[] { "ClienteId" });
            DropIndex("dbo.BoletaCabeceras", new[] { "ReservaId" });
            DropTable("dbo.BoletaDetalles");
            DropTable("dbo.BoletaCabeceras");
        }
    }
}
