using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LuzmaShopAPI.Models;

namespace LuzmaShopAPI.Data
{
    public class LuzmaShopAPIContext : DbContext
    {
        public LuzmaShopAPIContext (DbContextOptions<LuzmaShopAPIContext> options)
            : base(options)
        {
        }

        public DbSet<LuzmaShopAPI.Models.Offer> Offer { get; set; } = default!;
        public DbSet<LuzmaShopAPI.Models.Product> Product { get; set; } = default!;
        public DbSet<LuzmaShopAPI.Models.ProductCategory> ProductCategory { get; set; } = default!;
        public DbSet<LuzmaShopAPI.Models.User> User { get; set; } = default!;
        public DbSet<LuzmaShopAPI.Models.Review> Review { get; set; } = default!;
        public DbSet<LuzmaShopAPI.Models.Cart> Cart { get; set; } = default!;
        public DbSet<LuzmaShopAPI.Models.CartItem> CartItem { get; set; } = default!;
        public DbSet<LuzmaShopAPI.Models.UserCart> UserCart { get; set; } = default!;
        public DbSet<LuzmaShopAPI.Models.UserCartItem> UserCartItem { get; set; } = default!;
    }
}
