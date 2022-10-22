using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]  // api controller
    [Route("[controller]")] // route
    public class ItemsController : ControllerBase  // controller base
    {
        private readonly IItemsRepository _repository;
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(IItemsRepository repository, ILogger<ItemsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemsAsync()
        {
            var items = (await _repository.GetItemsAsync())
                        .Select(item => item.AsItemDto());
            _logger.LogInformation("{0}: Retrived {1} items", DateTime.UtcNow.ToString("hh:mm:ss"), items.Count());
            return Ok(items);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await _repository.GetItemAsync(id);
            if ( item is null )
            {
                return NotFound();
            }
            return Ok(item.AsItemDto());
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            var item = new Item{
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await _repository.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItemAsync), new {id = item.Id}, item.AsItemDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
        {
            var item = await _repository.GetItemAsync(id);
            if(item is null)
            {
                return NotFound();
            }
            // var updatedItem = new Item{
            //     Id = item.Id,
            //     Name = itemDto.Name,
            //     Price = itemDto.Price,
            //     CreatedDate = item.CreatedDate
            // };

            ///WITH expresion for record types, updateedItem is a copy of item with the specified changes, still immutable
            var updatedItem = item with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };
            await _repository.UpdateItemAsync(updatedItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var item = _repository.GetItemAsync(id);
            if(item is null)
            {
                return NotFound();
            }
            await _repository.DeleteItemAsync(id);
            return NoContent();
        }
    }
}