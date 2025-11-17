using LibraryAPI.Data;
using LibraryAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnBookController : ControllerBase
    {
        private readonly LibraryDbContext _db;
        public ReturnBookController(LibraryDbContext db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> Return(ReturnRequestDTO dto)
        {
            ReturnResponseDTO response = new ReturnResponseDTO();
            try
            {
                var record = await _db.BorrowRecords
                    .Include(r => r.Book)
                    .FirstOrDefaultAsync(r => r.BorrowId == dto.BorrowId);

                if (record == null) return NotFound("Record not found.");
                if (record.IsReturned.Equals("Y")) return BadRequest("Already returned.");

                record.IsReturned = "Y";
                record.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
                record.Book.AvailableCopies += 1;

                await _db.SaveChangesAsync();

                response.IsReturned = record.IsReturned;
                response.ReturnDate = record.ReturnDate;
                response.AvailableCopies = record.Book.AvailableCopies;

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
