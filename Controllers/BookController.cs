using Microsoft.AspNetCore.Mvc;
using WebAPIDiscussion.Database;
using WebAPIDiscussion.Models; 

namespace WebAPIDiscussion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public BooksController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetBookList")]
        public ActionResult<IEnumerable<Book>> GetBookList()
        {
            var books = _context.Books.ToList();
            return Ok(books);
        }

        [HttpPost("AddBook")]
        public async Task<ActionResult<Book>> AddBook([FromBody] Bookmodel book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nBook = new Book
            {
                Id = book.Id,
                Author = book.Author,
                Title = book.Title,
                PublisherName = book.PublisherName,
                DatePublished = book.DatePublished,
                DateAdded = book.DateAdded

            };

            _context.Books.Add(nBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookList), new { id = book.Id }, book);
        }

        [HttpPost("AddBooks")]
        public async Task<ActionResult<IEnumerable<Book>>> AddBooks([FromBody] List<Book> books)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Books.AddRange(books);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookList), books);
        }
    }
}
