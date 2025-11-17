using LibraryAPI.Data;
using LibraryAPI.DTO;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/borrow")]
    [ApiController]
    public class BorrowBookController : ControllerBase
    {
        private readonly LibraryDbContext _db;
        public BorrowBookController(LibraryDbContext db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> Borrow(BorrowRequestDTO dto)
        {
            BorrowResponseDTO response = new BorrowResponseDTO();
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var book = await _db.Books.FindAsync(dto.BookId);
                if (book == null) return NotFound("Book not found.");
                if (book.AvailableCopies <= 0)
                    return BadRequest("No copies available.");

                var member = await _db.Members.FindAsync(dto.MemberId);
                if (member == null) return NotFound("Member not found.");

                book.AvailableCopies -= 1;

                var record = new BorrowRecord()
                {
                    MemberId = member.MemberId,
                    BookId = book.BookId,
                    BorrowDate = DateOnly.FromDateTime(DateTime.Now),
                    IsReturned = "N"
                };

                _db.BorrowRecords.Add(record);
                await _db.SaveChangesAsync();

                response.MemberId = record.MemberId;
                response.BookId = record.BookId;
                response.BorrowDate = record.BorrowDate;

                return Ok(response);
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
