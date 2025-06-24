// Services/Interfaces/IMemberService.cs
using LibManage.DTOs.Member;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMemberService
{
    Task<List<MemberDto>> GetAllMembersAsync();
    Task<MemberDto?> GetMemberByIdAsync(int id);
    Task<CreateMemberDto> GetCreateMemberViewModelAsync(); // <-- ADD THIS
    Task CreateMemberAsync(CreateMemberDto dto);
    Task UpdateMemberAsync(int id, CreateMemberDto dto);
    Task DeleteMemberAsync(int id);
    Task<CreateMemberDto> PopulatePlansAsync(CreateMemberDto dto);
    Task<CreateMemberDto?> GetEditMemberDtoAsync(int id);


}
