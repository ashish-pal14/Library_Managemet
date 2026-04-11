using LibraryManagement.Models;
using LibraryManagement.Repositories;
using LibraryManagement.DTOs;

namespace LibraryManagement.Services;

public class MemberService : IMemberService
{
    private readonly IRepository<Member> _memberRepo;
    public MemberService(IRepository<Member> memberRepo) => _memberRepo = memberRepo;

    public async Task<IEnumerable<MemberDto>> GetAllMembersAsync()
    {
        var members = await _memberRepo.GetAllAsync();
        return members.Select(m => new MemberDto
        {
            Id = m.Id,
            Name = m.Name,
            Email = m.Email,
            Phone = m.Phone,
            MembershipDate = m.MembershipDate
        });
    }

    public async Task<MemberDto?> GetMemberByIdAsync(int id)
    {
        var member = await _memberRepo.GetByIdAsync(id);
        if (member == null) return null;
        return new MemberDto
        {
            Id = member.Id,
            Name = member.Name,
            Email = member.Email,
            Phone = member.Phone,
            MembershipDate = member.MembershipDate
        };
    }

    public async Task<MemberDto> AddMemberAsync(CreateMemberDto dto)
    {
        var member = new Member
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            MembershipDate = DateTime.UtcNow
        };
        await _memberRepo.AddAsync(member);
        await _memberRepo.SaveAsync();
        return new MemberDto
        {
            Id = member.Id,
            Name = member.Name,
            Email = member.Email,
            Phone = member.Phone,
            MembershipDate = member.MembershipDate
        };
    }

    public async Task<bool> UpdateMemberAsync(int id, UpdateMemberDto dto)
    {
        var member = await _memberRepo.GetByIdAsync(id);
        if (member == null) return false;
        member.Name = dto.Name;
        member.Email = dto.Email;
        member.Phone = dto.Phone;
        _memberRepo.Update(member);
        await _memberRepo.SaveAsync();
        return true;
    }

    public async Task<bool> DeleteMemberAsync(int id)
    {
        var member = await _memberRepo.GetByIdAsync(id);
        if (member == null) return false;
        _memberRepo.Delete(member);
        await _memberRepo.SaveAsync();
        return true;
    }
}
