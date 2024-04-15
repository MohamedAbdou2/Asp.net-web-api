using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IEmployeeRepo
    {
        List<Employee> GetAll();    
        Employee GetById(int id);
        Employee GetByName(string name);

        Employee GetEmployeeDepartment(int id);

        void Add(Employee employee);

        void Update(int id ,Employee employee);

        void Delete(int id);


    }
}
