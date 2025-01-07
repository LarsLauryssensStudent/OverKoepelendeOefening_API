
using System.ComponentModel.DataAnnotations;

namespace OverKoepelendeOefening_LarsLauryssens.Models
{
    public class Course : ICourse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        //navobject
        public ICollection<Enrollment>? Enrollments { get; set; } = null!;
    }
}
