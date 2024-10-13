using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DBOperations
{
  public class DataGenerator
  {
    public static void Initialize(IServiceProvider serviceProvider)
    {
      using (var context = new BookStoreDbContext(
      serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
      {
        // Look for any book.
        if (context.Books.Any())
        {
          return;   // Data was already seeded
        }

        context.Books.AddRange(
           new Book
           {
             Id = 1,
             Title = "Lean Startup",
             GenreId = 1, // Personal Growth
             PublishDate = new DateTime(2001, 05, 17),
             PageCount = 200,
           },
            new Book
            {
              Id = 2,
              Title = "Herland",
              GenreId = 2, // Science Fiction
              PublishDate = new DateTime(2010, 05, 23),
              PageCount = 400,
            },
            new Book
            {
              Id = 3,
              Title = "Dune",
              GenreId = 2, // Science Fiction
              PublishDate = new DateTime(2001, 12, 21),
              PageCount = 540,
            });

        context.SaveChanges();
      }
    }
  }
}