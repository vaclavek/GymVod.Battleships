using GymVod.Battleships.DataLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace GymVod.Battleships.DataLayer
{
    public class MyContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=Data\\battleships.db");
        }
    }
}
