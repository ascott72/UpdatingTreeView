namespace UpdatingTreeView.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TreeViewModels", "ParentID", c => c.Int(nullable: false));
            DropColumn("dbo.TreeViewModels", "NodeDepth");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TreeViewModels", "NodeDepth", c => c.Int(nullable: false));
            DropColumn("dbo.TreeViewModels", "ParentID");
        }
    }
}
