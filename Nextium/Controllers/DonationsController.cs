using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nextium.Models; 
using System.Threading.Tasks;
using System.Collections.Generic;
using Nextium.DataLayer;
using System.Linq;
using System;

namespace Nextium.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationsController : ControllerBase
    {
        private readonly NextiumContext _context;

        public DonationsController(NextiumContext context)
        {
            _context = context;
        }

        // GET: api/Donations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donation>>> GetDonations()
        {
            return await _context.Donations.Include(d => d.Donor).ToListAsync();
        }

        // GET: api/Donations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Donation>> GetDonation(int id)
        {
            var donation = await _context.Donations.Include(d => d.Donor).FirstOrDefaultAsync(d => d.DonationId == id);

            if (donation == null)
            {
                return Ok(new { Message = "Not Found" });
            }

            return donation;
        }

        // PUT: api/Donations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonation(int id, Donation donation)
        {
            if (id != donation.DonationId)
            {
                return Ok(new { Message = "Donation Not Exist" });
            }

            _context.Entry(donation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonationExists(id))
                {
                    return Ok(new { Message = "Donation Not Found" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { Message = "Donation Details Updated Successfully" }); 
        }

        // POST: api/Donations
        [HttpPost]
        public async Task<ActionResult<Donation>> PostDonation(Donation donation)
        {
            try
            {
                _context.Donations.Add(donation);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Donations Created Successfully" }); // CreatedAtAction("GetDonation", new { id = donation.DonationId }, donation);
            }
            catch (Exception ex)
            {
                return Ok(new { Message = ex.InnerException.Message });
            }
        }

        // DELETE: api/Donations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonation(int id)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }

            _context.Donations.Remove(donation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DonationExists(int id)
        {
            return _context.Donations.Any(e => e.DonationId == id);
        }
    }
}
