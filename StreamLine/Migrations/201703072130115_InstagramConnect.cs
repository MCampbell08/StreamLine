namespace StreamLine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InstagramConnect : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "InstagramConnect", c=>c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "InstagramConnect");
        }
    }
}
