using LibraryManagement.Models;
using LibraryManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repositories;

public class BorrowingRepository : IRepository<Borrowing>
{
    private readonly AppDbContext _context;
    public BorrowingRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Borrowing>> GetAllAsync() => 
        await _context.Borrowings.Include(b => b.Book).Include(b => b.Member).ToListAsync();

    public async Task<Borrowing?> GetByIdAsync(int id) =>
        await _context.Borrowings.Include(b => b.Book).Include(b => b.Member)
            .FirstOrDefaultAsync(b => b.Id == id);

    public async Task AddAsync(Borrowing entity) => await _context.Borrowings.AddAsync(entity);
    public void Update(Borrowing entity) => _context.Borrowings.Update(entity);
    public void Delete(Borrowing entity) => _context.Borrowings.Remove(entity);
    public async Task SaveAsync() => await _context.SaveChangesAsync();

    public async Task<bool> IsBookBorrowedAsync(int bookId) =>
        await _context.Borrowings.AnyAsync(b => b.BookId == bookId && b.ReturnDate == null);

    public async Task<IEnumerable<Borrowing>> GetActiveBorrowingsByMemberAsync(int memberId) =>
        await _context.Borrowings
            .Where(b => b.MemberId == memberId && b.ReturnDate == null)
            .Include(b => b.Book)
            .ToListAsync();
}
