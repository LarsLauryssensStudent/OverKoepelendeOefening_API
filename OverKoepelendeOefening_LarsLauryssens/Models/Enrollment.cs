
using System.ComponentModel.DataAnnotations;

namespace OverKoepelendeOefening_LarsLauryssens.Models
{
    public class Enrollment : IEnrollment
    {
        [Required]
        public int StudentId {  get; set; }
        //navobject
        public Student? Student { get; set; } = null!;


        [Required]
        public int CourseId { get; set; }
        //navobject
        public Course? Course { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }
    }
}
