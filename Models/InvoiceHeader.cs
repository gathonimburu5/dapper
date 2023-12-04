using System.ComponentModel.DataAnnotations.Schema;

namespace DapperApp.Models
{
    public class InvoiceHeader
    {
        public int InvoiceHeaderId { get; set; }
        [ForeignKey("Customers")]
        public int CustomerId { get; set; }
        public string InvoiceCode { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Remaks { get; set; }
        public bool Status { get; set; }
        public decimal Total { get; set; }
        public virtual List<InvoiceDetail> InvoiceDetailList { get; set; } = new List<InvoiceDetail>();
    }
    public class InvoiceDetail
    {
        public int InvoiceDetailId { get; set; }
        [ForeignKey("Products")]
        public int ProductId { get; set; }
        [ForeignKey("InvoiceHeader")]
        public int InvoiceId { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public int Vat { get; set; }
        public decimal VatValue { get; set; }
        public int Discount { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal SubTotal { get; set; }
    }
}
