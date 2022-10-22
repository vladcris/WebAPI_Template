using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Catalog.Dapper
{
    public class DapperContext
    {
        private readonly IConfiguration _config;
        private readonly string _connString;
        public DapperContext(IConfiguration config)
        {
            _config = config;
            //_connString = _config.GetConnectionString("SqlConnection");            
            _connString = _config.GetConnectionString("DefaultConnection");            
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connString);
        }
    }
}