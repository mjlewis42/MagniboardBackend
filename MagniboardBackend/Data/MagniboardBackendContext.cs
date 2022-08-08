using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MagniboardBackend.Data.EntityModels;

namespace MagniboardBackend.Data
{
    public class MagniboardDbConnection : DbContext
    {
        public MagniboardDbConnection (DbContextOptions<MagniboardDbConnection> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Template>()
                .Property(p => p.showTemplateHeader)
                .HasConversion(
                    v => v ? 1 : 0,
                    v => (v == 1));
            modelBuilder
                .Entity<Cell>()
                .Property(p => p.header)
                .HasConversion(
                    v => v ? 1 : 0,
                    v => (v == 1));
            modelBuilder
                .Entity<Cell>()
                .Property(p => p.droppable)
                .HasConversion(
                    v => v ? 1 : 0,
                    v => (v == 1));
            modelBuilder
                .Entity<Template>()
                .Property(p => p.isActive)
                .HasConversion(
                    v => v ? 1 : 0,
                    v => (v == 1));
        }

        public DbSet<Magnet> Magnet { get; set; }
        public DbSet<Template> Template { get; set; }
        public DbSet<Board> Board { get; set; }
        public DbSet<Cell> Cell { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Role> Role { get; set; }
    }
}
