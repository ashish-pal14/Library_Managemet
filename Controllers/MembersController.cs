using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Services;
using LibraryManagement.DTOs;

namespace LibraryManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;
    public MembersController(IMemberService memberService) => _memberService = memberService;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _memberService.GetAllMembersAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var member = await _memberService.GetMemberByIdAsync(id);
        return member == null ? NotFound() : Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMemberDto dto)
    {
        var created = await _memberService.AddMemberAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateMemberDto dto)
    {
        var success = await _memberService.UpdateMemberAsync(id, dto);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _memberService.DeleteMemberAsync(id);
        return success ? NoContent() : NotFound();
    }
}
