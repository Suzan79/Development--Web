using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SimpleModelsAndRelations.Model
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movies{get;set;}
        public DbSet<Actor> Actors{get;set;}
        public DbSet<MovieActor> MovieActor{get; set;}
       public MovieContext(DbContextOptions<MovieContext> options) : base(options){ }

        // many-to-many relationship
       protected override void OnModelCreating(ModelBuilder mp){
         mp.Entity<MovieActor>()
             .HasKey(t => new{t.MovieId, t.ActorId});
         
         mp.Entity<MovieActor>()
            .HasOne(ma => ma.Movie)
            .WithMany(m => m.Actors)
            .HasForeignKey(ma => ma.MovieId);
        
        mp.Entity<MovieActor>()
            .HasOne(ma => ma.Actor)
            .WithMany(m => m.Movies)
            .HasForeignKey(ma => ma.ActorId);
       }

    }



public class Movie{
    public int Id{get;set;}
    public string Title{get;set;}
    public List<MovieActor> Actors{get;set;}
}

public class Actor{
    public int Id{get;set;}
    public string Name{get;set;}
    public string Gender{get;set;}
    public DateTime Birth{get;set;}
    public List<MovieActor> Movies{get;set;}
}

public class MovieActor{
    public int MovieId{get;set;}
    public Movie Movie{get;set;}
    public int ActorId{get;set;}
    public Actor Actor{get;set;}
}

}

