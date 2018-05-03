using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Demo
{
     public class BloggingContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public BloggingContext(DbContextOptions<BloggingContext> config): base(config)
        {

        }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     //optionsBuilder.UseSqlServer(@"Server=db;Database=blog;User=sa;Password=DemoPassword.1;");
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Post>(entity =>
                {
                    entity.ToTable("posts");
                    entity.Property(e=>e.PostId).HasColumnType("bigint");
                    entity.Property(e=>e.Title).HasColumnType("nvarchar(500)");
                    entity.Property(e=>e.Content).HasColumnType("nvarchar(max)");
                }
            );
        }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

    }
}