using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab3AgendaV2.Data;
using Lab3AgendaV2.Models;
using Lab3AgendaV2.ViewModel;
using AutoMapper;

namespace Lab3AgendaV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TasksController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        /// <summary>
        ///   Returns filters tasks by deadline 
        /// </summary>
        /// <param name="startDate">start of date check</param>
        /// <param name="endDate">end of date check</param>
        /// <returns>A list of tasks with the deadline</returns>
        [HttpGet]
        [Route("filter/startDate={startDate:datetime}&endDate={endDate:datetime}")]
        public async Task<ActionResult<IEnumerable<Models.Task>>> FilterByTaskDeadline(DateTime startDate, DateTime endDate)
        {

            return await _context.Tasks.Where(task => startDate < task.DateTimeDeadline && task.DateTimeDeadline < endDate).ToListAsync();
        }


        [HttpPost("{id}/Comments")]
        public IActionResult PostCommentForTask(int id, Comment comment)
        {
            comment.Task = _context.Tasks.Find(id);
            if (comment.Task == null)
            {
                return NotFound();
            }
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("{id}/Comments")]
        public ActionResult<IEnumerable<TaskWithCommentViewModel>> GetCommentForTask(int id)
        {
            var query = _context.Comments.Where(comm => comm.Task.Id == id).Select(c => new TaskWithCommentViewModel
            {
                Id = c.Task.Id,
                Title = c.Task.Title,
                Description = c.Task.Description,
                DateTimeAdded = c.Task.DateTimeAdded,
                DateTimeDeadline = c.Task.DateTimeDeadline,
                Importance = c.Task.Importance,
                State = c.Task.State,
                DateTimeClosedAt = c.Task.DateTimeClosedAt,
                Comments = c.Task.Comments.Select(tc => new CommentViewModel
                {
                    Id = tc.Id,
                    Text = tc.Text,
                    Important = tc.Important,
                    CommentDatetime = tc.CommentDatetime

                }).ToList()

            }) ;

            var query_two = _context.Tasks.Where(comm => comm.Id == id).Select(t => _mapper.Map<TaskWithCommentViewModel>(t));

            return query_two.ToList();
        }


        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskViewModel>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            var taskViewModel = _mapper.Map<TaskViewModel>(task);

            return taskViewModel;
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, Models.Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
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

        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Models.Task>> PostTask(Models.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { id = task.Id }, task);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/comments/5
        [HttpDelete("comments/{id}")]
        public async Task<IActionResult> DeleteComment(long id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
