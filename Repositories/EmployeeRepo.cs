using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly ITIEntity _context;

        public EmployeeRepo(ITIEntity context)
        {
            _context = context;
        }

        public List<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public Employee GetById(int id)
        {
            return _context.Employees.FirstOrDefault(x => x.Id == id);
        }

        public Employee GetByName(string name)
        {
            return _context.Employees.FirstOrDefault(x => x.Name == name);
        }

        public Employee GetEmployeeDepartment(int id)
        {
            return _context.Employees.Include(e => e.Department).FirstOrDefault(x => x.Id == id);
        }
        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges(); 
        }

        public void Delete(int id)
        {
           Employee emp = GetById(id);
           if (emp != null) { 
             _context.Remove(emp);
            _context.SaveChanges(); 
            }
        }

      

        public void Update(int id, Employee emp)
        {
            Employee OldEmp = GetById(id);
            if (OldEmp != null)
            {
                OldEmp.Name = emp.Name;
                OldEmp.Salary = emp.Salary;
                OldEmp.Address = emp.Address;
                OldEmp.Age = emp.Age;
                _context.SaveChanges();
            }
        }

       
    }
}
