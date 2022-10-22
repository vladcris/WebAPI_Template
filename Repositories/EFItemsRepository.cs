using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.EFCore;
using Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Repositories
{
    public class EFItemsRepository : IItemsRepository
    {
        private readonly ItemsContext _context;
        public EFItemsRepository(ItemsContext context)
        {
            _context = context;
        }
        public async Task CreateItemAsync(Item item)
        {
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var item = GetItemAsync(id);

            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            //var item =  await _context.Items.FindAsync(id);
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            return item!;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            var items = await _context.Items.ToListAsync();

            return items;
        }

        public async Task UpdateItemAsync(Item item)
        {
        //     _context.Update(item);
            var itemToUpdate = await GetItemAsync(item.Id);

            itemToUpdate = item with
            {
                 Name = item.Name,
                 Price = item.Price   
            };

            _context.ChangeTracker.Clear();

            _context.Update(itemToUpdate);
            //_context.Items.Update(itemToUpdate);

            _context.SaveChanges();
        }
    }
}