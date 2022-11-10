using Catalog.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.Repositories
{
    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new()  // new term
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 65, CreatedDate = DateTimeOffset.UtcNow  },
            new Item { Id = Guid.NewGuid(), Name = "Sword",  Price = 665, CreatedDate = DateTimeOffset.UtcNow  },
            new Item { Id = Guid.NewGuid(), Name = "Bow",  Price = 432, CreatedDate = DateTimeOffset.UtcNow  },
            new Item { Id = Guid.NewGuid(), Name = "Spear",  Price= 783, CreatedDate = DateTimeOffset.UtcNow  }
        };

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(items);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            //return items.Where(x => x.Id == id).First();
            var item =  items.Where(item => item.Id == id).SingleOrDefault()!;
            return await Task.FromResult(item);
        }

        public async Task CreateItemAsync(Item item)
        {
            items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}