using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OverKoepelendeOefening_LarsLauryssens.Data;
using OverKoepelendeOefening_LarsLauryssens.Models;

namespace OverKoepelendeOefening_LarsLauryssens.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnrollmentsController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [HttpPost]
        public async Task<ActionResult> AddEnrollment(int StudentId, int CourseId)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == StudentId);
            var course = await _context.Students.FirstOrDefaultAsync(x=> x.Id == CourseId);
            if (student == null || course == null)
            {
                return BadRequest("Gelieve correcte Id's in te geven");
            }
            var result = await _context.Enrollments.FirstOrDefaultAsync(x => x.CourseId == course.Id && x.StudentId == student.Id);
            if (result != null)
            {
                return BadRequest("deze student is al ingeschreven voor deze cursus");
            }

            Enrollment newEnroll = new Enrollment()
            {
                StudentId = StudentId,
                CourseId = CourseId,
                EnrollmentDate = DateTime.Now
            };

            try
            {
                _context.Enrollments.Add(newEnroll);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(newEnroll);
        }

        [HttpDelete("{studentId:int}/{courseId:int}")]
        public async Task<ActionResult> DeleteEnrollment([FromRoute] int studentId, [FromRoute] int CourseId)
        {
            var result = await _context.Enrollments.FirstOrDefaultAsync( x => x.CourseId == CourseId && x.StudentId == studentId);
            if (result == null)
            {
                return BadRequest("Geen enrollment met deze keys");
            }
            try
            {
                _context.Enrollments.Remove(result);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
   

    }
}
