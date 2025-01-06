using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OverKoepelendeOefening_LarsLauryssens.Data;
using OverKoepelendeOefening_LarsLauryssens.Models;

namespace OverKoepelendeOefening_LarsLauryssens.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PostsController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPosts()
        {
            var posts = await _context.Posts.ToListAsync();
            if (posts == null)
            {
                return NotFound();
            }
            if (posts.Count == 0)
            {
                return Ok("Geen posts aanwezig");
            }
            return Ok(posts);

        }

        [HttpGet("details/{id:int}")]
        public async Task<ActionResult> GetPostById([FromRoute] int id)
        {
            var result = await _context.Posts.SingleOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return NotFound("Geen post met dit Id aanwezig");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddPost([FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Gelieve correcte data in te vullen");
            }

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPostById),new { id = post.Id}, post);
        }


    }
}
