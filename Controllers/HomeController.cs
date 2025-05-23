
using Laba.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laba.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }


       
        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] UserCreateDto dto)
        {
   
            var projects = dto.Projects
                .Select(p => new Project { Content = p.Content })
                .ToList();

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Projects = projects
            };


            foreach (var proj in projects)
            {
                _context.Entry(proj).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Added;
            }

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetUserById", new { id = user.Id }, user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
       
            var usersWithProjects = await _context.Users
                .Include(u => u.Projects)
                .ToListAsync();   

            var userDtos = usersWithProjects
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Projects = u.Projects
                        .Select(p => new ProjectResponseDto
                        {
                            Id = p.Id,
                            Content = p.Content
                        })
                        .ToList()  
                })
                .ToList();

            return Ok(userDtos);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID в URL и в теле должны совпадать.");

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            user.Name = dto.Name;
            user.Email = dto.Email;
  

            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
