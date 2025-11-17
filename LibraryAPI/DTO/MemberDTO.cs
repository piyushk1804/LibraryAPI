using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DTO
{
    public class MemberDTO
    {
        [Required]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateOnly JoinDate { get; set; }
    }
}
