using System.ComponentModel.DataAnnotations;

namespace DevopsAssignment.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
    }
}
