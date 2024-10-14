using BookStore.Data;
using BookStore.Models;
using BookStore.DTO;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BookRepository
    {
        private readonly BookContext _context;

        public BookRepository(BookContext context)
        {
            _context = context;
        }

        // GET all books using raw SQL
        public async Task<List<BookSummary>> GetAllBooksAsync()
        {
            return await _context.Books
                .FromSqlRaw("SELECT id, author FROM Books")
                                .Select(b => new BookSummary
                {
                    Id = b.Id,
                    Author = b.Author
                })
                .ToListAsync();
        }

        // GET book by ID using raw SQL
        public async Task<Book?> GetBookByIdAsync(int id)
        {
            var books = await _context.Books
                .FromSqlRaw("SELECT * FROM Books WHERE Id = {0}", id)
                .ToListAsync();
            
            return books.FirstOrDefault();
        }

        // Add a new book using raw SQL
        public async Task AddBookAsync(Book book)
        {
            await _context.Database
                .ExecuteSqlRawAsync("INSERT INTO Books (Title, Author, Price) VALUES ({0}, {1}, {2})", 
                                    book.Title, book.Author, book.Price);
        }

        // Update an existing book using raw SQL
        public async Task UpdateBookAsync(Book book)
        {
            await _context.Database
                .ExecuteSqlRawAsync("UPDATE Books SET Title = {0}, Author = {1}, Price = {2} WHERE Id = {3}", 
                                    book.Title, book.Author, book.Price, book.Id);
        }

        // Delete a book by ID using raw SQL
        public async Task DeleteBookAsync(int id)
        {
            await _context.Database
                .ExecuteSqlRawAsync("DELETE FROM Books WHERE Id = {0}", id);
        }
    }
}
