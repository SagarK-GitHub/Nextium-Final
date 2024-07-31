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
    public class DonorsController : ControllerBase
    {
        private readonly NextiumContext _context;

        public DonorsController(NextiumContext context)
        {
            _context = context;
        }

        // GET: api/Donors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donor>>> GetDonors()
        {
            return await _context.Donors.ToListAsync();
        }

        // GET: api/Donors/5
        [HttpGet("{phoneNo}")]
        public ActionResult<Donor> GetDonor(string phoneNo)
        {
            var donor = _context.Donors.Where(x => x.PhoneNumber == phoneNo).ToList();

            if (donor.Count == 0)
            {
                return Ok(new { Message = "Donor Not Found" }); 
            }

            return Ok(donor);
        }

        // PUT: api/Donors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonor(int id, Donor donor)
        {
            if (id != donor.DonorId)
            {
                return Ok(new { Message = "Donor Not Found" }); 
            }

            _context.Entry(donor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonorExists(id))
                {
                    return Ok(new { Message = "Donot Not Found" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { Message = "Donor Details Updated Successfully" });
        }

        // POST: api/Donors
        [HttpPost]
        public async Task<ActionResult<Donor>> PostDonor([FromBody] Donor donor)
        {
            try
            {
                var donors = _context.Donors.Where(x => x.PhoneNumber == donor.PhoneNumber).ToList();
                if (donors.Count == 0)
                {
                    _context.Donors.Add(donor);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    donors[0].Email = donor.Email;
                    donors[0].FirstName = donor.FirstName;
                    donors[0].LastName = donor.LastName;
                    donors[0].Address = donor.Address;
                    donors[0].City = donor.City;
                    donors[0].State = donor.State;
                    donors[0].ZipCode = donor.ZipCode;
                    donors[0].Country = donor.Country;
                    donors[0].Donations = donor.Donations;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Message = ex.InnerException.Message });
            }

            return Ok(new { Message = "Donation Successfull" });
        }

        // DELETE: api/Donors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonor(int id)
        {
            var donor = await _context.Donors.FindAsync(id);
            if (donor == null)
            {
                return NotFound();
            }

            _context.Donors.Remove(donor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DonorExists(int id)
        {
            return _context.Donors.Any(e => e.DonorId == id);
        }
    }
}
