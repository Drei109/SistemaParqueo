namespace SistemaParqueo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedClienteEstadoIdinCliente : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ClienteEstado", newName: "ClienteEstadoT");
            RenameColumn(table: "dbo.ClienteEstadoT", name: "ClienteEstadoId", newName: "ClienteEstadoIdT");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.ClienteEstadoT", name: "ClienteEstadoIdT", newName: "ClienteEstadoId");
            RenameTable(name: "dbo.ClienteEstadoT", newName: "ClienteEstado");
        }
    }
}
