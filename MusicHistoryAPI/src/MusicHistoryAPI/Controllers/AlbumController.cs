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
    public class AlbumController : Controller
    {
        private MusicHistoryContext _context;

        public AlbumController(MusicHistoryContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var album = from a in _context.Album
                        select a;

            if (album == null)
            {
                return NotFound();
            }

            return Ok(album);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetAlbumById")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Album album = _context.Album.Single(m => m.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }

            return Ok(album);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Album.Add(album);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AlbumExists(album.AlbumId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetAlbumById", new { id = album.AlbumId }, album);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Album album)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != album.AlbumId)
            {
                return BadRequest();
            }

            _context.Entry(album).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(album.AlbumId))
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

            Album album = _context.Album.Single(a => a.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }

            _context.Album.Remove(album);
            _context.SaveChanges();

            return Ok(album);
        }

        private bool AlbumExists(int id)
        {
            return _context.Album.Count(a => a.AlbumId == id) > 0;
        }
    }
}
