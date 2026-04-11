using LibraryManagement.DTOs;

namespace LibraryManagement.Services;
public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllBooksAsync();
    Task<BookDto?> GetBookByIdAsync(int id);
    Task<BookDto> AddBookAsync(CreateBookDto dto);
    Task<bool> UpdateBookAsync(int id, UpdateBookDto dto);
    Task<bool> DeleteBookAsync(int id);
}
