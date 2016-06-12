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
    public class TrackController : Controller
    {
        private MusicHistoryContext _context;

        public TrackController(MusicHistoryContext context)
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

            var track = from t in _context.Track
                        join al in _context.Album 
                        on t.AlbumId equals al.AlbumId
                        select new
                        {
                            TrackId = t.TrackId,
                            AlbumId = al.AlbumId,
                            AlbumTitle = al.AlbumTitle,
                            Title = t.Title,
                            Author = t.Author
                        };
            
                                                                 
            if (track == null)
            {
                return NotFound();
            }

            return Ok(track);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetTrackById")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var track = (from t in _context.Track
                          join al in _context.Album
                          on t.AlbumId equals al.AlbumId
                          select new
                          {
                              TrackId = t.TrackId,
                              AlbumId = al.AlbumId,
                              AlbumTitle = al.AlbumTitle,
                              Title = t.Title,
                              Author = t.Author
                          }).Single(m => m.TrackId == id);

            if (track == null)
            {
                return NotFound();
            }

            return Ok(track);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Track track)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Track.Add(track);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TrackExists(track.TrackId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetTrackById", new { id = track.TrackId }, track);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Track track)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != track.TrackId)
            {
                return BadRequest();
            }

            _context.Entry(track).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackExists(track.TrackId))
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

            Track track = _context.Track.Single(a => a.TrackId == id);
            if (track == null)
            {
                return NotFound();
            }

            _context.Track.Remove(track);
            _context.SaveChanges();

            return Ok(track);
        }

        private bool TrackExists(int id)
        {
            return _context.Track.Count(a => a.TrackId == id) > 0;
        }
    }
}
