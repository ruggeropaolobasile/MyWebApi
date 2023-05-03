using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApi.model;
using InterviewManager.Services;


namespace MyWebApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly GoogleCalendarService _googleCalendarService;

        public InterviewsController(ApplicationDbContext context, GoogleCalendarService googleCalendarService)
        {
            _context = context;
            _googleCalendarService = googleCalendarService;
        }


        // GET: api/Interviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Interview>>> GetInterviews()
        {
            return await _context.Interviews.ToListAsync();
        }

        // GET: api/Interviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Interview>> GetInterview(int id)
        {
            var interview = await _context.Interviews.FindAsync(id);

            if (interview == null)
            {
                return NotFound();
            }

            return interview;
        }

        // PUT: api/Interviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInterview(int id, Interview interview)
        {
            if (id != interview.Id)
            {
                return BadRequest();
            }

            _context.Entry(interview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterviewExists(id))
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

        // POST: api/Interviews
        [HttpPost]
        public async Task<ActionResult<Interview>> CreateInterview(Interview interview)
        {
            _context.Interviews.Add(interview);
            await _context.SaveChangesAsync();

            // Aggiungere l'evento al calendario
            var calendarId = "primary"; // Sostituire con l'ID del calendario desiderato
            await _googleCalendarService.CreateInterviewEvent(calendarId, interview);

            return CreatedAtAction(nameof(GetInterview), new { id = interview.Id }, interview);
        }

        // DELETE: api/Interviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterview(int id)
        {
            var interview = await _context.Interviews.FindAsync(id);
            if (interview == null)
            {
                return NotFound();
            }

            _context.Interviews.Remove(interview);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InterviewExists(int id)
        {
            return _context.Interviews.Any(e => e.Id == id);
        }
    }
}
