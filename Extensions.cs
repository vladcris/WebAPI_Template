using System;
using System.Collections.Generic;
using Catalog.Dtos;
using Catalog.Entities;

namespace Catalog
{
    public static class Extensions
    {
        public static ItemDto AsItemDto(this Item item)
        {
            var itemDto = new ItemDto {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };

            return itemDto;
        }

        public static List<Item> AsItemList(this List<ItemSql> itemsSql)
        {
            List<Item> result = new();

            foreach (var item in itemsSql)
            {
                result.Add(new Item
                {
                    Id = Guid.Parse(item.Id),
                    Name = item.Name,
                    Price = item.Price,
                    CreatedDate = item.CreatedDate
                });
            }
            return result;
        }

        public static Item AsItem(this ItemSql item)
        {
            return new Item
            {
                Id = Guid.Parse(item.Id),
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }
    }
}