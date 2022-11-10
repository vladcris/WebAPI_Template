using System;
using System.Collections.Generic;
using System.Data;
using Catalog.Api.Dapper;
using Catalog.Api.Entities;
using Dapper;

namespace Catalog.Api.Repositories
{
    public class SqlServerDapperRepository : IItemsRepository
    {
        private readonly DapperContext _context;
        public SqlServerDapperRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateItemAsync(Item item)
        {
            string query = $"INSERT INTO Items values (@Id, @Name, @Price, @CreatedDate)";

            await SaveDataAsync(query, item);          
        }

        public async Task DeleteItemAsync(Guid id)
        {
           string query = $"DELETE FROM Items WHERE Id = @Id ";
           string newId = id.ToString();

           await SaveDataAsync(query, new { Id = newId });  
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var query = $"SELECT * FROM Items where Id = @Id";

            var result = await LoadObjectAsync<Item, dynamic>(query, new{ id });

            return result;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            string query = $"SELECT * FROM Items";
            
            var result = await LoadListAsync<Item>(query);
            
            return result;
        }

        public async Task UpdateItemAsync(Item item)
        {
            string query = $"UPDATE Items SET Name = @Name, Price = @Price where Id = @Id";

            string newId = item.Id.ToString();

            await SaveDataAsync(query, new { Id = newId, item.Name, item.Price });
        }


        private async Task<List<T>> LoadListAsync<T>(string query, object[]? parameters = null )
        {
            using( var conn = _context.CreateConnection() )
            {
                var result = (await conn.QueryAsync<T>(query, parameters)).AsList();
                return result;
            }
        }
        private async Task<T> LoadObjectAsync<T,U>(string query, U parameters)
        {
            using( var conn = _context.CreateConnection() )
            {
                var result = await conn.QuerySingleOrDefaultAsync<T>(query, parameters);
                return result;
            }
        }

        private async Task SaveDataAsync<T>(string query, T parameters)
        {
            using( var conn = _context.CreateConnection() )
            {
                var result = await conn.ExecuteAsync(query, parameters);
            }
        }
    }
}


