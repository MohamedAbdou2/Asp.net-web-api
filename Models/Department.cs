using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        public string ManagerName { get; set; }

        [JsonIgnore]
        //navigation
        public virtual List<Employee>? Employees { get; set; }

    }
}
