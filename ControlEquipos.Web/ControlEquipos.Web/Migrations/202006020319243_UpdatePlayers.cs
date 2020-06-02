namespace ControlEquipos.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePlayers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "Position", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "Position");
        }
    }
}
