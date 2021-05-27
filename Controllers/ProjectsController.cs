using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab3AgendaV2.Data;
using Lab3AgendaV2.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Lab3AgendaV2.ViewModel.Projects;
using System.Security.Claims;

namespace Lab3AgendaV2.Controllers
{   
    [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TasksController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectsController(ApplicationDbContext context, IMapper mapper, ILogger<TasksController> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
    }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<Project>> GetProjects()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = _context.Projects.Where(p => p.ApplicationUser.Id == user.Id).Include(p => p.Tasks).FirstOrDefault());
            var resultViewModel = _mapper.Map<ProjectsForUserResponse>(result);

            return Ok(resultViewModel);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.Id)
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
                if (!ProjectExists(id))
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

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> AddProject(NewProjectRequest newProjectRequest)
        {
            //Get current_user
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            List<Models.Task> tasksOfProject = new List<Models.Task>();
            newProjectRequest.TasksOfProjectIds.ForEach(tId =>
            {
                var taskWithId = _context.Tasks.Find(tId);

                if(taskWithId != null)
                {
                    tasksOfProject.Add(taskWithId);
                }
            });

            if(tasksOfProject.Count == 0)
            {
                return BadRequest();
            }

            var project = new Project
            {
                ApplicationUser = user,
                Tasks = tasksOfProject,
                Name = newProjectRequest.Name,
                DateTimeProject = newProjectRequest.ProjectAddDateTime.GetValueOrDefault()
               
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
