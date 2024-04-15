using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual List<Product>? products { get; set; }
    }
}
