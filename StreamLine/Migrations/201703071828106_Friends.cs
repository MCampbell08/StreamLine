namespace StreamLine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Friends : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Friends", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Friends");
        }
    }
}
