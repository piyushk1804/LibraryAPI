using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Models;

public partial class Member
{
    [Key]
    public int MemberId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Phone { get; set; }

    public DateOnly JoinDate { get; set; }

    [InverseProperty("Member")]
    public virtual ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
}
