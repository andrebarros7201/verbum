using Microsoft.EntityFrameworkCore;
using Verbum.API.Models;

namespace Verbum.API.Data;

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Db Tables
    public DbSet<User?> Users { get; set; }
    public DbSet<Community> Communities { get; set; }
    public DbSet<UserCommunity> UserCommunities { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<VotePost> VotePosts { get; set; }
    public DbSet<VoteComment> VoteComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        // USER COMMUNITY - Composite Key
        modelBuilder.Entity<UserCommunity>()
            .HasKey(c => new { c.UserId, c.CommunityId });

        // USER COMMUNITY - USER relationship
        modelBuilder.Entity<UserCommunity>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.CommunitiesJoined)
            .HasForeignKey(uc => uc.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // USER COMMUNITY - COMMUNITY relationship
        modelBuilder.Entity<UserCommunity>()
            .HasOne(uc => uc.Community)
            .WithMany(c => c.Members)
            .HasForeignKey(uc => uc.CommunityId)
            .OnDelete(DeleteBehavior.Cascade);

        // COMMUNITY - OWNER relationship
        modelBuilder.Entity<Community>()
            .HasOne(c => c.Owner)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // POST - USER relationship
        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // POST - COMMUNITY relationship
        modelBuilder.Entity<Post>()
            .HasOne(p => p.Community)
            .WithMany(c => c.Posts)
            .HasForeignKey(p => p.CommunityId)
            .OnDelete(DeleteBehavior.Cascade);

        // COMMENT - USER relationship
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // COMMENT - POST relationship
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        // VOTE - USER relationship
        modelBuilder.Entity<VotePost>()
            .HasOne(v => v.User)
            .WithMany(u => u.VotePosts)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<VoteComment>()
            .HasOne(v => v.User)
            .WithMany(u => u.VoteComments)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // VOTE - POST relationship
        modelBuilder.Entity<VotePost>()
            .HasOne(v => v.Post)
            .WithMany(p => p.Votes)
            .HasForeignKey(v => v.PostId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        // VOTE - COMMENT relationship
        modelBuilder.Entity<VoteComment>()
            .HasOne(v => v.Comment)
            .WithMany(c => c.Votes)
            .HasForeignKey(v => v.CommentId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        // Additional optimizations
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<Community>()
            .HasIndex(c => c.Name)
            .IsUnique();

        modelBuilder.Entity<Post>()
            .HasIndex(p => p.CommunityId);

        modelBuilder.Entity<Comment>()
            .HasIndex(c => c.PostId);

        modelBuilder.Entity<VotePost>()
            .HasIndex(v => new { v.UserId, v.PostId })
            .HasFilter("[PostId] IS NOT NULL");

        modelBuilder.Entity<VotePost>()
            .HasKey(c => new { c.UserId, c.PostId });

        modelBuilder.Entity<VoteComment>()
            .HasIndex(v => new { v.UserId, v.CommentId })
            .HasFilter("[CommentId] IS NOT NULL");

        modelBuilder.Entity<VoteComment>()
            .HasKey(c => new { c.UserId, c.CommentId });
    }
}