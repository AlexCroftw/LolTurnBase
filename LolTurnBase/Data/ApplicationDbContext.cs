using LolTurnBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LolTurnBase.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Champion> Champion {get; set;}
        public DbSet<Item> Items {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Champion>().HasData
                (
                new Champion { Id = 1, AbillityPower = 15, Armor = 10, AttackDamage = 30, Health = 300, Level = 1, MagicResist = 11, Mana = 100, Name = "Gragas" },
                new Champion { Id = 2, AbillityPower = 20, Armor = 10, AttackDamage = 30, Health = 200, Level = 1, MagicResist = 12, Mana = 200, Name = "Ryze" }
                );
            modelBuilder.Entity<Item>().HasData
                (
                 new Item {Id = 1, AbillityPowerGained = 20, ArmorGained = 15, AttackDamageGained = 30, Description="Provides Random Stuff",
                          HealthGained = 100, MagicResitGained = 100, ManaGained = 100, Name = "Allmighty", Cost = 999999 }
                );
        }
    }
}
