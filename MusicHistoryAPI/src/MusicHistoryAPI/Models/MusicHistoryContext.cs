using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicHistoryAPI.Models
{
    public class MusicHistoryContext : DbContext
    {
        public MusicHistoryContext(DbContextOptions<MusicHistoryContext> options)
            : base(options)
        { }

        public DbSet<Artist> Artist { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<Track> Track { get; set; }
        public DbSet<Customer> Customer { get; set; }


    }
}
