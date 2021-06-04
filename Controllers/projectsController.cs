using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Academic_project_manager_WebAPI.Models;

namespace Academic_project_manager_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class projectsController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public projectsController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/projects
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<project>>> Getprojects()
        {
            return await _context.projects.Include(g => g.Group).Include(g=>g.Group.Supervisor).Include(g=>g.Group.Student).ToListAsync();
        }
        // GET: api/groups
        [HttpGet("[action]")]
        public async Task<Object> GetGroups()
        {
            return await _context.groups.ToListAsync();
        }
        // GET: api/projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<project>> Getproject(int id)
        {
            var project = await _context.projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // PUT: api/projects/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Putproject(int id, project project)
        {
            if (id != project.projectId)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!projectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/projects
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("[action]")]
        public async Task<ActionResult<project>> Postproject(project project)
        {
            _context.projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getproject", new { id = project.projectId }, project);
        }

        // DELETE: api/projects/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<project>> Deleteproject(int id)
        {
            var project = await _context.projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.projects.Remove(project);
            await _context.SaveChangesAsync();

            return project;
        }

        private bool projectExists(int id)
        {
            return _context.projects.Any(e => e.projectId == id);
        }
    }
}
