
CREATE TABLE BorrowRecords(
	BorrowId INT IDENTITY(1,1) PRIMARY KEY,
	MemberId INT NOT NULL,
	BookId INT NOT NULL,
	BorrowDate DATE NOT NULL,
	ReturnDate DATE NOT NULL,
	IsReturned CHAR(1) NOT NULL

	FOREIGN KEY (MemberId) REFERENCES Members(MemberId),
    FOREIGN KEY (BookId) REFERENCES Books(BookId)
)


