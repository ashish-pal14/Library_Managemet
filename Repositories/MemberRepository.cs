using LibraryManagement.Models;
using LibraryManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repositories;

public class MemberRepository : IRepository<Member>
{
    private readonly AppDbContext _context;
    public MemberRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Member>> GetAllAsync() => await _context.Members.ToListAsync();
    public async Task<Member?> GetByIdAsync(int id) => await _context.Members.FindAsync(id);
    public async Task AddAsync(Member entity) => await _context.Members.AddAsync(entity);
    public void Update(Member entity) => _context.Members.Update(entity);
    public void Delete(Member entity) => _context.Members.Remove(entity);
    public async Task SaveAsync() => await _context.SaveChangesAsync();

    public async Task<Member?> GetByEmailAsync(string email) =>
        await _context.Members.FirstOrDefaultAsync(m => m.Email == email);
}
