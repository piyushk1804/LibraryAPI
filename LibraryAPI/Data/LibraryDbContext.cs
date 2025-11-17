using System;
using System.Collections.Generic;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Data;

public partial class LibraryDbContext : DbContext
{
    public LibraryDbContext()
    {
    }

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BorrowRecord> BorrowRecords { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C207B6C896D1");

            entity.Property(e => e.AvailableCopies).HasDefaultValue(0);
        });

        modelBuilder.Entity<BorrowRecord>(entity =>
        {
            entity.HasKey(e => e.BorrowId).HasName("PK__BorrowRe__4295F83FCE4DD895");

            entity.Property(e => e.IsReturned).IsFixedLength();

            entity.HasOne(d => d.Book).WithMany(p => p.BorrowRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BorrowRec__BookI__3E52440B");

            entity.HasOne(d => d.Member).WithMany(p => p.BorrowRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BorrowRec__IsRet__3D5E1FD2");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Members__0CF04B1821721749");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
