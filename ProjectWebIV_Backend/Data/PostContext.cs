using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectWebIV_Backend.Models;
using System;

namespace ProjectWebIV_Backend.Data
{
    public class PostContext : IdentityDbContext
    {
        #region Properties
        public DbSet<Post> Posts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        #endregion

        #region Constructor
        public PostContext(DbContextOptions<PostContext> options) : base(options){}
        #endregion

        #region Method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne()
                .IsRequired()
                .HasForeignKey("PostId")
                .OnDelete(DeleteBehavior.Cascade); //Shadow property
            modelBuilder.Entity<Post>().Property(r => r.Title).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Comment>().Property(r => r.Text).IsRequired();
            modelBuilder.Entity<Comment>().Property(r => r.Name).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Customer>().Property(c => c.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Customer>().Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Customer>().Property(c => c.Email).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Post>().HasData(
                new Post { Id = 1, Title = "Post 1", Created = DateTime.Now, Description = "testje" },
                new Post { Id = 2, Title = "Post 2", Created = DateTime.Now }
            );

            modelBuilder.Entity<Comment>().HasData(
            //Shadow property can be used for the foreign key, in combination with anaonymous objects
            new { Id = 1, Text = "Comment 1", Created = DateTime.Now, Name = "Robbie Verdurme", PostId = 1, },
            new { Id = 2, Text = "Comment 2", Created = DateTime.Now, Name = "Robbie Verdurme", PostId = 1 },
            new { Id = 3, Text = "Comment 3", Created = DateTime.Now, Name = "Robbie Verdurme", PostId = 1 }
            );
        }
        #endregion
    }
}
