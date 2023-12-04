using DapperApp.Models;
using DapperApp.Repository.Implement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DapperApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly ILogger<InvoiceController> logger;
        public InvoiceController(IInvoiceRepository invoiceRepository, ILogger<InvoiceController> logger)
        {
            this.invoiceRepository=invoiceRepository;
            this.logger=logger;
        }
        [HttpPost]
        public IActionResult CreateInvoice([FromBody]InvoiceHeader model)
        {
            logger.LogInformation("Class : InvoiceController | Method : CreateInvoice | Start method | Params {0}", model.ToString());
            Status status = new Status();
            if(!ModelState.IsValid)
            {
                status.Code = 400;
                status.Message = "All Fields are Required!!";
                logger.LogInformation("Class : InvoiceController | Method : CreateInvoice | Start method | Params {0}", status.Message);
                return Ok(status);
            }
            var result = invoiceRepository.CreateInvoice(model);
            if(result != null)
            {
                status.Code = 200;
                status.Message = "Successfully Created Invoices.";
                logger.LogInformation("Class : InvoiceController | Method : CreateInvoice | Start method | Params {0}", status.Message);
                return Ok(status);
            }
            else
            {
                status.Code = 400;
                status.Message = "Failed to Create Invoice";
                logger.LogInformation("Class : InvoiceController | Method : CreateInvoice | Start method | Params {0}", status.Message);
                return Ok(status);
            }
        }
    }
}
