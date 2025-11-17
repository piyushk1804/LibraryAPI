using LibraryAPI.Data;
using LibraryAPI.DTO;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/members")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly LibraryDbContext _db;
        public MembersController(LibraryDbContext db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> AddMember(MemberDTO dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var member = new Member
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    JoinDate = DateOnly.FromDateTime(DateTime.Now)
                };

                _db.Members.Add(member);
                await _db.SaveChangesAsync();
                return Ok(member);
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
