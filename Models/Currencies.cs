namespace DapperApp.Models
{
    public class Currencies
    {
        public int CurrencyId { get; set; }
        public string CurrName { get; set; }
        public string Abbre { get; set; }
        public string Country { get; set; }
        public decimal Rate { get; set; }
    }
}
