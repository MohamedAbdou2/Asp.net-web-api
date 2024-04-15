using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Product
    {
       public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [Range(10.5,20000.5)]
        public double Price { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [ForeignKey("Category")]
        public int? Category_Id { get; set; }

        public Category? Category { get; set; }
    }
}
