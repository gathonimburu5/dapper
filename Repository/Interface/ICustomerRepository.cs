using DapperApp.Models;

namespace DapperApp.Repository.Interface
{
    public interface ICustomerRepository
    {
        Task AddCustomer(Customers customer);
        Task UpdateCustomer(Customers customer);
        Task<Customers> GetCustomer(int id);
        Task<IEnumerable<Customers>> GetAll();
    }
}
