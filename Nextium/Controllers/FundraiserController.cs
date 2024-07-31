using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nextium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nextium.DataLayer;

namespace Nextium.Controllers
{

   // [Route("api/[controller]")]
   // [ApiController]
    public class FundraisersController : ControllerBase
    {
        private readonly FundraiserRepository _repository;

        public FundraisersController()
        {
            _repository = new FundraiserRepository();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Fundraiser>> Get()
        {
            return Ok(_repository.GetAll());
        }
    }
}
