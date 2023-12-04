using DapperApp.Models;
using DapperApp.Repository.Implement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository currencyRepository;
        private readonly ILogger<CurrencyController> logger;
        public CurrencyController(ICurrencyRepository currencyRepository, ILogger<CurrencyController> logger)
        {
            this.currencyRepository=currencyRepository;
            this.logger=logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCurrency()
        {
            logger.LogInformation("Class:CurrencyController | Method:GetAllCurrency | Start method | Params {0}", "test");
            var currency = await currencyRepository.GetAll();
            logger.LogInformation("Class:CurrencyController | Method:GetAllCurrency | Start method | Params {0}", currency.ToString());
            return Ok(currency);
        }
        [HttpGet]
        public async Task<IActionResult> GetCurrency(int id)
        {
            logger.LogInformation("Class:CurrencyController | Method:GetCurrency | Start method | Params {0}", "test");
            var result = await currencyRepository.GetCurencyById(id);
            logger.LogInformation("Class:CurrencyController | Method:GetCurrency | Start method | Params {0}", result.ToString());
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateCurrency([FromBody]Currencies model)
        {
            logger.LogInformation("Class:CurrencyController | Method:CreateCurrency | Start method | Params {0}", model.ToString());
            Status status = new Status();
            if (!ModelState.IsValid)
            {
                status.Code = 400;
                status.Message = "All Fields are Required!, Please chack all Values!!";
                logger.LogInformation("Class:CurrencyController | Method:CreateCurrency | Start method | Params {0}", status.Message);
                return Ok(status);
            }
            var results = currencyRepository.CreateCurrency(model);
            if (results != null)
            {
                status.Code = 200;
                status.Message = "Successfully Created Currency";
                logger.LogInformation("Class:CurrencyController | Method:CreateCurrency | Start method | Params {0}", status.Message);
                return Ok(status);
            }
            else
            {
                status.Code = 400;
                status.Message = "Failed to Create Currency!!";
                logger.LogInformation("Class:CurrencyController | Method:CreateCurrency | Start method | Params {0}", status.Message);
                return Ok(status);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCurrency([FromBody]Currencies model, int id)
        {
            logger.LogInformation("Class:CurrencyController | Method:UpdateCurrency | Start method | Params {0}", model.ToString());
            Status status = new Status();
            if (!ModelState.IsValid)
            {
                status.Code = 400;
                status.Message = "All Fields Are Required!, Please Fill All the Fields!!";
                logger.LogInformation("Class:CurrencyController | Method:UpdateCurrency | Start method | Params {0}", status.Message);
                return Ok(status);
            }
            var detail = await currencyRepository.GetCurencyById(id);
            if(detail != null)
            {
                var results = currencyRepository.UpdateCurrency(model);
                if(results != null)
                {
                    status.Code = 200;
                    status.Message = "Successfully Updated Currency.";
                    logger.LogInformation("Class:CurrencyController | Method:UpdateCurrency | Start method | Params {0}", status.Message);
                    return Ok(status);
                }
                else
                {
                    status.Code = 400;
                    status.Message = "Failed to Update Currency!!";
                    logger.LogInformation("Class:CurrencyController | Method:UpdateCurrency | Start method | Params {0}", status.Message);
                    return Ok(status);
                }
            }
            return Ok(status);
        }
        [HttpDelete]
        public IActionResult DeleteCurrency(int id)
        {
            Status status = new Status();
            var currency = currencyRepository.DeleteCurrency(id);
            if (currency != null)
            {
                status.Code = 200;
                status.Message = "Successfully Deleted Currency";
                logger.LogInformation("Class:CurrencyController | Method:DeleteCurrency | Start method | Params {0}", status.Message);
                return Ok(status);
            }
            else
            {
                status.Code = 400;
                status.Message = "Failed to Delete Currency!!!";
                logger.LogInformation("Class:CurrencyController | Method:DeleteCurrency | Start method | Params {0}", status.Message);
                return Ok(status);
            }
        }
    }
}
