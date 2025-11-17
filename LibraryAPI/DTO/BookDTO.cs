using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DTO
{
    public class BookDTO
    {
        [Required]
        public string Title { get; set; }
        public string? Author { get; set; }

        [Required]
        public string ISBN { get; set; }
        public int PublishedYear { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Copies cannot be negative.")]
        public int AvailableCopies { get; set; }
    }
}
