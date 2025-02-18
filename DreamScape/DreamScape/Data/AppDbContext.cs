using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace DreamScape.Data
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString,
                ServerVersion.Parse("5.7.33")
            );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Administrator" },
                new Role { Id = 2, Name = "User" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Admin", Password = "password", RoleId = 1, Email = "admin@example.com", CreatedAt = DateTime.Now },
                new User { Id = 2, Name = "Player1", Password = "playerpass", RoleId = 2, Email = "player1@example.com", CreatedAt = DateTime.Now },
                new User { Id = 3, Name = "Player2", Password = "playerpass", RoleId = 2, Email = "player2@example.com", CreatedAt = DateTime.Now }
            );

            modelBuilder.Entity<Type>().HasData(
                new Type { Id = 1, Name = "Weapon" },
                new Type { Id = 2, Name = "Consumable" },
                new Type { Id = 3, Name = "Armor" },
                new Type { Id = 4, Name = "Material" }
            );

            modelBuilder.Entity<Rarity>().HasData(
                new Rarity { Id = 1, Name = "Common" },
                new Rarity { Id = 2, Name = "Rare" },
                new Rarity { Id = 3, Name = "Epic" },
                new Rarity { Id = 4, Name = "Legendary" }
            );

            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Name = "Sword of Destiny", Description = "A legendary sword.", Tradeable = true, RarityId = 4, TypeId = 1 },
                new Item { Id = 2, Name = "Healing Potion", Description = "Restores 50 HP.", Tradeable = true, RarityId = 1, TypeId = 2 },
                new Item { Id = 3, Name = "Steel Helmet", Description = "Protects the wearer.", Tradeable = false, RarityId = 2, TypeId = 3 },
                new Item { Id = 4, Name = "Magic Crystal", Description = "Used in crafting.", Tradeable = true, RarityId = 3, TypeId = 4 }
            );

            modelBuilder.Entity<UserItem>().HasData(
                new UserItem { Id = 1, UserId = 1, ItemId = 1, Quantity = 1 },
                new UserItem { Id = 2, UserId = 1, ItemId = 2, Quantity = 5 },
                new UserItem { Id = 3, UserId = 2, ItemId = 2, Quantity = 2 },
                new UserItem { Id = 4, UserId = 2, ItemId = 3, Quantity = 1 },
                new UserItem { Id = 5, UserId = 3, ItemId = 4, Quantity = 3 }
            );

            modelBuilder.Entity<Trade>().HasData(
                new Trade { Id = 1, SenderID = 1, ReceiverID = 2, Status = "Pending", CreatedAt = DateTime.Now },
                new Trade { Id = 2, SenderID = 2, ReceiverID = 3, Status = "Completed", CreatedAt = DateTime.Now }
            );

            modelBuilder.Entity<ItemTrade>().HasData(
                new ItemTrade { Id = 1, ItemId = 1, TradeId = 1, Quantity = 1, IsSenderItem = true },
                new ItemTrade { Id = 2, ItemId = 2, TradeId = 1, Quantity = 2, IsSenderItem = false },
                new ItemTrade { Id = 3, ItemId = 3, TradeId = 2, Quantity = 1, IsSenderItem = true }
            );
        }


    }

}
