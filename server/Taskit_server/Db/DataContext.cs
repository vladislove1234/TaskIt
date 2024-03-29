﻿using System;
using Microsoft.EntityFrameworkCore;
using Taskit_server.Model.Entities;
using Taskit_server.Model.Entities.RoleModels;
using Taskit_server.Model.Entities.TakenTaskModels;
using Taskit_server.Model.Entities.TaskModels;
using Taskit_server.Model.Entities.TeamModels;
using Taskit_server.Model.Entities.UserModels;

namespace Taskit_server.Db
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<TakenTask> TakenTasks { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies();
            }

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
