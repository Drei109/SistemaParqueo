namespace SistemaParqueo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmpresaIdNullified : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "EmpresaId", "dbo.Empresa");
            DropIndex("dbo.AspNetUsers", new[] { "EmpresaId" });
            AlterColumn("dbo.AspNetUsers", "EmpresaId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "EmpresaId");
            AddForeignKey("dbo.AspNetUsers", "EmpresaId", "dbo.Empresa", "EmpresaId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "EmpresaId", "dbo.Empresa");
            DropIndex("dbo.AspNetUsers", new[] { "EmpresaId" });
            AlterColumn("dbo.AspNetUsers", "EmpresaId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "EmpresaId");
            AddForeignKey("dbo.AspNetUsers", "EmpresaId", "dbo.Empresa", "EmpresaId", cascadeDelete: true);
        }
    }
}
