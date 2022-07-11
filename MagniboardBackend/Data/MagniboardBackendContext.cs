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
        public MagniboardDbConnection (DbContextOptions<MagniboardDbConnection> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Table>()
                .Property(p => p.showTableHeader)
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
                .Entity<Board>()
                .Property(p => p.isActive)
                .HasConversion(
                    v => v ? 1 : 0,
                    v => (v == 1));
        }

        public DbSet<Magnets> Magnets { get; set; }
        public DbSet<Table> Table { get; set; }
        public DbSet<Board> Board { get; set; }
    }
}
