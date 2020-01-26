using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json;
using week3.Models;
using week3.Paginator;

namespace week3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }
        
        // GET api/movies/GetActorsPaged/page_index/page_size (with pagination)
        // e.g. http://localhost:5000/api/movies/GetActorsPaged/2/3
        [HttpGet("GetActorsPaged/{page_index}/{page_size}")]
        public IActionResult GetAllActors(int page_index, int page_size)
        {
            var res = _context.Actors.GetPage<Actor>(page_index, page_size, a => a.Id);
            if(res == null) return NotFound();
            return Ok(res);
        }

        // GET api/movies
        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get()
        {
            return _context.Movies.ToList();
        }

        // GET api/movies/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var movie = _context.Movies.FirstOrDefault(t => t.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return new ObjectResult(movie);
        }

        // POST api/movies
        [HttpPost]
        public void Post([FromBody] Movie value)
        {
        }

        // PUT api/movies/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Movie value)
        {
        }

        // DELETE api/movies/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
