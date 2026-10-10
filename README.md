# Library Management

![demo](demo.png)

A Windows Forms application for managing a library's book records. The project targets **.NET Framework 4.7.2** and stores its data in Microsoft SQL Server.

## Requirements

- Visual Studio with .NET Framework 4.7.2 targeting pack
- SQL Server or SQL Server Express
- The Entity Framework 6 NuGet package (declared in `packages.config`)

## Database setup

The application expects a database named `LibraryDB` with a `dbo.Books` table. Create the table with the following schema:

```sql
CREATE DATABASE LibraryDB;
GO

USE LibraryDB;
GO

-- 1. Publishers Table
CREATE TABLE Publishers (
    PublisherId INT IDENTITY(1,1) PRIMARY KEY,
    PublisherName NVARCHAR(255) NOT NULL
);

-- 2. Authors Table
CREATE TABLE Authors (
    AuthorId INT IDENTITY(1,1) PRIMARY KEY,
    authorName NVARCHAR(100) NOT NULL,
);

-- 3. Genres Table (Recommended to separate)
CREATE TABLE Genres (
    GenreId INT IDENTITY(1,1) PRIMARY KEY,
    GenreName NVARCHAR(100) NOT NULL
);

-- 4. Books Table
CREATE TABLE Books (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    PublisherId INT FOREIGN KEY REFERENCES Publishers(PublisherId),
    GenreId INT FOREIGN KEY REFERENCES Genres(GenreId),
    Edition NVARCHAR(50),
    Year INT,
    Price DECIMAL(10,2),
    Tags NVARCHAR(MAX), -- Can store comma-separated tags or use a separate Tag table later
    Pages INT,
    Language NVARCHAR(50)
);

-- 5. Junction Table for Books and Authors (Many-to-Many relationship)
CREATE TABLE BookAuthors (
    BookId INT FOREIGN KEY REFERENCES Books(Id) ON DELETE CASCADE,
    AuthorId INT FOREIGN KEY REFERENCES Authors(AuthorId) ON DELETE CASCADE,
    PRIMARY KEY (BookId, AuthorId)
);
```

Update the `LibraryDBEntities1` connection string in `App.config` to use the SQL Server instance available on your machine. The checked-in configuration currently points to `DESKTOP-2FRKVQK\SQLEXPRESS` and uses Windows Integrated Security.

## Build and run

1. Open `Library_Management.sln` in Visual Studio.
2. Restore the NuGet packages if prompted.
3. Confirm the database and connection string are configured.
4. Build and run the `Library_Management` project.

## User interface

The main form is laid out with fields for book title, author, publisher, edition, year, price, genre, tags, page count, and language, along with a book grid and Add, Delete, Update, and Search controls.
