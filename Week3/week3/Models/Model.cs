using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace week3.Models
{
  public class MovieContext : DbContext
  {
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Actor> Actors { get; set; }

    // added constructor
    public MovieContext(DbContextOptions<MovieContext> options) : base(options)
    {
    }
    //this method is not needed with api, it is in Startup.cs now
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
    //   //here we define the name of our database
    //   optionsBuilder.UseNpgsql("UserID=postgres;Password=postgres;Host=localhost;Port=5432;Database=MoviesDB;Pooling=true;");
    // }
  }

  //this is the typed representation of a movie in our project
  public class Movie
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Release { get; set; }
    public List<Actor> Actors { get; set; }
  }
 
  //this is the typed representation of an actor in our project
  public class Actor
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Birth { get; set; }
    public string Gender { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
  }
}