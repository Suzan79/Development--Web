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
    //DONE 1: missing code 0.5pt 
    public DbSet<BookAuthor> BookAuthor { get; set; }
    /////

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<BookAuthor>()
      .HasKey(t => new { t.AuthorId, t.BookId });

      //DONE 2: missing code 2pt
      modelBuilder.Entity<BookAuthor>()
      .HasOne(ba => ba.Book)
      .WithMany(b => b.Authors)
      .HasForeignKey(ba => ba.BookId);

      modelBuilder.Entity<BookAuthor>()
      .HasOne(ba => ba.Author)
      .WithMany(a => a.Books)
      .HasForeignKey(ba => ba.AuthorId);
      /////
    }

  }

  public class Book
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Year { get; set; }
    public List<BookAuthor> Authors { get; set; }
  }

  //DONE 3: missing class 1pt
  public class BookAuthor
  {
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
  }
  /////


  public class Author
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Birth { get; set; }
    public string Gender { get; set; }
    //DONE 4: missing code 0.5pt
    public List<BookAuthor> Books { get; set; }
    /////

  }
}



