using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Services;

namespace LibraryManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BorrowingsController : ControllerBase
{
    private readonly IBorrowingService _borrowingService;
    public BorrowingsController(IBorrowingService borrowingService) => _borrowingService = borrowingService;

    [HttpPost("borrow")]
    public async Task<IActionResult> BorrowBook(int bookId, int memberId)
    {
        var result = await _borrowingService.BorrowBookAsync(bookId, memberId);
        return result ? Ok() : BadRequest("Book not available or invalid data");
    }

    [HttpPost("return/{borrowingId}")]
    public async Task<IActionResult> ReturnBook(int borrowingId)
    {
        var result = await _borrowingService.ReturnBookAsync(borrowingId);
        return result ? Ok() : NotFound();
    }

    [HttpGet("history/{memberId}")]
    public async Task<IActionResult> GetMemberHistory(int memberId) =>
        Ok(await _borrowingService.GetBorrowingHistoryByMemberAsync(memberId));

    [HttpGet]
    public async Task<IActionResult> GetAllBorrowings()
    {
        var borrowings = await _borrowingService.GetAllBorrowingsAsync();
        return Ok(borrowings);
    }

}
