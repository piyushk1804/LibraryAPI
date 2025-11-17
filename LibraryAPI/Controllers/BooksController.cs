using LibraryAPI.Data;
using LibraryAPI.DTO;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _db;
        public BooksController (LibraryDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookDTO bookDTO)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                bool isbnExists = await _db.Books.AnyAsync(b => b.Isbn == bookDTO.ISBN);
                if (isbnExists)
                    return Conflict("ISBN already exists.");

                if (bookDTO.AvailableCopies < 0)
                    return BadRequest("AvailableCopies cannot be negative.");

                var book = new Book
                {
                    Title = bookDTO.Title,
                    Author = bookDTO.Author,
                    Isbn = bookDTO.ISBN,
                    PublishedYear = bookDTO.PublishedYear,
                    AvailableCopies = bookDTO.AvailableCopies
                };

                _db.Books.Add(book);
                await _db.SaveChangesAsync();
                return Ok(book);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new
                {
                    message = "Internal Server Error",
                    error = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await _db.Books
                    .Select(b => new
                    {
                        b.BookId,
                        b.Title,
                        b.Author,
                        b.Isbn,
                        b.PublishedYear,
                        b.AvailableCopies
                    })
                    .ToListAsync();

                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Internal Server Error",
                    error = ex.Message
                });
            }
        }
    }
}
