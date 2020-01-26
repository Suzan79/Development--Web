using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SimpleModelsAndRelations.Model;

namespace EmptyTemplateMVCReact.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {

        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;

            if (_context.Movies.Count() == 0 & _context.Actors.Count() == 0)
            {
                Movie m1 = new Movie
                {
                    Title = "No Country For Old Men"
                };
                Movie m2 = new Movie
                {
                    Title = "The Matrix"
                };

                Actor a1 = new Actor
                {
                    Name = "Actor One",
                    Gender = "Male",
                    Birth = new DateTime(1970, 1, 1)
                };

                Actor a2 = new Actor
                {
                    Name = "Actor Two",
                    Gender = "Female",
                    Birth = new DateTime(1970, 1, 1)
                };

                Actor a3 = new Actor
                {
                    Name = "Actor Three",
                    Gender = "Female",
                    Birth = new DateTime(1970, 1, 1)
                };
                Actor a4 = new Actor
                {
                    Name = "Actor Four",
                    Gender = "Male",
                    Birth = new DateTime(1970, 1, 1)
                };

                Actor a5 = new Actor
                {
                    Name = "Actor Five",
                    Gender = "Female",
                    Birth = new DateTime(1970, 1, 1)
                };

                MovieActor ma1 = new MovieActor();
                ma1.Actor = a1;
                ma1.Movie = m1;

                _context.Add(ma1);
                _context.SaveChanges();

                ma1.Actor = a3;

                _context.Add(ma1);
                _context.SaveChanges();

                ma1.Actor = a5;

                _context.Add(ma1);
                _context.SaveChanges();

                MovieActor ma2 = new MovieActor();
                ma2.Actor = a2;
                ma2.Movie = m2;

                _context.Add(ma2);
                _context.SaveChanges();

                ma2.Actor = a4;

                _context.Add(ma2);
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IActionResult Get()
        {   
            var movies = from m in _context.Movies 
                let actorsList = (from mv in _context.MovieActor where mv.MovieId == m.Id
                                join a in _context.Actors on mv.ActorId equals a.Id
                                select new {Id = a.Id , Name = a.Name , Gender = a.Gender, Birth = a.Birth}).ToList()
                        select new {Id = m.Id, Title = m.Title, Actors = actorsList};
            
            return Ok(movies);
        }

    }
}