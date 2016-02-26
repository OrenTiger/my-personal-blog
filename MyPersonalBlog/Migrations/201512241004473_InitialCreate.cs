namespace MyPersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 30),
                        Username = c.String(nullable: false, maxLength: 30),
                        PasswordHash = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        AvatarUrl = c.String(maxLength: 255),
                        Text = c.String(nullable: false, maxLength: 1000),
                        CreateDate = c.DateTime(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentId = c.Int(nullable: false),
                        UserId = c.String(nullable: false),
                        UserProvider = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .Index(t => t.CommentId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 140),
                        IntroText = c.String(nullable: false, maxLength: 1000),
                        MainText = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        IsPublished = c.Boolean(nullable: false),
                        ViewsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostListPageSize = c.Int(nullable: false),
                        PageSize = c.Int(nullable: false),
                        AdminEmail = c.String(nullable: false, maxLength: 254),
                        VkAppId = c.String(nullable: false),
                        VkAppSecret = c.String(nullable: false),
                        GoogleAppId = c.String(nullable: false),
                        GoogleAppSecret = c.String(nullable: false),
                        OAuthRedirectUri = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostTag",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.TagId })
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostTag", "TagId", "dbo.Tags");
            DropForeignKey("dbo.PostTag", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Likes", "CommentId", "dbo.Comments");
            DropIndex("dbo.PostTag", new[] { "TagId" });
            DropIndex("dbo.PostTag", new[] { "PostId" });
            DropIndex("dbo.Likes", new[] { "CommentId" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropTable("dbo.PostTag");
            DropTable("dbo.Settings");
            DropTable("dbo.Tags");
            DropTable("dbo.Posts");
            DropTable("dbo.Likes");
            DropTable("dbo.Comments");
            DropTable("dbo.Admins");
        }
    }
}
