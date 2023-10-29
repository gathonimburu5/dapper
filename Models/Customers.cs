using System.ComponentModel.DataAnnotations.Schema;

namespace DapperApp.Models
{
    public class Customers
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string County { get; set; }
        public string Address { get; set; }
        public string KraPin { get; set; }
        public string PhysicalAddress { get; set; }
        public string CustomerCode { get; set; }
        [ForeignKey("Currency")]
        public int CurrencyId { get; set; }
        public virtual Currencies? Currency { get; set; }
    }
}
