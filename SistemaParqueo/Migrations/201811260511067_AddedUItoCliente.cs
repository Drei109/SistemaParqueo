namespace SistemaParqueo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUItoCliente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cliente", "UID", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cliente", "UID");
        }
    }
}
