namespace WebApi.DTO
{
    public class DepartmetnWithEmployeesDto
    {
        public int Id { get; set; } 

        public string Name { get; set; }    

        public List<EmployeeDto> EmpNames { get; set; } = new List<EmployeeDto>();

    }

    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
