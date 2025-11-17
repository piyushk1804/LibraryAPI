CREATE TABLE Books (
    BookId INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Author VARCHAR(255),
    ISBN VARCHAR(20) UNIQUE,
    PublishedYear INT,
    AvailableCopies INT DEFAULT 0
)

