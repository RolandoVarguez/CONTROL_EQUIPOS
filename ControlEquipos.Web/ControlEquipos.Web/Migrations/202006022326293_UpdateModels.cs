namespace ControlEquipos.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModels : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.AspNetUsers", "Imagen");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Imagen", c => c.Binary());
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String());
        }
    }
}
