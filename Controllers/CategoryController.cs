using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DTO;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ITIEntity _context;

        public CategoryController(ITIEntity context)
        {
            _context = context;
        }

        [HttpGet("{id:int}",Name ="CategoryDetailsRoute")]
        public IActionResult GetCategory(int id)
        {

            Category cat = _context.Categories.FirstOrDefault(c => c.Id == id);
            return Ok(cat);

        }



        [HttpGet("product/{id}")]
        public IActionResult GetCategorywithProductList(int id) { 
        
            Category cat = _context.Categories.Include(p=>p.products).FirstOrDefault(c => c.Id == id);

            CategoryNameWithProductListDto catDto = new CategoryNameWithProductListDto();

            catDto.Name = cat.Name;
            
            foreach(var item in cat.products)
            {
                catDto.Catproducts.Add(new PrductListDto { Id = item.Id ,Name = item.Name});
            }



            return Ok(catDto);
        
        }

        [HttpPost]

        public IActionResult PostCategory(Category Newcategory)
        {
            _context.Categories.Add(Newcategory);
            _context.SaveChanges();
            string url = Url.Link("CategoryDetailsRoute",new { id = Newcategory.Id }); 
            return Created(url,Newcategory);

        }
    }
}
