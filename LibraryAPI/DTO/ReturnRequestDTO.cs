using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DTO
{
    public class ReturnRequestDTO
    {
        [Required]
        public int BorrowId { get; set; }
    }
}
