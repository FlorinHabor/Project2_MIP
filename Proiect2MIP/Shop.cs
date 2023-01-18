using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Proiect2MIP
{
    internal class Shop
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
        public DateTime ReciveTime { get; set; }
        public DateTime ExpiryDate { get; set; }
        [Required]
        public int quantity { get; set; }
    }
    class ShopDbContext : DbContext
    {
        public DbSet<Shop> shops { get; set; }
    }
    internal class User
    {
        public int id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
    class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
    internal class Purchase
    {
        public int id { get; set; }
        public string user { get; set; }
        public string item { get; set; }
        public int quantity { get; set; }
    }
    class PurchaseDbContext : DbContext
    {
        public DbSet<Purchase> purchases { get; set; }
    }

    internal class LoginState
    {
        public static string username { get; set; }

    }
}
