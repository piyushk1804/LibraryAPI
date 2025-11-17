namespace LibraryAPI.DTO
{
    public class BorrowResponseDTO
    {
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateOnly BorrowDate { get; set; }
        public string? IsReturned { get; set; }
    }
}
