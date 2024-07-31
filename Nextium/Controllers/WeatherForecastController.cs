using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nextium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nextium.Controllers
{
     [ApiController]
     [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

       // [HttpGet]
        //public IEnumerable<Fundraiser> GetFundraisers()
        //{
        //    //return await new Task<ActionResult<IEnumerable<Fundraiser>>>() =
        //    //{
        //    //    new Fundraiser{ },
        //    //};
        //    //return await _context.Fundraisers.ToListAsync();

        //    return new List<Fundraiser>()
        //    {
        //        new Fundraiser(){ Id=1, Description="Desc1", Goal=100000, AmountRaised=20000, Title="Title 1"},
        //        new Fundraiser(){ Id=2, Description="Desc2", Goal=200000, AmountRaised=50000, Title="Title 2"},
        //    }.ToArray();
        //}

        //[HttpPost]
        //public async Task<ActionResult<Fundraiser>> CreateFundraiser(Fundraiser fundraiser)
        //{
        //    //_context.Fundraisers.Add(fundraiser);
        //    //await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetFundraisers), new { id = fundraiser.Id }, fundraiser);
        //}

        //public IEnumerable<Fundraiser> GetFundraisersDetails()
        //{
        //    return new List<Fundraiser>()
        //    {
        //        new Fundraiser(){ Id=1, Description="Desc1", Goal=100000, AmountRaised=20000, Title="Title 1"},
        //        new Fundraiser(){ Id=2, Description="Desc2", Goal=200000, AmountRaised=50000, Title="Title 2"},
        //    };
        //}
    }
}
