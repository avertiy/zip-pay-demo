﻿using Microsoft.EntityFrameworkCore;
using ZipPayDemo.Domain.Entities;

namespace ZipPayDemo.Api.Data
{
    public class DataContext : DbContext
    {
        protected DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var users = modelBuilder.Entity<User>();
            users.HasIndex(u => u.Email).IsUnique();
            users.ToTable("Users");

            modelBuilder.Entity<Account>().ToTable("Accounts");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
