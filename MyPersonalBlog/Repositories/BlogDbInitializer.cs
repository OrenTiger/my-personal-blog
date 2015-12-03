using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public class BlogDbInitializer : DropCreateDatabaseAlways<BlogContext>
    {
        protected override void Seed(BlogContext db)
        {
            List<Post> testPosts = new List<Post>();

            testPosts.Add(new Post { Id = 1, Title = "Test blog post #1", IntroText = "Intro Text", MainText = "Main Text", CreateDate = DateTime.Now, IsPublished = true });
            testPosts.Add(new Post { Id = 2, Title = "Test blog post #2", IntroText = "Intro Text", MainText = "Main Text", CreateDate = DateTime.Now.AddMinutes(1), IsPublished = true });
            testPosts.Add(new Post { Id = 3, Title = "Test blog post #3", IntroText = "Intro Text", MainText = "Main Text", CreateDate = DateTime.Now.AddMinutes(2), IsPublished = true });
            testPosts.Add(new Post { Id = 4, Title = "Test blog post #4", IntroText = "Intro Text", MainText = "Main Text", CreateDate = DateTime.Now.AddMinutes(3), IsPublished = false });
            testPosts.Add(new Post { Id = 5, Title = "Test blog post #5", IntroText = "Intro Text", MainText = "Main Text", CreateDate = DateTime.Now.AddMinutes(4), IsPublished = true });
            testPosts.Add(new Post { Id = 6, Title = "Test blog post #6", IntroText = "Intro Text", MainText = "Main Text", CreateDate = DateTime.Now.AddMinutes(5), IsPublished = true });
            testPosts.Add(new Post { Id = 7, Title = "Test blog post #7", IntroText = "Intro Text", MainText = "Main Text", CreateDate = DateTime.Now.AddMinutes(6), IsPublished = false });
            testPosts.Add(new Post { Id = 8, Title = "Test blog post #8", IntroText = "Intro Text", MainText = "Main Text", CreateDate = DateTime.Now.AddMinutes(7), IsPublished = true });
            testPosts.Add(new Post { Id = 9, Title = "Test blog post #9", IntroText = "Intro Text", MainText = "Main Text", CreateDate = DateTime.Now.AddMinutes(8), IsPublished = true });

            testPosts[0].Title = "Lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum";
            testPosts[0].IntroText = "Lorem ipsum dolor sit amet, id scaevola maluisset omittantur vis, vivendum praesent assentior his id. In vis commodo conclusionemque. Aliquip docendi antiopam his eu. In illud patrioque similique sed. At mea nostrum percipitur, quidam quaeque petentium nam ea. Eum cu tractatos patrioque dissentias, at mel consul vocent, nibh duis legendos eam at.";
            testPosts[0].MainText = "Ad nam commodo numquam verterem. Vis ea illum zril, quo no adhuc everti periculis. Delenit definitiones has ne, nec eu maluisset rationibus. Cu liber fierent ius. Eu mei animal molestie splendide, te mei suas wisi elitr. At iriure quaeque alienum vix. Ius ut eligendi comprehensam, veniam debitis signiferumque ex vim. Laudem aperiri ex mei, doming mediocrem pri ex. Omnium sapientem vim an, nostro admodum percipitur eu sed. Ex partem graecis ius. Novum pertinacia cotidieque in eam, paulo nemore tincidunt vis ut. Has cu nulla nominati, admodum eligendi appellantur et vix, posse graece ad duo. Eum an oblique persius vivendum, summo salutatus ad est. Vis et saperet debitis. Cu epicuri volutpat cotidieque eos. Mei case laoreet no, phaedrum sapientem inciderint ad ius. Everti lucilius quaestio ei nam, sonet choro ne per, natum animal fuisset duo ea. Te verterem reprimique interesset sit, everti sanctus minimum mea at. Id justo postulant est, te nam essent gloriatur, duo volumus detraxit te. In labore delectus per, in per graece denique. Et mei sale omnis, aliquid copiosae laboramus ei vel. Mei ne ludus munere impedit, modus nonumes ne vim.";

            db.Posts.Add(testPosts[0]);
            db.Posts.Add(testPosts[1]);
            db.Posts.Add(testPosts[2]);
            db.Posts.Add(testPosts[3]);
            db.Posts.Add(testPosts[4]);
            db.Posts.Add(testPosts[5]);
            db.Posts.Add(testPosts[6]);
            db.Posts.Add(testPosts[7]);
            db.Posts.Add(testPosts[8]);

            List<Tag> testTags = new List<Tag>();

            testTags.Add(new Tag { Id = 1, Name = "C#", Posts = new List<Post>() { testPosts[1], testPosts[2] } });
            testTags.Add(new Tag { Id = 2, Name = "ASP.NET MVC", Posts = new List<Post>() { testPosts[0], testPosts[1] } });
            testTags.Add(new Tag { Id = 3, Name = "Web API" });
            testTags.Add(new Tag { Id = 4, Name = "Web Dev", Posts = new List<Post>() { testPosts[0] } });
            testTags.Add(new Tag { Id = 5, Name = "Visual Studio", Posts = new List<Post>() { testPosts[4] } });
            testTags.Add(new Tag { Id = 6, Name = "WinPhone", Posts = new List<Post>() { testPosts[7] } });
            testTags.Add(new Tag { Id = 7, Name = "Xamarin", Posts = new List<Post>() { testPosts[8] } });
            testTags.Add(new Tag { Id = 8, Name = ".NET", Posts = new List<Post>() { testPosts[5] } });
            testTags.Add(new Tag { Id = 9, Name = "Unity3D", Posts = new List<Post>() { testPosts[6] } });

            db.Tags.Add(testTags[0]);
            db.Tags.Add(testTags[1]);
            db.Tags.Add(testTags[2]);
            db.Tags.Add(testTags[3]);
            db.Tags.Add(testTags[4]);
            db.Tags.Add(testTags[5]);
            db.Tags.Add(testTags[6]);
            db.Tags.Add(testTags[7]);
            db.Tags.Add(testTags[8]);

            db.Comments.Add(new Comment { Id = 1, Text = "Test Comment #1", CreateDate = DateTime.Now, Username = "User 1", IsApproved = false, PostId = 1 });
            db.Comments.Add(new Comment { Id = 2, Text = "Test Comment #2", CreateDate = DateTime.Now.AddMinutes(1), Username = "User 2", IsApproved = false, PostId = 1 });
            db.Comments.Add(new Comment { Id = 3, Text = "Test Comment #3", CreateDate = DateTime.Now.AddMinutes(2), Username = "User 2", IsApproved = true, PostId = 1 });
            db.Comments.Add(new Comment { Id = 4, Text = "Test Comment #3", CreateDate = DateTime.Now.AddMinutes(3), Username = "User 4", IsApproved = true, PostId = 1 });

            // TODO: Добавить функцию BCrypt-хэширования паролей
            // https://cmatskas.com/a-simple-net-password-hashing-implementation-using-bcrypt/
            db.Admins.Add(new Admin { Login = "admin", Username = "Timur Basyrov", PasswordHash = "12345" });

            db.SaveChanges();
            base.Seed(db);
        }
    }
}