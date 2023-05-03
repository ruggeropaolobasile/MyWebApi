using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApi.model;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobOffersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JobOffersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/JobOffers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobOffer>>> GetJobOffers()
        {
            return await _context.JobOffers.ToListAsync();
        }

        // GET: api/JobOffers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobOffer>> GetJobOffer(int id)
        {
            var jobOffer = await _context.JobOffers.FindAsync(id);

            if (jobOffer == null)
            {
                return NotFound();
            }

            return jobOffer;
        }

        // POST: api/JobOffers
        [HttpPost]
        public async Task<ActionResult<JobOffer>> CreateJobOffer(JobOffer jobOffer)
        {
            _context.JobOffers.Add(jobOffer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJobOffer), new { id = jobOffer.Id }, jobOffer);
        }

        // PUT: api/JobOffers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobOffer(int id, JobOffer jobOffer)
        {
            if (id != jobOffer.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobOffer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobOfferExists(id))
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

        // DELETE: api/JobOffers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobOffer(int id)
        {
            var jobOffer = await _context.JobOffers.FindAsync(id);
            if (jobOffer == null)
            {
                return NotFound();
            }

            _context.JobOffers.Remove(jobOffer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobOfferExists(int id)
        {
            return _context.JobOffers.Any(e => e.Id == id);
        }
    }
}
