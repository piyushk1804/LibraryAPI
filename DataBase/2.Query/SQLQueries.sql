
--List the top 5 most borrowed books.
Select Top 5
    b.BookId,
    b.Title,
    b.Author,
    Count(br.BorrowId) As TimesBorrowed
From BorrowRecords br
Join Books b On br.BookId = b.BookId
Group By b.BookId, b.Title, b.Author
Order By TimesBorrowed desc

--Find members who have borrowed more than 3 books in the last month.
Select 
    m.MemberId,
    m.Name,
    Count(br.BorrowId) As BorrowCount
From BorrowRecords br
Join Members m On br.MemberId = m.MemberId
Where br.BorrowDate >= DateAdd(Month, -1, GetDate())
Group By m.MemberId, m.Name
Having Count(br.BorrowId) > 3





