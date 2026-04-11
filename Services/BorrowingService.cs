using LibraryManagement.Models;
using LibraryManagement.Repositories;
using LibraryManagement.DTOs;

namespace LibraryManagement.Services;

public class BorrowingService : IBorrowingService
{
    private readonly IRepository<Borrowing> _borrowingRepo;
    private readonly BookRepository _bookRepo;
    private readonly IRepository<Member> _memberRepo;

    public BorrowingService(IRepository<Borrowing> borrowingRepo, BookRepository bookRepo, IRepository<Member> memberRepo)
    {
        _borrowingRepo = borrowingRepo;
        _bookRepo = bookRepo;
        _memberRepo = memberRepo;
    }

    public async Task<bool> BorrowBookAsync(int bookId, int memberId)
    {
        var book = await _bookRepo.GetByIdAsync(bookId);
        var member = await _memberRepo.GetByIdAsync(memberId);
        if (book == null || member == null || book.AvailableCopies <= 0)
            return false;

        var borrowing = new Borrowing
        {
            BookId = bookId,
            MemberId = memberId,
            BorrowDate = DateTime.UtcNow
        };
        await _borrowingRepo.AddAsync(borrowing);
        book.AvailableCopies--;
        _bookRepo.Update(book);
        await _borrowingRepo.SaveAsync();
        return true;
    }

    public async Task<bool> ReturnBookAsync(int borrowingId)
    {
        var borrowing = await _borrowingRepo.GetByIdAsync(borrowingId);
        if (borrowing == null || borrowing.ReturnDate != null)
            return false;

        borrowing.ReturnDate = DateTime.UtcNow;
        var book = await _bookRepo.GetByIdAsync(borrowing.BookId);
        if (book != null) book.AvailableCopies++;
        _borrowingRepo.Update(borrowing);
        _bookRepo.Update(book!);
        await _borrowingRepo.SaveAsync();
        return true;
    }

    public async Task<IEnumerable<BorrowDto>> GetBorrowingHistoryByMemberAsync(int memberId)
    {
        var borrowings = await _borrowingRepo.GetAllAsync();
        var filtered = borrowings.Where(b => b.MemberId == memberId);
        return filtered.Select(b => new BorrowDto
        {
            Id = b.Id,
            BookId = b.BookId,
            BookTitle = b.Book?.Title ?? string.Empty,
            MemberId = b.MemberId,
            MemberName = b.Member?.Name ?? string.Empty,
            BorrowDate = b.BorrowDate,
            ReturnDate = b.ReturnDate
        });
    }

    public async Task<IEnumerable<BorrowDto>> GetAllBorrowingsAsync()
    {
        var borrowings = await _borrowingRepo.GetAllAsync();
        return borrowings.Select(b => new BorrowDto
        {
            Id = b.Id,
            BookId = b.BookId,
            BookTitle = b.Book?.Title ?? string.Empty,
            MemberId = b.MemberId,
            MemberName = b.Member?.Name ?? string.Empty,
            BorrowDate = b.BorrowDate,
            ReturnDate = b.ReturnDate
        });
    }
}
