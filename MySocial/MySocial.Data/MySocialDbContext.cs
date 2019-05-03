using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySocial.Data.EntityConfiguration;
using MySocial.Data.Models;
using System;

namespace MySocial.Data
{
    public class MySocialDbContext : IdentityDbContext<User>
    {
        public MySocialDbContext(DbContextOptions options) : base(options)
        {
        }

        public MySocialDbContext()
        {        
        }
        
        public DbSet<Chat> Chats { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Message> Message { get; set; }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration<User>(new UserConfiguration());
            builder.ApplyConfiguration<Post>(new PostConfiguration());
            builder.ApplyConfiguration<Comment>(new CommentConfiguration());
            builder.ApplyConfiguration<Message>(new MessageConfiguration());
            builder.ApplyConfiguration<Chat>(new ChatConfiguration());  
        }
    }
}