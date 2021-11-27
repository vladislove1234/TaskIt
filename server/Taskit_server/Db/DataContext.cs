using System;
using Microsoft.EntityFrameworkCore;
using Taskit_server.Model.Entities;
using Taskit_server.Model.Entities.UserModels;

namespace Taskit_server.Db
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
