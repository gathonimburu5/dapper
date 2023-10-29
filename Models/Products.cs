using System.ComponentModel.DataAnnotations.Schema;

namespace DapperApp.Models
{
    public class Products
    {
        public int ProductId { get; set; }
        public string ProName { get; set; }
        public string ProCode { get; set; }
        public string ProDescription { get; set; }
        public int Qtry { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public int ReorderLevel { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Categories? Category { get; set; }
        [ForeignKey("MeasureUnit")]
        public int UnitId { get; set; }
        public virtual Units? MeasureUnit { get; set; }
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
