using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MusicHistoryAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace MusicHistoryAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [EnableCors("AllowDevelopmentEnvironment")]
    public class CustomerController : Controller
    {
        private MusicHistoryContext _context;

        public CustomerController(MusicHistoryContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get([FromQuery]int? id, string customerName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<Customer> customer = from c in _context.Customer
                                             // where c.CustomerId == id
                                             select new Customer
                                             {
                                                 CustomerId = c.CustomerId,
                                                 CustomerName = c.CustomerName,
                                                 CreatedDate = c.CreatedDate,
                                                 Location = c.Location,
                                                 Email = c.Email,
                                                 FavoriteTracks = from t in _context.Track
                                                                  join al in _context.Album on t.AlbumId equals al.AlbumId
                                                                  join ar in _context.Artist on al.ArtistId equals ar.ArtistId
                                                                  select new TrackInfo
                                                                  {
                                                                      AlbumTitle = al.AlbumTitle,
                                                                      YearReleased = al.YearReleased,
                                                                      Author = t.Author,
                                                                      Genre = t.Genre,
                                                                      Title = t.Title
                                                                  }
                                             };

            if (id != null)
            {
                customer = customer.Where(cus => cus.CustomerId == id);
            }

            if (customerName != null)
            {
                customer = customer.Where(cus => cus.CustomerName == customerName);
            }

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = from g in _context.Customer
                               where g.CustomerName == customer.CustomerName
                               select g;

            if (existingUser.Count<Customer>() > 0)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            _context.Customer.Add(customer);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            // return CreatedAtRoute("GetCustomer", new { id = geek.GeekId }, geek);
            return Ok(existingUser);
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.CustomerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer customer = _context.Customer.Single(c => c.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            _context.SaveChanges();

            return Ok(customer);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Count(c => c.CustomerId == id) > 0;
        }
    }
}
