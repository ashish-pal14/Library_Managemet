using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Services;
using LibraryManagement.DTOs;

namespace LibraryManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    public BooksController(IBookService bookService) => _bookService = bookService;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _bookService.GetAllBooksAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        return book == null ? NotFound() : Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookDto dto)
    {
        var created = await _bookService.AddBookAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateBookDto dto)
    {
        var success = await _bookService.UpdateBookAsync(id, dto);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _bookService.DeleteBookAsync(id);
        return success ? NoContent() : NotFound();
    }
}
