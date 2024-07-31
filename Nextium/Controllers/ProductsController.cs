using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Nextium.Models;
using Nextium.Repositories;

namespace Nextium.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ProductsController : ControllerBase
    {
        //private readonly ProductRepository _repository;

        //public ProductsController()
        //{
        //    _repository = new ProductRepository();
        //}

        //[HttpGet]
        //public ActionResult<IEnumerable<Product>> Get()
        //{
        //    return Ok(_repository.GetAll());
        //}

        //[HttpGet("{id}")]
        //public ActionResult<Product> Get(int id)
        //{
        //    var product = _repository.GetById(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(product);
        //}

        //[HttpPost]
        //public ActionResult<Product> Post([FromBody] Product product)
        //{
        //    _repository.Add(product);
        //    return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        //}

        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody] Product product)
        //{
        //    if (id != product.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var existingProduct = _repository.GetById(id);
        //    if (existingProduct == null)
        //    {
        //        return NotFound();
        //    }

        //    _repository.Update(product);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var existingProduct = _repository.GetById(id);
        //    if (existingProduct == null)
        //    {
        //        return NotFound();
        //    }

        //    _repository.Delete(id);
        //    return NoContent();
        //}
    }
}