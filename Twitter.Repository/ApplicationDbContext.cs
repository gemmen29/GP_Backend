using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.Models;

namespace Twitter.Repository
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Tweet> Tweet { get; set; }
        public DbSet<Reply> Reply { get; set; }
        public DbSet<Following> Following { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserBookmarks>()
                .HasKey(b => new { b.UserId, b.TweetId });
            modelBuilder.Entity<UserLikes>()
                .HasKey(b => new { b.UserId, b.TweetId });
            modelBuilder.Entity<Following>()
                .HasOne(u => u.FollowerUser)
                .WithMany(u => u.Following)
                .IsRequired()
    .           OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Following>()
                .HasOne(u => u.FollowingUser)
                .WithMany(u => u.Followers)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Following>()
                .HasKey(b => new { b.FollowerId, b.FollowingId });
            modelBuilder.Entity<Tweet>()
                .HasOne(u => u.RespondedTweet)
                .WithOne(u => u.Tweet)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Tweet>()
                .HasMany(u => u.Replies)
                .WithOne(u => u.ReplyTweet)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Reply>()
                .HasKey(r => new { r.TweetId, r.ReplyId});
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
