namespace SistemaParqueo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeyIdentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CocheraUsuario", "AspNetUsersId", c => c.String(maxLength: 128));
            AddColumn("dbo.Persona", "AspNetUsersId", c => c.String(maxLength: 128));
            CreateIndex("dbo.CocheraUsuario", "AspNetUsersId");
            CreateIndex("dbo.Persona", "AspNetUsersId");
            AddForeignKey("dbo.CocheraUsuario", "AspNetUsersId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Persona", "AspNetUsersId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Persona", "AspNetUsersId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CocheraUsuario", "AspNetUsersId", "dbo.AspNetUsers");
            DropIndex("dbo.Persona", new[] { "AspNetUsersId" });
            DropIndex("dbo.CocheraUsuario", new[] { "AspNetUsersId" });
            DropColumn("dbo.Persona", "AspNetUsersId");
            DropColumn("dbo.CocheraUsuario", "AspNetUsersId");
        }
    }
}
