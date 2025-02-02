﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Academic_project_manager_WebAPI.Models;
using Academic_project_manager_WebAPI.Models.DTOS;
using Microsoft.AspNetCore.Authorization;

namespace Academic_project_manager_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public GroupsController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/Groups
        //[HttpGet("[action]")]
        //public async Task<ActionResult<IEnumerable<GroupDTOS>>> Getgroups()
        //{
        //    return await (_context.groups.
        //        Include(g=>g.project))
        //        .Select(x=>GroupToDTOS(x)).
        //        ToListAsync();
        //}
        // GET: api/Groups
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<GroupDTOS1>>> Getgroups()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var coordinator= await _context.Coordinators.Where(c => c.supervisorId == userId).FirstOrDefaultAsync();
            return await (_context.groups.Where(g=>g.coordinatorId==coordinator.Id).
                Include(g => g.project)).Include(s=>s.Supervisor)
                .Select(x=>GroupToDTOS1(x))
                .ToListAsync();
        }
        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDTOS1>> GetGroup(int id)
        {
            var @group = await _context.groups.Where(g => g.groupId == id).Include(p => p.project).Include(s=>s.Supervisor).FirstOrDefaultAsync();

            if (@group == null)
            {
                return NotFound();
            }
            return GroupToDTOS1(@group);
        }
        //groups/getgroupsbysupervisor
        [HttpGet("[action]")]
        [Authorize(Roles ="Supervisor,Coordinator")]
        public async Task<ActionResult<IEnumerable<GroupDTOS1>>> GetGroupsBySupervisor()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var @group = await _context.groups.Where(g => g.supervisorId == userId).
                Include(p => p.project).Include(s => s.Supervisor).Select(x => GroupToDTOS1(x)).ToListAsync();

            if (@group == null)
            {
                return NotFound();
            }
            return @group;
        }
        //groups/getgroupsbysupervisor
        [HttpGet("[action]")]
        [Authorize(Roles = "Student,Supervisor,Coordinator")]
        public async Task<ActionResult<GroupDTOS1>> GetGroupsByStudent()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var @student = await _context.students.Where(s => s.Id == userId).FirstOrDefaultAsync();
            var @group = await _context.groups.Where(g => g.groupId == student.GroupId).
                Include(p => p.project).Include(s => s.Supervisor).Select(x => GroupToDTOS1(x)).FirstOrDefaultAsync();

            if (@group == null)
            {
                return NotFound();
            }
            return @group;
        }
        // PUT: api/Groups/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> PutGroup(int id, GroupDTOS1 @groupDTOS)
        {
            if (id != @groupDTOS.groupId)
            {
                return BadRequest();
            }
            var @group = await _context.groups.Where(g => g.groupId == id).Include(p => p.project).Include(s=>s.Supervisor).FirstOrDefaultAsync();
            if(@group==null)
            {
                return NotFound();
            }
            group.groupName = groupDTOS.groupName;
            group.supervisorId = groupDTOS.supervisorId;
            group.project.projectName = groupDTOS.projectName;
            group.project.projectDescription = groupDTOS.projectDescription;
            _context.Entry(@group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("[action]")]
        [Authorize(Roles ="Coordinator")]
        public async Task<ActionResult<GroupDTOS>> PostGroup(GroupDTOS groupDTOS)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var coordinator = await _context.Coordinators.Where(c => c.supervisorId == userId).FirstOrDefaultAsync();
            var group = new Group()
            {
                groupName = groupDTOS.groupName,
                supervisorId = groupDTOS.supervisorId,
                gitProjectId=groupDTOS.gitProjectId,
                created_at=groupDTOS.created_at,
                http_url_to_repo=groupDTOS.http_url_to_repo,
                coordinatorId=coordinator.Id,
                project = new project() { projectName = groupDTOS.projectName, projectDescription = groupDTOS.projectDescription }
            };
            _context.groups.Add(group);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new { id = group.groupId }, GroupToDTOS(group));
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Group>> DeleteGroup(int id)
        {
            var @group = await _context.groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }
            var @students = await _context.students.Where(s => s.GroupId == id).ToListAsync();
            foreach(var student in students)
            {
                student.GroupId = null;
            }
            _context.groups.Remove(@group);
            await _context.SaveChangesAsync();

            return @group;
        }
        //Assign Students to group
        [HttpPut("[action]")]
        public async Task<ActionResult> AssignStudent([FromBody]StudentGroupAssignDTO[] students)
        {
            var coordinatorId = User.Claims.First(c => c.Type == "UserID").Value;
            var coordinator = _context.Coordinators.First(c => c.supervisorId == coordinatorId);
            foreach (var student in students)
            {
                var stu = _context.students.First(s => s.Id == student.id);
                if(stu==null)
                {
                    return NotFound();
                }
                stu.GroupId = student.groupId;
                stu.coordinatorId = coordinator.Id;
                _context.Entry(stu).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
        //Update students by group
        [HttpPut("[action]/{id}")]
        public async Task<ActionResult> updateStudentsByGroupId([FromRoute]int id,[FromBody]StudentGroupAssignDTO[] students)
        {
            var studentGroup = await _context.students.Where(s => s.GroupId == id).ToListAsync();
            foreach(var student in studentGroup)
            {
                student.GroupId = null;
            }
            foreach(var student in students) 
            {
                var stu = _context.students.First(s => s.Id == student.id);
                if (stu == null)
                {
                    return NotFound();
                }
                stu.GroupId = student.groupId;
                _context.Entry(stu).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
        //Return Students by group
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<IEnumerable<StudentModel>>> getStudentsByGroup(int id)
        {
            var studentGroup = await _context.students.Where(s => s.GroupId == id).ToListAsync();
            return studentGroup;
        }
        private bool GroupExists(int id)
        {
            return _context.groups.Any(e => e.groupId == id);
        }
        private static GroupDTOS GroupToDTOS(Group group) =>
            new GroupDTOS
            {
                groupId = group.groupId,
                groupName = group.groupName,
                supervisorId = group.supervisorId,
                projectId = group.project.projectId,
                projectName = group.project.projectName,
                projectDescription = group.project.projectDescription,
                gitProjectId=group.gitProjectId,
                created_at=group.created_at,
                http_url_to_repo=group.http_url_to_repo
            };
        private static GroupDTOS1 GroupToDTOS1(Group group) =>
           new GroupDTOS1
           {
               groupId = group.groupId,
               groupName = group.groupName,
               supervisorId = group.supervisorId,
               username=group.Supervisor.UserName,
               projectId = group.project.projectId,
               projectName = group.project.projectName,
               projectDescription = group.project.projectDescription,
               gitProjectId = group.gitProjectId,
               created_at = group.created_at,
               http_url_to_repo = group.http_url_to_repo
           };
    }
}
