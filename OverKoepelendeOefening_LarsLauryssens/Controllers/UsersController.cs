using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OverKoepelendeOefening_LarsLauryssens.Data;
using OverKoepelendeOefening_LarsLauryssens.DTO;
using OverKoepelendeOefening_LarsLauryssens.Models;

namespace OverKoepelendeOefening_LarsLauryssens.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext context;

        public UsersController(AppDbContext appDbContext)
        {
            context = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var result = await context.Users
                .Include(u => u.Profile)
                .ToListAsync();

            if (result == null )
            {
                return BadRequest("Geen users gevonden");
            }
            return Ok(result);
        }


        [HttpGet("details/{id:guid}")]
        public async Task<ActionResult> GetUserWithProfile([FromRoute] Guid id)
        {
            var result = await context.Users
                .Include(u => u.Profile)
                .Select(p => new UserDetailsDTO()
                {
                    Id = p.Id,
                    Email = p.Email,
                    Password = p.Password,
                    Username = p.Username,
                    profile = new ProfileUserDTO()
                    {
                        Id = p.Profile.Id,
                        Biography = p.Profile.Biography,
                        Location = p.Profile.Location,
                        BirthDate = p.Profile.BirthDate,
                        UserId = p.Profile.UserId
                    }
                })
                .FirstOrDefaultAsync(u => u.Id == id);

            if (result == null )
            {
                return BadRequest("Geen user met dit ID aanwezig");

            }
            return Ok(result);

        }

        [HttpPost]
        public async Task<ActionResult> AddUserWithProfile([FromBody] AddUserWithProfileDTO user)
        {
            var newUser = new User()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Username = user.Username,
                Profile = new Profile()
                {
                    Id = user.profile.Id,
                    Biography = user.profile.Biography,
                    BirthDate = user.profile.BirthDate,
                    Location = user.profile.Location,
                    UserId = user.Id
                }
            };

            try
            {
                context.Users.Add(newUser);
                await context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAllUsers), user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateuserAndProfile([FromRoute] Guid id, User newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Gelieve correcte data in te vullen");
            }
            if (newUser.Id != id)
            {
                return BadRequest("Gelieve een correcte Id in te vullen");
            }
            // we gebruiken de entrystate om aan te geven dat er een aanpassing is gebeurt
            context.Entry(newUser).State = EntityState.Modified;
            if (newUser.Profile != null)
            {
                // Attach the Profile entity and mark it as modified
                context.Entry(newUser.Profile).State = EntityState.Modified;
            }
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();


        }

        [HttpDelete("{id:guid}")] 
        public async Task<ActionResult> DeleteUser([FromRoute] Guid id)
        {
            var userToDel = await context.Users.Include(x => x.Profile).FirstOrDefaultAsync(us => us.Id == id);

            if (userToDel == null)
            {
                return NotFound("Geen user met dit id gevonden");
            }

            try
            {
                context.Users.Remove(userToDel);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
