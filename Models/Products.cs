using System.ComponentModel.DataAnnotations.Schema;

namespace DapperApp.Models
{
    public class Products
    {
        public int ProductId { get; set; }
        public string ProName { get; set; }
        public string ProCode { get; set; }
        public string ProDescription { get; set; }
        public int Qty { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int ReorderLevel { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }
    }

    public class Units
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
    }

    public class Categories
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
