using LibraryManagement.DTOs;

namespace LibraryManagement.Services;

public interface IBorrowingService
{
    Task<bool> BorrowBookAsync(int bookId, int memberId);
    Task<bool> ReturnBookAsync(int borrowingId);
    Task<IEnumerable<BorrowDto>> GetBorrowingHistoryByMemberAsync(int memberId);
    Task<IEnumerable<BorrowDto>> GetAllBorrowingsAsync();
}
