using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{
  private static List<Book> BookList = new List<Book>()
  {
    new Book{
      Id = 1,
      Title = "Lean Startup",
      GenreId = 1, // Personal Growth
      PublishDate = new DateTime(2001,05,17),
      PageCount = 200,
    },
    new Book{
      Id = 2,
      Title = "Herland",
      GenreId = 2, // Science Fiction
      PublishDate = new DateTime(2010,05,23),
      PageCount = 400,
    },
    new Book{
      Id = 3,
      Title = "Dune",
      GenreId = 2, // Science Fiction
      PublishDate = new DateTime(2001,12,21),
      PageCount = 540,
    }
  };


  [HttpGet]
  public IActionResult GetBooks()
  {
    var books = BookList.OrderBy(b => b.Id).ToList();
    return Ok(books);
  }

  [HttpGet("{id}")]
  public IActionResult GetBookById(int id)
  {
    var book = BookList.Where(b => b.Id == id).FirstOrDefault();
    return Ok(book);
  }

  [HttpPost]
  public IActionResult AddBook([FromBody] Book book)
  {
    if (BookList.Any(b => b.Id == book.Id))
      return BadRequest();

    BookList.Add(book);
    return Ok();
  }

  [HttpPut("{id}")]
  public IActionResult UpdateBook(int id, [FromBody] Book book)
  {
    var bookToUpdate = BookList.Where(b => b.Id == id).FirstOrDefault();
    if (bookToUpdate == null)
      return NotFound();

    bookToUpdate.Title = book.Title;
    bookToUpdate.GenreId = book.GenreId;
    bookToUpdate.PublishDate = book.PublishDate;
    bookToUpdate.PageCount = book.PageCount;

    return Ok(bookToUpdate);
  }

  [HttpDelete("{id}")]

  public IActionResult DeleteBook(int id)
  {
    var book = BookList.FirstOrDefault(book => book.Id == id);
    if (book is null)
      return BadRequest("book cannot found");

    BookList.Remove(book);
    return Ok();
  }
}
