namespace SistemaParqueo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentityEmpresaRelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "EmpresaId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "EmpresaId");
            AddForeignKey("dbo.AspNetUsers", "EmpresaId", "dbo.Empresa", "EmpresaId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "EmpresaId", "dbo.Empresa");
            DropIndex("dbo.AspNetUsers", new[] { "EmpresaId" });
            DropColumn("dbo.AspNetUsers", "EmpresaId");
        }
    }
}
