using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Academic_project_manager_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace Academic_project_manager_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public ActivitiesController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/Activities
        [HttpGet]
        [Authorize(Roles ="Coordinator")]
        public async Task<ActionResult<IEnumerable<Activities>>> GetActivities()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var coordinator = await _context.Coordinators.Where(c => c.supervisorId == userId).FirstOrDefaultAsync();
            return await _context.Activities.Where(a=>a.coordinatorId==coordinator.Id).OrderByDescending(c => c.Id).ToListAsync();
        }
        [HttpGet("[action]")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<IEnumerable<Activities>>> GetActivitiesForStudents()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var student = await _context.students.Where(s => s.Id == userId).FirstOrDefaultAsync();
            return await _context.Activities.Where(a => a.coordinatorId == student.coordinatorId).Include(s=>s.Coordinator).OrderByDescending(c=>c.Id).ToListAsync();
        }
        // GET: api/Activities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Activities>> GetActivities(int id)
        {
            var activities = await _context.Activities.FindAsync(id);

            if (activities == null)
            {
                return NotFound();
            }

            return activities;
        }

        // PUT: api/Activities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivities(int id, Activities activities)
        {
            if (id != activities.Id)
            {
                return BadRequest();
            }

            _context.Entry(activities).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivitiesExists(id))
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

        // POST: api/Activities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Activities>> PostActivities(Activities activities)
        {
            _context.Activities.Add(activities);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivities", new { id = activities.Id }, activities);
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Activities>> DeleteActivities(int id)
        {
            var activities = await _context.Activities.FindAsync(id);
            if (activities == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activities);
            await _context.SaveChangesAsync();

            return activities;
        }

        private bool ActivitiesExists(int id)
        {
            return _context.Activities.Any(e => e.Id == id);
        }
    }
}
