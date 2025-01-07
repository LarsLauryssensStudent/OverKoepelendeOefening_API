namespace OverKoepelendeOefening_LarsLauryssens.Models
{
    public interface IEnrollment
    {
        int StudentId { get; set; }
        int CourseId { get; set; }
        DateTime EnrollmentDate { get; set; }

    }
}
