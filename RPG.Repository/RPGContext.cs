using Microsoft.EntityFrameworkCore;
using RPG.Domain.Entities;

namespace RPG.Repository
{
    public class RPGContext : DbContext
    {
        public RPGContext(DbContextOptions<RPGContext> options) : base (options)
        {
            
        }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Move> Moves { get; set; }
    }
}