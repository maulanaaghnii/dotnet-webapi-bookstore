using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.DTO{
    public class BookSummary
    {
        public int Id { get; set; }
        public string Author { get; set; } = string.Empty;
    }
}
