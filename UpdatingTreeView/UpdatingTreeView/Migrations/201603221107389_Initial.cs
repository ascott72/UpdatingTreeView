namespace UpdatingTreeView.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.TreeViewModels",
            //    c => new
            //    {
            //        NodeID = c.Int(nullable: false, identity: true),
            //        NodeName = c.String(nullable: false),
            //        ParentID = c.Int(nullable: false),
            //    })
            //    .PrimaryKey(t => t.NodeID);

        }
        
        public override void Down()
        {
            DropTable("dbo.TreeViewModels");
        }
    }
}
