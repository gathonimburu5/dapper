using DapperApp.Models;
using DapperApp.Repository.Implement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace DapperApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly ILogger<ProductController> logger;
        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            this.productRepository=productRepository;
            this.logger=logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            logger.LogInformation("Class:ProductController | Method:GetAllProducts | Start method | Params {0}", "test");
            var products = await productRepository.GetAllProducts();
            logger.LogInformation("Class:ProductController | Method:GetAllProducts | Start method | Params {0}", products.ToString());
            return Ok(products);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductById(int id)
        {
            logger.LogInformation("Class:ProductController | Method:GetProductById | Start method | Params {0}", "test");
            var product = await productRepository.GetProductsById(id);
            logger.LogInformation("Class:ProductController | Method:GetProductById | Start method | Params {0}", product.ToString());
            return Ok(product);
        }
        [HttpPost]
        public IActionResult CreateProducts([FromBody]Products product)
        {
            logger.LogInformation("Class:ProductController | Method:CreateProducts | Start method | Params {0}", product.ToString());
            Status status = new Status();
            if (!ModelState.IsValid)
            {
                status.Code = 400;
                status.Message = "All Fields are Required, Please Check on all Fields!!";
                logger.LogInformation("Class:ProductController | Method:CreateProducts | Start method | Params {0}", status.Message);
                return Ok(status);
            }
            var result = productRepository.CreateProduct(product);
            if(result != null)
            {
                status.Code = 200;
                status.Message = "Successfully Created Product.";
                logger.LogInformation("Class:ProductController | Method:CreateProducts | Start method | Params {0}", status.Message);
                return Ok(status);
            }
            else
            {
                status.Code = 400;
                status.Message = "Failed to Create Product, Please Check Out!!";
                logger.LogInformation("Class:ProductController | Method:CreateProducts | Start method | Params {0}", status.Message);
                return Ok(status);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody]Products model, int id)
        {
            logger.LogInformation("Class:ProductController | Method:UpdateProduct | Start method | Params {0}", "test");
            Status status = new Status();
            if (!ModelState.IsValid)
            {
                status.Code = 400;
                status.Message = "All Fields are Required, Please Fill all Fields!!";
                logger.LogInformation("Class:ProductController | Method:UpdateProduct | Start method | Params {0}", status.Message);
                return Ok(status);
            }
            var detail = await productRepository.GetProductsById(id);
            if(detail != null)
            {
                var result = productRepository.UpdatePoduct(model);
                if(result != null)
                {
                    status.Code = 200;
                    status.Message = "Successfully Updated Products.";
                    logger.LogInformation("Class:ProductController | Method:UpdateProduct | Start method | Params {0}", status.Message);
                    return Ok(status);
                }
                else
                {
                    status.Code = 400;
                    status.Message = "Failed to Update Products!!";
                    logger.LogInformation("Class:ProductController | Method:UpdateProduct | Start method | Params {0}", status.Message);
                    return Ok(status);
                }
            }
            return Ok(status);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            logger.LogInformation("Class:ProductController | Method:GetAllCategorys | Start method | Params {0}", "test");
            var category = await productRepository.GetAllCategories();
            logger.LogInformation("Class:ProductController | Method:GetAllProducts | Start method | Params {0}", category.ToString());
            return Ok(category);
        }
        [HttpGet]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await productRepository.GetCategoriesById(id);
            logger.LogInformation("Class:ProductController | Method:GetCategoryById | Start method | Params {0}", category.ToString());
            return Ok(category);
        }
        [HttpPost]
        public IActionResult CreateCategory([FromBody] Categories category)
        {
            logger.LogInformation("Class:ProductController | Method:CreateCategory | Start method | Params {0}", category.ToString());
            Status status = new Status();
            if(!ModelState.IsValid)
            {
                status.Code = 400;
                status.Message = "All Fields are required!!";
                logger.LogInformation("Class:ProductController | Method:CreateCategory | Start method | Params {0}", status.Message);
                return Ok(status);
            }
            var result = productRepository.CreateCategory(category);
            if(result != null)
            {
                status.Code = 200;
                status.Message = "Successfully Created Category.";
                logger.LogInformation("Class:ProductController | Method:CreateCategory | Start method | Params {0}", status.Message);
                return Ok(status);
            }
            else
            {
                status.Code = 400;
                status.Message = "Failed to Create Category";
                logger.LogInformation("Class:ProductController | Method:CreateCategory | Start method | Params {0}", status.Message);
                return Ok(status);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] Categories model, int id)
        {
            logger.LogInformation("Class:ProductController | Method:UpdateCategory | Start method | Params {0}", model.ToString());
            Status status = new Status();
            if (!ModelState.IsValid)
            {
                status.Code = 400;
                status.Message = "All Fields Are Required!!";
                logger.LogInformation("Class:ProductController | Method:UpdateCategory | Start method | Params {0}", status.Message);
                return Ok(status);
            }
            var getDetails = await productRepository.GetCategoriesById(id);
            if(getDetails != null)
            {
                var result = productRepository.UpdateCategory(model);
                if(result != null)
                {
                    status.Code = 200;
                    status.Message = "Successfully Updated Category.";
                    logger.LogInformation("Class:ProductController | Method:UpdateCategory | Start method | Params {0}", status.Message);
                    return Ok(status);
                }
                else
                {
                    status.Code = 400;
                    status.Message = "Failled to Update Category!!";
                    logger.LogInformation("Class:ProductController | Method:UpdateCategory | Start method | Params {0}", status.Message);
                    return Ok(status);
                }
            }
            return Ok(status);
        }
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            Status status = new Status();
            var category = productRepository.DeleteCategory(id);
            if (category != null)
            {
                status.Code = 200;
                status.Message = "Successfully Deleted Category.";
                logger.LogInformation("Class:ProductController | Method:DeleteCategory | Start method | Params {0}", status.Message);
                return Ok(status);
            }
            else
            {
                status.Code = 400;
                status.Message = "Failed to Delete Category";
                logger.LogInformation("Class:ProductController | Method:DeleteCategory | Start method | Params {0}", status.Message);
                return Ok(status);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUnits()
        {
            var units = await productRepository.GetAllUnits();
            logger.LogInformation("Class:ProductController | Method:GetAllUnits | Start method | Params {0}", units.ToString());
            return Ok(units);
        }
        [HttpGet]
        public async Task<IActionResult> GetUnitById(int id)
        {
            var units = await productRepository.GetUnitsById(id);
            logger.LogInformation("Class:ProductController | Method:GetUnitById | Start method | Params {0}", units.ToString());
            return Ok(units);
        }
        [HttpPost]
        public IActionResult CreateUnits([FromBody] Units model)
        {
            logger.LogInformation("Class:ProductController | Method:CreateUnits | Start method | Params {0}", model.ToString());
            Status status = new Status();
            if (!ModelState.IsValid)
            {
                status.Code = 400;
                status.Message = "All Fields are Requied!!";
                logger.LogInformation("Class:ProductController | Method:CreateUnits | Start method | Params {0}", status.Message);
                return Ok(status);
            }
            var unit = productRepository.CrreateMeasureUnit(model);
            if(unit != null)
            {
                status.Code = 200;
                status.Message = "Successfully Created Units.";
                logger.LogInformation("Class:ProductController | Method:CreateUnits | Start method | Params {0}", status.Message);
                return Ok(status);
            }
            else
            {
                status.Code = 400;
                status.Message = "Failed to Create Units!!";
                logger.LogInformation("Class:ProductController | Method:CreateUnits | Start method | Params {0}", status.Message);
                return Ok(status);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUnits([FromBody] Units model, int id)
        {
            logger.LogInformation("Class:ProductController | Method:UpdateUnits | Start method | Params {0}", model.ToString());
            Status status = new Status();
            if (!ModelState.IsValid)
            {
                status.Code = 400;
                status.Message = "All Fields Are Required!!";
                logger.LogInformation("Class:ProductController | Method:UpdateUnits | Start method | Params {0}", status.Message);
                return Ok(status);
            }
            var details = await productRepository.GetUnitsById(id);
            if(details != null)
            {
                var result = productRepository.UpdateMeasureUnit(model);
                if(result != null)
                {
                    status.Code = 200;
                    status.Message = "Successfully Updated Measure Units.";
                    logger.LogInformation("Class:ProductController | Method:UpdateUnits | Start method | Params {0}", status.Message);
                    return Ok(status);
                }
                else
                {
                    status.Code = 400;
                    status.Message = "Failed to Update Measure Units!!";
                    logger.LogInformation("Class:ProductController | Method:UpdateUnits | Start method | Params {0}", status.Message);
                    return Ok(status);
                }
            }
            return Ok(status);
        }
        [HttpDelete]
        public IActionResult DeleteMeasureUnit(int id)
        {
            Status status = new Status();
            var result = productRepository.DeleteMeasureUnit(id);
            if (result != null)
            {
                status.Code = 200;
                status.Message = "Successfully Deleted Measure Units.";
                logger.LogInformation("Class:ProductController | Method:DeleteMeasureUnit | Start method | Params {0}", status.Message);
                return Ok(status);
            }
            else
            {
                status.Code = 400;
                status.Message = "Failed to Delete Measure Units!!";
                logger.LogInformation("Class:ProductController | Method:DeleteMeasureUnit | Start method | Params {0}", status.Message);
                return Ok(status);
            }
        }
    }
}
