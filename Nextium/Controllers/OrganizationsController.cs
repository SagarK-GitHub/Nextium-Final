using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nextium.DataLayer;
using Nextium.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Nextium.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly NextiumContext _context;

        public OrganizationsController(NextiumContext context)
        {
            _context = context;
        }

        // GET: api/Organizations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganizations()
        {
            //  // Example usage of hashing
            //  var hasher = new PasswordHasher();
            //  string password = "mySecurePassword";
            //  string hashedPassword = hasher.HashPassword(password);
            // // Console.WriteLine($"Hashed Password: {hashedPassword}");
            //  bool isPasswordValid = hasher.VerifyPassword(password, hashedPassword);
            ////  Console.WriteLine($"Password is valid: {isPasswordValid}");

            // Example usage of encryption
          //  var aesEncryption = new AesEncryption();
            //string plainText = "password@123";
            //string encryptedText = aesEncryption.Encrypt(plainText);
            // Console.WriteLine($"Encrypted Text: {encryptedText}");
            //string decryptedText = aesEncryption.Decrypt(encryptedText);
            // Console.WriteLine($"Decrypted Text: {decryptedText}");

            return await _context.Organizations.Include(o => o.Requirements).ToListAsync();
        }

        // GET: api/Organizations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetOrganization(int id)
        {
            var organization = await _context.Organizations.Include(o => o.Requirements)
                                                           .FirstOrDefaultAsync(o => o.organization_Id == id);

            if (organization == null)
            {
                return Ok(new { Message = "Organization not found" });
            }

            return organization;
        }

        // POST: api/Organizations
        [HttpPost]
        public async Task<ActionResult<Organization>> PostOrganization(Organization organization)
        {
            try
            {
                _context.Organizations.Add(organization);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Your Donation Request Successfully Submitted" });
            }
            catch (Exception ex)
            {
                return Ok(new { Message = ex.InnerException.Message });
            }            
        }

        // PUT: api/Organizations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganization(int id, Organization organization)
        {
            if (id != organization.organization_Id)
            {
                return BadRequest();
            }

            _context.Entry(organization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(id))
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

        // DELETE: api/Organizations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(int id)
        {
            var organization = await _context.Organizations.FindAsync(id);
            if (organization == null)
            {
                return NotFound();
            }

            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganizationExists(int id)
        {
            return _context.Organizations.Any(e => e.organization_Id == id);
        }

        //// GET: api/organization/search
        //[HttpGet("search")]
        //public IActionResult Search([FromQuery] string searchRequest)
        //{
        //    List<Organization> organizations = new List<Organization>();
        //    if (!string.IsNullOrEmpty(searchRequest))
        //    {
        //        var test = _context.Requirements;
        //        var query = _context.Organizations.Where(x => x.name.Contains(searchRequest));

        //        organizations.AddRange(query);
        //        if (organizations.Count > 0)
        //        {
        //            foreach (var org in organizations)
        //            {
        //                var requirementQuery = _context.Requirements.Where(x => x.organization_Id == org.organization_Id).ToList();
        //            }
        //        }

        //        var requirementSearch = _context.Requirements.Where(x => x.requirement.Contains(searchRequest)).ToList();

        //        foreach (var requirement in requirementSearch)
        //        {
        //            var Queryresult = organizations.Where(x => x.organization_Id == requirement.organization_Id).ToList();
        //            if (Queryresult.Count == 0)
        //            {
        //                var requery = _context.Organizations.Where(x => x.organization_Id == requirement.organization_Id).ToList();
        //                organizations.AddRange(requery);
        //            }
        //        }

        //    }

        //    var result = organizations;

        //    return Ok(result);
        //}

        [HttpGet("search")]
        public IActionResult Search(string searchRequest)
        {
            // Retrieve organizations
            var organizations = _context.Organizations.ToList();

            if (organizations == null || !organizations.Any())
            {
                return Ok(new { Message = "No organizations found." });
            }

            List<Requirements> requirementSearch = new List<Requirements>();

            foreach (var org in organizations)
            {
                var requirementQuery = _context.Requirements
                    .Where(x => x.organization_Id == org.organization_Id).ToList();

                if (requirementQuery == null || !requirementQuery.Any())
                {
                    continue; // Skip to the next organization if no requirements found
                }

                var requirementSearchResults = requirementQuery
                    .Where(x => x.requirement.Contains(searchRequest))
                    .ToList();

                if (requirementSearchResults != null && requirementSearchResults.Any())
                {
                    requirementSearch.AddRange(requirementSearchResults);
                }
            }

            if (!requirementSearch.Any())
            {
                return Ok(new { Message = "No requirements found for the search criteria." });
            }

            return Ok(requirementSearch);
        }

    }
}
