using Dapper;
using DapperApp.Models;

namespace DapperApp.Repository.Implement
{
    public interface ICurrencyRepository
    {
        Task CreateCurrency(Currencies model);
        Task UpdateCurrency(Currencies model);
        Task DeleteCurrency(int id);
        Task<IEnumerable<Currencies>> GetAll();
        Task<Currencies> GetCurencyById(int id);
    }


    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly DapperContextClass context;
        public CurrencyRepository(DapperContextClass context)
        {
            this.context=context;
        }
        public async Task CreateCurrency(Currencies model)
        {
            string query = "INSERT INTO \"Currencies\"(\"CurrName\", \"Abbre\", \"Country\", \"Rate\")VALUES (@CurrName, @Abbre, @Country, @Rate); ";
            using (var conn = context.CeateConnection())
            {
                await conn.ExecuteAsync(query, model);
            }
        }

        public async Task DeleteCurrency(int id)
        {
            using (var cnn = context.CeateConnection())
            {
                string query = "delete from \"Currencies\" where \"Currencies\".\"CurrencyId\" =  @id ";
                await cnn.ExecuteAsync(query, new { id });
            }
        }

        public async Task<IEnumerable<Currencies>> GetAll()
        {
            string query = "SELECT * FROM \"Currencies\" ORDER BY \"CurrencyId\" ASC  ";
            using (var cnn = context.CeateConnection())
            {
                var curr = await cnn.QueryAsync<Currencies>(query);
                return curr.ToList();
            }
        }

        public async Task<Currencies> GetCurencyById(int id)
        {
            string query = "SELECT * FROM public.\"Currencies\" where \"CurrencyId\" = @id ";
            using (var cnn = context.CeateConnection())
            {
                var currency = await cnn.QueryFirstAsync<Currencies>(query, new { id });
                return currency;
            }
        }

        public async Task UpdateCurrency(Currencies model)
        {
            string query = "UPDATE public.\"Currencies\" SET \"CurrName\"='"+model.CurrName+"', \"Abbre\"='"+model.Abbre+"', \"Country\"='"+model.Country+"', \"Rate\"='"+model.Rate+"'    " +
                "WHERE \"CurrencyId\"= '"+model.CurrencyId+"'; ";
            using (var cnn = context.CeateConnection())
            {
                await cnn.ExecuteAsync(query, model);
            }
        }
    }
}
