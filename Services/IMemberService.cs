using LibraryManagement.DTOs;

namespace LibraryManagement.Services;

public interface IMemberService
{
    Task<IEnumerable<MemberDto>> GetAllMembersAsync();
    Task<MemberDto?> GetMemberByIdAsync(int id);
    Task<MemberDto> AddMemberAsync(CreateMemberDto dto);
    Task<bool> UpdateMemberAsync(int id, UpdateMemberDto dto);
    Task<bool> DeleteMemberAsync(int id);
}
