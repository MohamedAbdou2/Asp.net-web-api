using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DTO;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly ITIEntity _context;

        public DepartmentController(ITIEntity context)
        {
           _context = context;
        }

        [HttpGet]
        public IActionResult GetDeptById(int id)
        {
          Department dept =   _context.Departments.Include(e=>e.Employees).FirstOrDefault(d=>d.Id == id);
            //mapping dept to deptdto 

         DepartmetnWithEmployeesDto deptDto = new DepartmetnWithEmployeesDto();
         deptDto.Id = dept.Id;   
         deptDto.Name = dept.Name;
         foreach(var item in dept.Employees)
            {
                deptDto.EmpNames.Add(new EmployeeDto { Id = item.Id, Name = item.Name });

            }
         return Ok(deptDto);

        }
    }
}
