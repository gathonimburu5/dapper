using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DapperApp.Models
{
    public class Customers
    {
        public int CustomerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string County { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string KraPin { get; set; }
        [Required]
        public string PhysicalAddress { get; set; }
        [Required]
        public string CustomerCode { get; set; }
        public int CurrencyId { get; set; }
    }
}
