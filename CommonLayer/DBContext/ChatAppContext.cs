using CommonLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.DBContext
{
    public class ChatAppContext : DbContext
    {
        public ChatAppContext()
        {

        }
        public ChatAppContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserRegistration> UserDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserRegistration>(entity =>
            {
                entity.HasIndex(u => u.EmailAddress)
                .IsUnique();
            });
        }
    }
}
