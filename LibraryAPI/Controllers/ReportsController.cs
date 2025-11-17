using LibraryAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly LibraryDbContext _db;
        public ReportsController(LibraryDbContext db) => _db = db;

        [HttpGet("topborrowed")]
        public async Task<IActionResult> GetTopBorrowed()
        {
            try
            {
                var result = await _db.BorrowRecords
                    .GroupBy(r => r.BookId)
                    .Select(g => new
                    {
                        BookId = g.Key,
                        TimesBorrowed = g.Count()
                    })
                    .OrderByDescending(x => x.TimesBorrowed)
                    .Take(5)
                    .Join(_db.Books, r => r.BookId, b => b.BookId, (r, b) => new
                    {
                        b.BookId,
                        b.Title,
                        b.Author,
                        r.TimesBorrowed
                    })
                    .ToListAsync();

                return Ok(result);
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

        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdue()
        {
            try
            {
                var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);

                var records = await _db.BorrowRecords
                    .Where(r => r.IsReturned == "N")
                    .Include(r => r.Member)
                    .Include(r => r.Book)
                    .ToListAsync();

                var overdue = records
                    .Where(r => r.BorrowDate.AddDays(14) < today)
                    .Select(r => new
                    {
                        r.BorrowId,
                        Member = new { r.Member.MemberId, r.Member.Name, r.Member.Email },
                        Book = new { r.Book.BookId, r.Book.Title, r.Book.Isbn },
                        r.BorrowDate,
                        DueDate = r.BorrowDate.AddDays(14)
                    })
                    .ToList();

                return Ok(overdue);
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
