using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class ITIEntity: DbContext
    {
        public ITIEntity() { }
        public ITIEntity(DbContextOptions options):base(options) { }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Product> Products { get; set; }   

        public DbSet<Department> Departments { get; set; }  

        public DbSet<Category> Categories { get; set; }





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=WebApi;Integrated Security=True;Encrypt=False;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
