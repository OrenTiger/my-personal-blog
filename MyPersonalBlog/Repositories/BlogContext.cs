﻿using System.Linq;
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
        }
    }
}