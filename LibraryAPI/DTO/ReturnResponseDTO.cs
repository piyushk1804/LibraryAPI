namespace LibraryAPI.DTO
{
    public class ReturnResponseDTO
    {
        public string? IsReturned { get; set; }
        public DateOnly ReturnDate { get; set; }
        public int? AvailableCopies { get; set; }
    }
}
