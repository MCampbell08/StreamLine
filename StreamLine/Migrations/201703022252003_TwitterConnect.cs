namespace StreamLine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TwitterConnect : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TwitterConnect", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TwitterConnect");
        }
    }
}
