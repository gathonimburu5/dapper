using DapperApp.Models;
using DapperApp.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ILogger<CustomerController> logger;
        public CustomerController(ICustomerRepository customerRepository, ILogger<CustomerController> logger)
        {
            this.customerRepository=customerRepository;
            this.logger=logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            logger.LogInformation("Class:CustomerController | Method:Get | Start method | Params {0}", "test");
            var customer = await customerRepository.GetAll();
            logger.LogInformation("Class:CustomerController | Method:Get | Start method | Params {0}", customer.ToString() );
            return Ok(customer);
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomer(int id)
        {
            logger.LogInformation("Class:CustomerController | Method:GetCustomer | Stat Method | Paams {0}", "test");
            var customer = await customerRepository.GetCustomer(id);
            logger.LogInformation("Class:CustomerController | Method:GetCustomer | Stat Method | Paams {0}", customer.ToString() );
            return Ok(customer);
        }
        [HttpPost]
        public IActionResult CreateCustomers([FromBody]Customers model)
        {
            logger.LogInformation("Class:CustomerController | Method:CreateCustomers | Stat Method | Paams {0}", model.ToString());
            Status status = new Status();
            if (!ModelState.IsValid)
            {
                status.Code = 0;
                status.Message = "All Fields are Required!!";
                logger.LogInformation("Class:CustomerController | Method:CreateCustomers | Stat Method | Paams {0}", status.Message);
                return Ok(status);
            }
            var customer = customerRepository.AddCustomer(model);
            if(customer != null)
            {
                status.Code = 1;
                status.Message = "Successfully Created Customers.";
                logger.LogInformation("Class:CustomerController | Method:CreateCustomers | Stat Method | Paams {0}", status.Message);
                return Ok(status);
            }
            else
            {
                status.Code = 0;
                status.Message = "Failed to Create Customers!!!";
                logger.LogInformation("Class:CustomerController | Method:CreateCustomers | Stat Method | Paams {0}", status.Message);
                return Ok(status);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCustomers([FromBody] Customers model, int id)
        {
            logger.LogInformation("Class:CustomerController | Method:UpdateCustomers | Stat Method | Paams {0}", model.ToString());
            Status status = new Status();
            if (!ModelState.IsValid)
            {
                status.Code = 400;
                status.Message = "All Fields are required";
                logger.LogInformation("Class:CustomerController | Method:UpdateCustomers | Stat Method | Paams {0}", status.Message);
                return Ok(status);
            }
            var details = await customerRepository.GetCustomer(id);
            if(details != null)
            {
                var res = customerRepository.UpdateCustomer(model);
                if (res != null)
                {
                    status.Code = 200;
                    status.Message = "Customers Successfully Updated";
                    logger.LogInformation("Class:CustomerController | Method:UpdateCustomers | Stat Method | Paams {0}", status.Message);
                    return Ok(status);
                }
                else
                {
                    status.Code = 400;
                    status.Message = "Failed to Update Customers!!!";
                    logger.LogInformation("Class:CustomerController | Method:UpdateCustomers | Stat Method | Paams {0}", status.Message);
                    return Ok(status);
                }
            }
            return Ok(status);
        }
    }
}
