using Catalog.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.EFCore
{
    public class ItemsContext : DbContext
    {
        public ItemsContext(DbContextOptions<ItemsContext> opt) : base(opt)
        {   

        }
        public DbSet<Item> Items {get; set;}
    }
}