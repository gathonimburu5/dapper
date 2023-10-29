using Npgsql;
using System.Data;

namespace DapperApp.Models
{
    public class DapperContextClass
    {
        private readonly IConfiguration configuration;
        private readonly string ConnectionString; 
        public DapperContextClass(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = configuration.GetConnectionString("Connection");
        }
        public IDbConnection CeateConnection() => new NpgsqlConnection(ConnectionString);
    }
}
