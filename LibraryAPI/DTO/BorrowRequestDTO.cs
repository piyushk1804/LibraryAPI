using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DTO
{
    public class BorrowRequestDTO
    {
        [Required]
        public int MemberId { get; set; }

        [Required]
        public int BookId { get; set; }
    }
}
