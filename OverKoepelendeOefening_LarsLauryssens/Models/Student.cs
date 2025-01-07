
using System.ComponentModel.DataAnnotations;

namespace OverKoepelendeOefening_LarsLauryssens.Models
{
    public class Student : IStudent
    {
        [Key]
        public int Id {  get; set; }

        [Required]
        public string FirstName {  get; set; } = string.Empty;

        [Required]
        public string LastName {  get; set; } = string.Empty;

        [Required]
        public string Email {  get; set; } = string.Empty;

        //navobject
        //We maken hier gebruik van een collection ipv een list
        public ICollection<Enrollment>? Enrollments { get; set; } = null!;
    }
}
