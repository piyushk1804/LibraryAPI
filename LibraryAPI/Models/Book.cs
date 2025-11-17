using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Models;

[Index("Isbn", Name = "UQ__Books__447D36EA88F2731F", IsUnique = true)]
public partial class Book
{
    [Key]
    public int BookId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? Author { get; set; }

    [Column("ISBN")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Isbn { get; set; }

    public int? PublishedYear { get; set; }

    public int? AvailableCopies { get; set; }

    [InverseProperty("Book")]
    public virtual ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
}
