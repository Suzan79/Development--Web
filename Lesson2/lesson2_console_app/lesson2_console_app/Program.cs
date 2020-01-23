using System;
using lesson2_console_app.Model;
using System.Linq;
using System.Collections.Generic;

namespace lesson2_console_app
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeDb();
            Console.WriteLine("Done.");
        }
        static void InitializeDb()
        {

            using (var db = new MovieContext())
            {
                if (db.Movies.Count() == 0 && db.Actors.Count() == 0)
                {
                    Movie m1 = new Movie
                    {
                        Title = "No country for old men",
                        Release = new DateTime(2008, 2, 14),
                        Actors = new List<Actor> {
                        new Actor{
                            Name = "Tommy Lee",
                            Birth = new DateTime(1962,10,3),
                            Gender = "Male"},
                        new Actor{
                            Name = "Xavier Berdem",
                            Birth = new DateTime(1969,03,1),
                            Gender = "Male"}
                            }
                    };
                    Movie m2 = new Movie
                    {
                        Title = "Star Wars",
                        Release = new DateTime(2018, 11, 11),
                        Actors = new List<Actor> {
                        new Actor{
                            Name = "Harrison Ford",
                            Birth = new DateTime(1942,07,13),
                            Gender = "Male"}
                        }
                    };
                    Movie m3 = new Movie
                    {
                        Title = "Titanic",
                        Release = new DateTime(1998, 1, 15),
                        Actors = new List<Actor> {
                        new Actor{
                            Name = "Leonardo Di Caprio",
                            Birth = new DateTime(1974,11,11),
                            Gender = "Male"},
                        new Actor{
                            Name = "Kate Winslet",
                            Birth = new DateTime(1975,10,5),
                            Gender = "Female"}
                    }
                    };

                    db.Movies.Add(m1);
                    db.Movies.Add(m2);
                    db.Movies.Add(m3);

                    db.SaveChanges();
                }


            }

        }
    }
}
