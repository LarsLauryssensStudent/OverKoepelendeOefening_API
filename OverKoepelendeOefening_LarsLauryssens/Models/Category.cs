using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OverKoepelendeOefening_LarsLauryssens.Models
{
    public class Category : ICategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public List<Post>? Posts { get; set; } = new List<Post>();
    }
}
