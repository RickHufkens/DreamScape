using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace DreamScape.Data
{
    internal class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserItem> UserItems { get; set; }
        public DbSet<Item> Items { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString,
                ServerVersion.Parse("5.7.33")
            );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the relationship between Trade and Sender (one-to-many)
            modelBuilder.Entity<Trade>()
                .HasOne(t => t.Sender)            // A Trade has one Sender (User)
                .WithMany(u => u.SentTrades)      // A User can have many Sent Trades
                .HasForeignKey(t => t.SenderID)   // Foreign key in Trade that points to User
                .OnDelete(DeleteBehavior.Restrict);  // Optional: specify delete behavior

            // Configuring the relationship between Trade and Receiver (one-to-many)
            modelBuilder.Entity<Trade>()
                .HasOne(t => t.Receiver)          // A Trade has one Receiver (User)
                .WithMany(u => u.ReceivedTrades)  // A User can have many Received Trades
                .HasForeignKey(t => t.ReceiverID) // Foreign key in Trade that points to User
                .OnDelete(DeleteBehavior.Restrict);  // Optional: specify delete behavior

            // Configuring the relationship between User and Role (one-to-many or many-to-many, depending on your setup)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)         // A User has one Role
                .WithMany(r => r.Users)      // A Role can have many Users
                .HasForeignKey(u => u.RoleId) // Foreign key in User that points to Role
                .OnDelete(DeleteBehavior.Restrict); // Optional: specify delete behavior

            base.OnModelCreating(modelBuilder);

            // Seeding Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Administrator" },
                new Role { Id = 2, Name = "User" }
            );

            // Seeding Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Admin", Password = HashPassword("password"), RoleId = 1, Email = "admin@example.com", CreatedAt = DateTime.Now },
                new User { Id = 2, Name = "Player1", Password = HashPassword("playerpass"), RoleId = 2, Email = "player1@example.com", CreatedAt = DateTime.Now },
                new User { Id = 3, Name = "Player2", Password = HashPassword("playerpass"), RoleId = 2, Email = "player2@example.com", CreatedAt = DateTime.Now },
                new User { Id = 4, Name = "Player3", Password = HashPassword("playerpass"), RoleId = 2, Email = "player3@example.com", CreatedAt = DateTime.Now },
                new User { Id = 5, Name = "Player4", Password = HashPassword("playerpass"), RoleId = 2, Email = "player4@example.com", CreatedAt = DateTime.Now }
            );

            // Seeding Types
            modelBuilder.Entity<Type>().HasData(
                new Type { Id = 1, Name = "Weapon" },
                new Type { Id = 2, Name = "Consumable" },
                new Type { Id = 3, Name = "Armor" },
                new Type { Id = 4, Name = "Material" }
            );

            // Seeding Rarities with Color
            modelBuilder.Entity<Rarity>().HasData(
                new Rarity { Id = 1, Name = "Common", Color = "White" },
                new Rarity { Id = 2, Name = "Rare", Color = "Blue" },
                new Rarity { Id = 3, Name = "Epic", Color = "Purple" },
                new Rarity { Id = 4, Name = "Legendary", Color = "Gold" }
            );

            // Seeding Items with corresponding RarityId and TypeId
            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Name = "Sword of Destiny", Description = "A legendary sword.", Tradeable = true, RarityId = 4, TypeId = 1 },
                new Item { Id = 2, Name = "Healing Potion", Description = "Restores 50 HP.", Tradeable = true, RarityId = 1, TypeId = 2 },
                new Item { Id = 3, Name = "Steel Helmet", Description = "Protects the wearer.", Tradeable = false, RarityId = 2, TypeId = 3 },
                new Item { Id = 4, Name = "Magic Crystal", Description = "Used in crafting.", Tradeable = true, RarityId = 3, TypeId = 4 },
                new Item { Id = 5, Name = "Bow of the Wind", Description = "A bow with great precision.", Tradeable = true, RarityId = 4, TypeId = 1 },
                new Item { Id = 6, Name = "Mana Potion", Description = "Restores 30 MP.", Tradeable = true, RarityId = 1, TypeId = 2 },
                new Item { Id = 7, Name = "Iron Shield", Description = "A sturdy shield.", Tradeable = false, RarityId = 2, TypeId = 3 },
                new Item { Id = 8, Name = "Dragon's Scale", Description = "A rare material.", Tradeable = true, RarityId = 3, TypeId = 4 }
            );

            // Seeding UserItems
            modelBuilder.Entity<UserItem>().HasData(
                new UserItem { Id = 1, UserId = 1, ItemId = 1, Quantity = 2 }, // Admin with Sword of Destiny
                new UserItem { Id = 2, UserId = 1, ItemId = 2, Quantity = 10 }, // Admin with Healing Potions
                new UserItem { Id = 3, UserId = 1, ItemId = 5, Quantity = 1 },  // Admin with Bow of the Wind

                new UserItem { Id = 4, UserId = 2, ItemId = 3, Quantity = 3 },  // Player1 with Steel Helmet
                new UserItem { Id = 5, UserId = 2, ItemId = 6, Quantity = 8 },  // Player1 with Mana Potions
                new UserItem { Id = 6, UserId = 2, ItemId = 7, Quantity = 5 },  // Player1 with Iron Shield

                new UserItem { Id = 7, UserId = 3, ItemId = 4, Quantity = 4 },  // Player2 with Magic Crystal
                new UserItem { Id = 8, UserId = 3, ItemId = 6, Quantity = 12 }, // Player2 with Mana Potions
                new UserItem { Id = 9, UserId = 3, ItemId = 5, Quantity = 2 },  // Player2 with Bow of the Wind

                new UserItem { Id = 10, UserId = 4, ItemId = 1, Quantity = 6 }, // Player3 with Sword of Destiny
                new UserItem { Id = 11, UserId = 4, ItemId = 2, Quantity = 15 }, // Player3 with Healing Potions
                new UserItem { Id = 12, UserId = 4, ItemId = 8, Quantity = 3 }, // Player3 with Dragon's Scale

                new UserItem { Id = 13, UserId = 5, ItemId = 3, Quantity = 7 }, // Player4 with Steel Helmet
                new UserItem { Id = 14, UserId = 5, ItemId = 4, Quantity = 9 }, // Player4 with Magic Crystal
                new UserItem { Id = 15, UserId = 5, ItemId = 7, Quantity = 10 }  // Player4 with Iron Shield
            );

            // Seeding Trades
            modelBuilder.Entity<Trade>().HasData(
                new Trade { Id = 1, SenderID = 1, ReceiverID = 2, Status = "Pending", CreatedAt = DateTime.Now },
                new Trade { Id = 2, SenderID = 2, ReceiverID = 3, Status = "Completed", CreatedAt = DateTime.Now }
            );

            // Seeding ItemTrades
            modelBuilder.Entity<ItemTrade>().HasData(
                new ItemTrade { Id = 1, ItemId = 1, TradeId = 1, Quantity = 1, IsSenderItem = true },
                new ItemTrade { Id = 2, ItemId = 2, TradeId = 1, Quantity = 2, IsSenderItem = false },
                new ItemTrade { Id = 3, ItemId = 3, TradeId = 2, Quantity = 1, IsSenderItem = true }
            );
        }


        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }

}
