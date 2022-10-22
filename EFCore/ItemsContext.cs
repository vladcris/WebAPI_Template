using Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.EFCore
{
    public class ItemsContext : DbContext
    {
        public ItemsContext(DbContextOptions<ItemsContext> opt) : base(opt)
        {   

        }
        public DbSet<Item> Items {get; set;}
    }
}