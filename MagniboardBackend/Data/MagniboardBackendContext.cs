using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MagniboardBackend.Data
{
    public class MagniboardDbConnection : DbContext
    {
        public MagniboardDbConnection (DbContextOptions<MagniboardDbConnection> options)
            : base(options)
        {
        }

        public DbSet<MagniboardBackend.Data.Magnets>? Magnets { get; set; }
    }
}
