Create Procedure GetOverdueBooks
As
Begin
    Select 
        br.BorrowId,
        m.Name As MemberName,
        b.Title As BookTitle,
        br.BorrowDate,
        DateAdd(Day, 14, br.BorrowDate) As DueDate
    From BorrowRecords br
    Join Members m On br.MemberId = m.MemberId
    Join Books b On br.BookId = b.BookId
    Where br.IsReturned = 'N'
      AND DateAdd(Day, 14, br.BorrowDate) < Cast(GetDAte() As Date)
END


--exec GetOverdueBooks
