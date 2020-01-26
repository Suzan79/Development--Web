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
            // insertPrimaryData();
            // getTitle();
            // getTitleRelease();
            // ProjectionAndFiltering();
            // ProjectionAndOrdering();

            //// Run grouping and aggregation together

            // Grouping();
            // Aggregation();

            Joining();
            SubqueryAndAggregation();
        }
        
        static void SubqueryAndAggregation()
        {
            // SELECT *
            // FROM (SELECT count(A.actorId) AS actorsNr, m.title
            //       FROM actors AS a.movies AS m
            //       WHERE a.movieId = m.movieId
            //       GROUP BY m.title) AS q
            // WHERE q.actorsNr > 3;

            using (var db = new MovieContext())
            {
                var projected_movies =  from movie in db.Movies
                                        let actors_of_movie = (
                                            from actor in db.Actors
                                            where actor.MovieId == movie.Id
                                            select actor
                                        )
                                        where actors_of_movie.Count() < 3
                                        select new {
                                            Title = movie.Title,
                                            ActorsCount = actors_of_movie.Count().ToString()
                                        };

                ////// foreach loop does not work for some odd reason
                ////// because ef design was version 2.0.0 and now it's 2.1.1

                foreach (var movie in projected_movies)
                {
                    System.Console.WriteLine("Movie: {0} | Number of actors: {1}", movie.Title, movie.ActorsCount.ToString());
                }
            }
        }
        static void Joining()
        {
            // SELECT movies.Title, actors.Name
            // FROM movies, actors
            // WHERE movies.movieId = actors.movieId

            using (var db = new MovieContext())
            {
                var projected_movies =  from movie in db.Movies
                                        from actor in db.Actors
                                        where movie.Id == actor.MovieId
                                        select new {
                                                Title = movie.Title,
                                                ActorName = actor.Name
                                        };
                
                foreach (var m in projected_movies)
                {
                    System.Console.WriteLine(" Movie: {0} |  Actor: {1} ", m.Title, m.ActorName);
                }
            }

        }
        static void Aggregation()
        {
           // SELECT gender, count(*)
           // FROM actors
           // GROUP BY gender;
           using (var db = new MovieContext())
           {
               var result = from actor in db.Actors
                            group actor by actor.Gender into genderGrp
                            select  Tuple.Create (
                                    genderGrp.Key,
                                    genderGrp.Count()
                            );
                foreach (var r in result)
                {
                    System.Console.WriteLine(r);
                }
                // Or
                Console.WriteLine("Gender | Number of actors");
                foreach (var item in result){
                        Console.WriteLine("{0} | {1}", item.Item1, item.Item2);
                }
           } 
        }
        static void Grouping()
        {
            // SELECT *
            // FROM actors
            // GROUP BY gender;
            using (var db = new MovieContext())
            {
                var projected_movies =  from a in db.Actors
                                        group a by a.Gender into genderGroup
                                        select genderGroup;

                // foreach (var a in projected_movies)
                // {
                //     System.Console.WriteLine(a.Take(2));
                // }
                foreach (var movie in projected_movies){
                    Console.WriteLine("+ {0} ", movie.Key);
                    foreach (var actor in movie){
                        Console.WriteLine("-- {0} ", actor.Name);
                    }
                }
            }
        }

        static void ProjectionAndOrdering()
        {
            // SELECT *
            // FROM movies AS m
            // WHERE release > '01-01-2000'
            // ORDER BY m.release DESC;
            using (var db = new MovieContext())
            {
                var projected_movies =  from m in db.Movies 
                                        where m.Release > new DateTime (2000, 1, 1)
                                        orderby m.Release descending
                                        select m;
            }
        }
        static void ProjectionAndFiltering()
        {
            // SELECT *
            // FROM movies AS M
            // WHERE m.Release > 01-01-2000;
            using(var db = new MovieContext())
            {
                var projected_movies =  from m in db.Movies
                                        where m.Release > new DateTime (2000, 1, 1)
                                        select m;
                
                foreach (var m in projected_movies)
                {
                    System.Console.WriteLine(m.Title);
                }
            }
        }
        static void getTitleRelease()
        {
            // SELECT Title, Release FROM movies
            using (var db = new MovieContext())
            {
                var projected_movies =  from m in db.Movies 
                                        select new {m.Title, m.Release};
                
                foreach (var m in projected_movies)
                {
                    System.Console.WriteLine("Title: " + m.Title + "\nRelease: " + m.Release);
                }
            }
        }
        static void getTitle()
        {
            // SELECT *
            // FROM movies;
            using (var db = new MovieContext())
            {
                var projected_movies = from m in db.Movies select m;

                foreach (Movie m in projected_movies)
                System.Console.WriteLine(m.Title);
            }
        }
        static void insertPrimaryData()
        {
            using (var db = new MovieContext())
            {
                Movie m = new Movie
                {
                    Title = "No country for old men",
                    Actors = new List<Actor> {
                        new Actor{Name = "Tommy Lee"},
                        new Actor{Name = "Xavier Berdem"}
                    }
                };
                db.Movies.Add(m);
                db.SaveChanges();
            }

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

            // using (var db = new MovieContext())
            // {
            //     Movie m = new Movie
            //     {
            //         Title = "2nd movie",
            //         Actors = new List<Actor> {
            //             new Actor{Name = "Tommy Lee"},
            //             new Actor{Name = "Xavier Berdem"}
            //         },
            //         Release = new DateTime (2003, 3, 5)
            //     };
            //     db.Movies.Add(m);
            //     db.SaveChanges();
            // }
        }
    }
}
