using LibraryManagement.Models;
using LibraryManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repositories;

public class BookRepository : IRepository<Book>
{
    private readonly AppDbContext _context;
    public BookRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Book>> GetAllAsync() => await _context.Books.ToListAsync();
    public async Task<Book?> GetByIdAsync(int id) => await _context.Books.FindAsync(id);
    public async Task AddAsync(Book entity) => await _context.Books.AddAsync(entity);
    public void Update(Book entity) => _context.Books.Update(entity);
    public void Delete(Book entity) => _context.Books.Remove(entity);
    public async Task SaveAsync() => await _context.SaveChangesAsync();

    // Additional method for business logic
    public async Task<Book?> GetByISBNAsync(string isbn) =>
        await _context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
}
