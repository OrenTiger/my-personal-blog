using System.Data.Entity;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public class BlogContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Like> Likes { get; set; }

        public BlogContext() : base("BlogContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Реализация связи М : М для моделей Пост - Тэг
            modelBuilder.Entity<Post>().HasMany(p => p.Tags)
                .WithMany(t => t.Posts)
                .Map(m => m.MapLeftKey("PostId")
                    .MapRightKey("TagId")
                    .ToTable("PostTag"));

            // Реализация связи 1 : M для моделей Пост - Комментарий
            modelBuilder.Entity<Post>().HasMany(p => p.Comments).WithRequired(c => c.Post).HasForeignKey(c => c.PostId);

            // Реализация связи 1 : M для моделей Комментарий - Лайк
            modelBuilder.Entity<Comment>().HasMany(c => c.Likes).WithRequired(l => l.Comment).HasForeignKey(l => l.CommentId);
        }
    }
}