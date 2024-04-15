using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
   
        private readonly IEmployeeRepo _employeeRepo;

        public EmployeeController(IEmployeeRepo employeeRepo)
        {
           
           _employeeRepo = employeeRepo;
        }

        [HttpGet]
        public IActionResult GetEmployee()
        {
           List<Employee> emps = _employeeRepo.GetAll();
            return Ok(emps);

        }
        [HttpGet]
        [Route("{id:int}",Name ="EmployeeDetailsRoute")]
        //model binder in api (primitive : route(parameter or querystring)) | (complex ===> request body)
        public IActionResult GetById(int id)
        {
            Employee emp = _employeeRepo.GetById(id);
            return Ok(emp);
        }

        [HttpGet("{name:alpha}")]

        public IActionResult GetByName(string name)
        {
            Employee emp = _employeeRepo.GetByName(name);   
            return Ok(emp);
        }

        [HttpGet("dept/{id}")]
        public IActionResult GetEmployeeWithDepartment(int id)
        {
            Employee emp = _employeeRepo.GetEmployeeDepartment(id);
            return Ok(emp);
        }
        [HttpGet("dto/{id}")]
        public IActionResult GetEmployeeWithDepartment2(int id)
        {
            Employee emp = _employeeRepo.GetEmployeeDepartment(id);
            EmployeeNameWithDepartmentNameDto empDto = new EmployeeNameWithDepartmentNameDto();
            empDto.EmpId = emp.Id;  
            empDto.EmpName = emp.Name;
            empDto.DeptName = emp.Department.Name;
            return Ok(empDto);
        }

        [HttpPost]
        public IActionResult PostEmployee(Employee newEmp)
        {
            _employeeRepo.Add(newEmp);
            string url = Url.Link("EmployeeDetailsRoute",new {id = newEmp.Id});
            return Created(url,newEmp);

        }

        [HttpPut("{id}")]
        public IActionResult PutEmployee(int id,Employee emp) { 
           if(ModelState.IsValid)
            {
         
                _employeeRepo.Update(id, emp);  
                 return StatusCode(204);
   
            }
            return BadRequest(ModelState);
        
        }

        [HttpDelete]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _employeeRepo.Delete(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);  
            }
        }

    }
}
