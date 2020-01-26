using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;


namespace Model
{
  public class LibraryContext : DbContext
  {
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    //TODO 1: missing code 0.5pt 

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<BookAuthor>()
      .HasKey(t => new { t.AuthorId, t.BookId });

      //TODO 2: missing code 2pt
    }

  }

  public class Book
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Year { get; set; }
    public List<BookAuthor> Authors { get; set; }
  }

  //TODO 3: missing class 1pt

  public class Author
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Birth { get; set; }
    public string Gender { get; set; }
    //TODO 4: missing code 0.5pt



  }
}



