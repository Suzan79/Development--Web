using System;
using Model;
using System.Linq;
using System.Collections.Generic;

namespace Week1
{
    class Program
    {
        static void Main(string[] args)
        {
            // using (var db = new MovieContext())
            // {
            //     Movie m = new Movie
            //     {
            //         Title = "No country for old men",
            //         Actors = new List<Actor> {
            //             new Actor{Name = "Tommy Lee"},
            //             new Actor{Name = "Xavier Berdem"}
            //         }
            //     };
            //     db.Movies.Add(m);
            //     db.SaveChanges();
            // }

            using (var db = new MovieContext())
            {
                foreach (var movie in db.Movies)
                {
                    Console.WriteLine("Found movie with title " + movie.Title);
                    foreach (var actor in db.Actors.Where(a => movie.Id == a.MovieId))
                    {
                        Console.WriteLine("Found actor with name " + actor.Name);
                    }
                }
            }
        }
    }
}
