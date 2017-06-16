namespace eHal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customuser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomApplicationUsers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sellers", "Id", "dbo.CustomApplicationUsers");
            DropForeignKey("dbo.Listings", "CustomApplicationUser_Id", "dbo.CustomApplicationUsers");
            DropIndex("dbo.CustomApplicationUsers", new[] { "ApplicationUserId" });
            DropColumn("dbo.CustomApplicationUsers", "Id");
            RenameColumn(table: "dbo.CustomApplicationUsers", name: "ApplicationUserId", newName: "Id");
            
            AddColumn("dbo.CustomApplicationUsers", "sellerId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "CustomApplicationUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Sellers", "CustomApplicationUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.CustomApplicationUsers", "Id", c => c.Int(nullable: false));
            AddForeignKey("dbo.CustomApplicationUsers", "Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Sellers", "Id", "dbo.CustomApplicationUsers", "Id");
            AddForeignKey("dbo.Listings", "CustomApplicationUser_Id", "dbo.CustomApplicationUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Listings", "CustomApplicationUser_Id", "dbo.CustomApplicationUsers");
            DropForeignKey("dbo.Sellers", "Id", "dbo.CustomApplicationUsers");
            DropForeignKey("dbo.CustomApplicationUsers", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.CustomApplicationUsers", new[] { "Id" });
            DropPrimaryKey("dbo.CustomApplicationUsers");
            AlterColumn("dbo.CustomApplicationUsers", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Sellers", "CustomApplicationUserId");
            DropColumn("dbo.AspNetUsers", "CustomApplicationUserId");
            DropColumn("dbo.CustomApplicationUsers", "sellerId");
            AddPrimaryKey("dbo.CustomApplicationUsers", "Id");
            RenameColumn(table: "dbo.CustomApplicationUsers", name: "Id", newName: "ApplicationUserId");
            AddColumn("dbo.CustomApplicationUsers", "Id", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.CustomApplicationUsers", "ApplicationUserId");
            AddForeignKey("dbo.Listings", "CustomApplicationUser_Id", "dbo.CustomApplicationUsers", "Id");
            AddForeignKey("dbo.Sellers", "Id", "dbo.CustomApplicationUsers", "Id");
            AddForeignKey("dbo.CustomApplicationUsers", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
