using LibraryManagement.Models;
using LibraryManagement.Repositories;
using LibraryManagement.DTOs;

namespace LibraryManagement.Services;
public class BookService : IBookService
{
    private readonly IRepository<Book> _bookRepo;
    public BookService(IRepository<Book> bookRepo) => _bookRepo = bookRepo;

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
    {
        var books = await _bookRepo.GetAllAsync();
        return books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author,
            ISBN = b.ISBN,
            AvailableCopies = b.AvailableCopies,
            TotalCopies = b.TotalCopies
        });
    }

    public async Task<BookDto?> GetBookByIdAsync(int id)
    {
        var book = await _bookRepo.GetByIdAsync(id);
        if (book == null) return null;
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            AvailableCopies = book.AvailableCopies,
            TotalCopies = book.TotalCopies
        };
    }

    public async Task<BookDto> AddBookAsync(CreateBookDto dto)
    {
        var book = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            ISBN = dto.ISBN,
            TotalCopies = dto.TotalCopies,
            AvailableCopies = dto.TotalCopies  // initially all copies available
        };
        await _bookRepo.AddAsync(book);
        await _bookRepo.SaveAsync();
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            AvailableCopies = book.AvailableCopies,
            TotalCopies = book.TotalCopies
        };
    }

public async Task<bool> UpdateBookAsync(int id, UpdateBookDto dto)
{
    var book = await _bookRepo.GetByIdAsync(id);
    if (book == null) return false;

    book.Title = dto.Title;
    book.Author = dto.Author;
    int oldTotal = book.TotalCopies;
    book.TotalCopies = dto.TotalCopies;

    if (dto.TotalCopies > oldTotal)
        book.AvailableCopies += (dto.TotalCopies - oldTotal);
    else if (dto.TotalCopies < oldTotal)
        book.AvailableCopies -= (oldTotal - dto.TotalCopies);

    _bookRepo.Update(book);
    await _bookRepo.SaveAsync();
    return true;
}

public async Task<bool> DeleteBookAsync(int id)
{
    var book = await _bookRepo.GetByIdAsync(id);
    if (book == null) return false;

    _bookRepo.Delete(book);
    await _bookRepo.SaveAsync();
    return true;
}
}
