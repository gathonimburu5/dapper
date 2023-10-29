using Dapper;
using DapperApp.Models;
using DapperApp.Repository.Interface;

namespace DapperApp.Repository.Implement
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DapperContextClass context;
        public CustomerRepository(DapperContextClass context)
        {
            this.context = context;
        }

        public async Task AddCustomer(Customers customer)
        {
            string query = "INSERT INTO \"Customerz\"(\"FirstName\", \"LastName\", \"EmailAddress\", \"PhoneNumber\", \"County\", \"Address\", \"KraPin\", \"PhysicalAddress\", \"CustomerCode\", \"CurrencyId\") " +
                "VALUES (@firstName, @lastName, @emailAddress, @phoneNumber, @county, @address, @kraPin, @physicalAddress, @customerCode, @currencyId); ";
            using (var conn = context.CeateConnection())
            {
                await conn.ExecuteAsync(query, customer);
            }
        }

        public async Task<IEnumerable<Customers>> GetAll()
        {
            string query = "SELECT * FROM \"Customerz\" ORDER BY \"CustomerId\" ASC  ";
            using (var connection = context.CeateConnection())
            {
                var customer = await connection.QueryAsync<Customers>(query);
                return customer.ToList();
            }
        }

        public async Task<Customers> GetCustomer(int id)
        {
            string query = "Select * from \"Customerz\" where \"CustomerId\" = @id  ";
            using (var conn = context.CeateConnection())
            {
                var customer = await conn.QueryFirstOrDefaultAsync<Customers>(query, new { id });
                return customer;
            }
        }

        public async Task UpdateCustomer(Customers customer)
        {
            string query = "UPDATE \"Customerz\" SET \"FirstName\"='"+customer.FirstName+"', \"LastName\"='"+customer.LastName+"', \"EmailAddress\"='"+customer.EmailAddress+"',   " +
                " \"PhoneNumber\"='"+customer.PhoneNumber+"', \"County\"='"+customer.County+"', \"Address\"='"+customer.Address+"', \"KraPin\"='"+customer.KraPin+"',      " +
                " \"PhysicalAddress\"='"+customer.PhysicalAddress+"', \"CustomerCode\"= '"+customer.CustomerCode+"', \"CurrencyId\"= '"+customer.CurrencyId+"'     " +
                "WHERE \"CustomerId\" = '"+customer.CustomerId+"'; ";
            using (var cnn = context.CeateConnection())
            {
                await cnn.ExecuteAsync(query, customer);
            }
        }
    }
}
