namespace StreamLine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpotifyPlaylistUri : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SpotifyPlaylistUri", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SpotifyPlaylistUri");
        }
    }
}
