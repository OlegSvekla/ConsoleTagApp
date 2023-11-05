﻿using ConsoleTagApp.Domain.Entities;
using ConsoleTagApp.Infrastructure.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTagApp.Infrastructure.Data
{
    public class ConsoleDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagToUser> TagsToUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TagToUserConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
