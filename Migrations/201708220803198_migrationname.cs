namespace MvcShopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrationname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategories", "CategoryImg", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCategories", "CategoryImg");
        }
    }
}
