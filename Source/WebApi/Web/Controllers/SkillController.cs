using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DrakeLambert.Peerra.WebApi.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrakeLambert.Peerra.WebApi.Web.Controllers
{
    public class SkillController : ApiControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SkillController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> UsersWithSkills([FromQuery, Required] string skills)
        {
            var skillList = skills.Split(' ').Where(skill => skill.Length > 0).Select(skill => skill.ToLower()).ToList();
            var users = await _context.UserSkills.Where(userSkill => skillList.Contains(userSkill.SkillName)).ToListAsync();
            return Ok(users);
        }
    }
}
