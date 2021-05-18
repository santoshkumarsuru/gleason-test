﻿using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;
using WebApi.Entities;

namespace UserService.DBContext
{
    public class SampleDBContext : DbContext
    {
        private static bool _created = false;
        public SampleDBContext()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
                Users.Add(new User
                {
                    Username = "test",
                    Password = "test",
                    Email = "test@test.com",
                    FirstName = "test",
                    LastName = "test"
                });
                SaveChanges();
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            string binPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string dbPath = binPath + "\\sample.db";
            optionbuilder.UseSqlite(@"Data Source=" + dbPath);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(c => c.Roles);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
    }
}
