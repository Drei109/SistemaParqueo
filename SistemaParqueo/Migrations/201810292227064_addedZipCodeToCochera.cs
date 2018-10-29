namespace SistemaParqueo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedZipCodeToCochera : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cochera", "CodigoPostal", c => c.String(maxLength: 9));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cochera", "CodigoPostal");
        }
    }
}
