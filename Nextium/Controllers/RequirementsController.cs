using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nextium.DataLayer;
using Nextium.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Azure.Messaging;

namespace Nextium.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequirementsController : ControllerBase
    {
        private readonly NextiumContext _context;

        public RequirementsController(NextiumContext context)
        {
            _context = context;
        }

        // GET: api/Requirements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Requirements>>> GetRequirements()
        {
            return await _context.Requirements.ToListAsync();
        }

        // GET: api/Requirements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Requirements>> GetRequirement(int id)
        {
            var requirement = await _context.Requirements.FindAsync(id);

            if (requirement == null)
            {
                return NotFound();
            }

            return requirement;
        }

        // POST: api/Requirements
        [HttpPost]
        public async Task<ActionResult<Requirements>> PostRequirement(Requirements requirement)
        {
            try
            {
                _context.Requirements.Add(requirement);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Ok(new { Message = ex.Message });
            }   
            
             return Ok(new { Message = "Your Donation Request Successfully Submitted" });
        }

        // PUT: api/Requirements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequirement(int id, Requirements requirement)
        {
            if (id != requirement.requirement_Id)
            {
                return BadRequest();
            }

            _context.Entry(requirement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequirementExists(id))
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

        // DELETE: api/Requirements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequirement(int id)
        {
            var requirement = await _context.Requirements.FindAsync(id);
            if (requirement == null)
            {
                return NotFound();
            }

            _context.Requirements.Remove(requirement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequirementExists(int id)
        {
            return _context.Requirements.Any(e => e.requirement_Id == id);
        }

        // GET: api/Requirements/Search
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Requirements>>> SearchRequirements(
            [FromQuery] string searchword)
        {
            var query = _context.Requirements.AsQueryable();
            List<Requirements> searchResults = new List<Requirements>();

            if (!string.IsNullOrEmpty(searchword))
            {                
                query = query.Where(r => r.requirement.Contains(searchword) || r.Organization.name == searchword);
            }

            return await  query.ToListAsync();
        }       
    }
}
