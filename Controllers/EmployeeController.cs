using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ITIEntity _context;

        public EmployeeController(ITIEntity context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetEmployee()
        {
           List<Employee> emps = _context.Employees.ToList();
            return Ok(emps);

        }
        [HttpGet]
        [Route("{id:int}",Name ="EmployeeDetailsRoute")]
        //model binder in api (primitive : route(parameter or querystring)) | (complex ===> request body)
        public IActionResult GetById(int id)
        {
            Employee emp = _context.Employees.FirstOrDefault(x => x.Id == id);
            return Ok(emp);
        }

        [HttpGet("{name:alpha}")]

        public IActionResult GetByName(string name)
        {
            Employee emp = _context.Employees.FirstOrDefault(x => x.Name == name);
            return Ok(emp);
        }

        [HttpPost]
        public IActionResult PostEmployee(Employee newEmp)
        {
            _context.Employees.Add(newEmp);
            _context.SaveChanges();
            string url = Url.Link("EmployeeDetailsRoute",new {id = newEmp.Id});
            return Created(url,newEmp);

        }

        [HttpPut("{id}")]
        public IActionResult PutEmployee(int id,Employee emp) { 
           if(ModelState.IsValid)
            {
                Employee OldEmp = _context.Employees.FirstOrDefault(x=>x.Id == id);
                if(OldEmp != null)
                {
                    OldEmp.Name = emp.Name;
                    OldEmp.Salary = emp.Salary;
                    OldEmp.Address = emp.Address;
                    OldEmp.Age = emp.Age;
                    _context.SaveChanges();
                    return StatusCode(204);
                }
              return BadRequest();

            }
            return BadRequest(ModelState);
        
        }

        [HttpDelete]
        public IActionResult DeleteById(int id)
        {
            try
            {
                Employee emp = _context.Employees.FirstOrDefault(x => x.Id == id);
                _context.Employees.Remove(emp);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);  
            }
        }

    }
}
