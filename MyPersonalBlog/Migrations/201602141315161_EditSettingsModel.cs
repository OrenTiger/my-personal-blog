namespace MyPersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditSettingsModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Settings", "VkAppId", c => c.String());
            AlterColumn("dbo.Settings", "VkAppSecret", c => c.String());
            AlterColumn("dbo.Settings", "GoogleAppId", c => c.String());
            AlterColumn("dbo.Settings", "GoogleAppSecret", c => c.String());
            AlterColumn("dbo.Settings", "OAuthRedirectUri", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Settings", "OAuthRedirectUri", c => c.String(nullable: false));
            AlterColumn("dbo.Settings", "GoogleAppSecret", c => c.String(nullable: false));
            AlterColumn("dbo.Settings", "GoogleAppId", c => c.String(nullable: false));
            AlterColumn("dbo.Settings", "VkAppSecret", c => c.String(nullable: false));
            AlterColumn("dbo.Settings", "VkAppId", c => c.String(nullable: false));
        }
    }
}
