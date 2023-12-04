using Dapper;
using DapperApp.Models;
using Npgsql;

namespace DapperApp.Repository.Implement
{
    public interface IInvoiceRepository
    {
        Task CreateInvoice(InvoiceHeader model);
        Task<IEnumerable<InvoiceHeader>> GetAll();
        Task<InvoiceHeader> GetSingleInvoice(int id);
    }
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DapperContextClass context;
        public InvoiceRepository(DapperContextClass context)
        {
            this.context=context;
        }
        public async Task CreateInvoice(InvoiceHeader model)
        {
            using (var cnn = context.CeateConnection())
            {
                cnn.Open();
                using (var trans = cnn.BeginTransaction())
                {
                    try
                    {
                        string query = "INSERT INTO public.\"Invoices\"(\"CustomerId\", \"InvoiceCode\", \"InvoiceDate\", \"Remarks\", \"Status\", \"Total\") " +
                        "VALUES (@customerId, @invoiceCode, @invoiceDate, @remaks, @status, @total) returning \"InvoiceId\"; ";
                        var id = await cnn.ExecuteScalarAsync<int>(query, model, trans);
                        foreach (var detail in model.InvoiceDetailList)
                        {
                            detail.InvoiceId = id;
                            string detailQuery = "INSERT INTO public.\"InvoiceDetails\"(\"ProductId\", \"InvoiceId\", \"Qty\", \"UnitPrice\", \"VAT\", \"VatValue\", \"Discount\",\"DiscountValue\", \"Subtotal\")  " +
                                "VALUES (@productId, @invoiceId, @qty, @unitPrice, @vat, @vatValue, @discount,@discountValue, @subtotal);  ";
                            await cnn.ExecuteAsync(detailQuery, detail, trans);
                        }
                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                    }
                }
                cnn.Close();
            }
        }

        public Task<IEnumerable<InvoiceHeader>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<InvoiceHeader> GetSingleInvoice(int id)
        {
            throw new NotImplementedException();
        }
    }
}
