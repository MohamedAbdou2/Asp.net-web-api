using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _productRepo;

        public ProductController(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            List<Product> products = _productRepo.GetAll();
            return Ok(products);    

        }
        [HttpGet("{id:int}",Name ="PrductDetailsRoute")]
        public IActionResult GetByProductId(int id)
        {
            Product product = _productRepo.GetById(id);
            return Ok(product);
        }

        [HttpPost]
        public IActionResult PostProduct(Product newproduct) {
            _productRepo.Add(newproduct);
            string url = Url.Link("PrductDetailsRoute", new { id = newproduct.Id });
            return Created(url,newproduct);

        }
        [HttpPut("{id}")]

        public IActionResult putProduct(int id,Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepo.Update(id, product);
                return StatusCode(StatusCodes.Status204NoContent); 

            }

            return BadRequest(ModelState);


        }

        [HttpDelete]

        public IActionResult deleteProduct(int id) 
        {
            try
            {
                _productRepo.Delete(id);
                return StatusCode(204);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
