using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyPersonalBlog.Models
{
    public class BlogDbInitializer : DropCreateDatabaseAlways<BlogContext>
    {
        protected override void Seed(BlogContext db)
        {
            List<Post> testPosts = new List<Post>(3);

            testPosts.Add(new Post { Id = 1, Title = "Test blog post #1", IntroText = "Intro Text", MainText = "Main Text", CreateDate = DateTime.Now, Published = true });
            testPosts.Add(new Post { Id = 2, Title = "Test blog post #2", IntroText = "Intro Text", MainText = "Main Text", CreateDate = DateTime.Now, Published = true });
            testPosts.Add(new Post { Id = 3, Title = "Test blog post #3", IntroText = "Intro Text", MainText = "Main Text", CreateDate = DateTime.Now, Published = true });

            db.Posts.Add(testPosts[0]);
            db.Posts.Add(testPosts[1]);
            db.Posts.Add(testPosts[2]);

            List<Tag> testTags = new List<Tag>(3);

            testTags.Add(new Tag { Id = 1, Name = "C#", Posts = new List<Post>() { testPosts[1], testPosts[2] } });
            testTags.Add(new Tag { Id = 2, Name = "ASP.NET MVC", Posts = new List<Post>() { testPosts[0], testPosts[1] } });
            testTags.Add(new Tag { Id = 3, Name = "Web API" });

            db.Tags.Add(testTags[0]);
            db.Tags.Add(testTags[1]);
            db.Tags.Add(testTags[2]);

            base.Seed(db);
        }
    }
}