using LibManage.DTOs.LendingRecord;
using LibManage.Models;
using LibManage.Repositories;
using LibManage.Repositories.Interfaces;
using LibManage.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

public class LendingRecordService : ILendingRecordService
{
    private readonly ILendingRecordRepository _repo;
    private readonly IBookRepository _bookRepo;
    private readonly IMemberRepository _memberRepo;

    public LendingRecordService(
        ILendingRecordRepository repo,
        IBookRepository bookRepo,
        IMemberRepository memberRepo)
    {
        _repo = repo;
        _bookRepo = bookRepo;
        _memberRepo = memberRepo;
    }

    public async Task<List<LendingRecordDto>> GetAllAsync()
    {
        var records = await _repo.GetAllAsync();

        return records.Select(r => new LendingRecordDto
        {
            Id = r.Id,
            BookId = r.BookId,
            BookTitle = r.Book?.Title ?? "",
            MemberId = r.MemberId,
            MemberName = r.Member?.Name ?? "",
            BorrowedDate = r.BorrowedDate,
            DueDate = r.DueDate,
            ReturnedDate = r.ReturnedDate
        }).ToList();
    }

    public async Task AddAsync(LendingRecordDto dto)
    {
        var record = new LendingRecord
        {
            BookId = dto.BookId,
            MemberId = dto.MemberId,
            BorrowedDate = DateTime.UtcNow,
            DueDate = dto.DueDate
        };

        await _repo.AddAsync(record);
    }

    public async Task MarkAsReturnedAsync(int id)
    {
        var record = await _repo.GetByIdAsync(id);
        if (record != null)
        {
            record.ReturnedDate = DateTime.UtcNow;
            await _repo.UpdateAsync(record);
        }
    }

    // ✅ New: ViewModel for Create page
    public async Task<CreateLendingRecordDto> GetCreateViewModelAsync()
    {
        var books = await _bookRepo.GetAllAsync();
        var members = await _memberRepo.GetAllAsync();

        return new CreateLendingRecordDto
        {
            Books = books.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Title }).ToList(),
            Members = members.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList(),
            BorrowedDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(14)
        };
    }

    // ✅ New: Create from ViewModel
    public async Task CreateAsync(CreateLendingRecordDto dto)
    {
        var record = new LendingRecord
        {
            BookId = dto.BookId,
            MemberId = dto.MemberId,
            BorrowedDate = dto.BorrowedDate,
            DueDate = dto.DueDate
        };

        await _repo.AddAsync(record);
    }

    public async Task<LendingRecordDto?> GetByIdAsync(int id)
    {
        var r = await _repo.GetByIdAsync(id);
        if (r == null) return null;

        return new LendingRecordDto
        {
            Id = r.Id,
            BookId = r.BookId,
            BookTitle = r.Book?.Title ?? "",
            MemberId = r.MemberId,
            MemberName = r.Member?.Name ?? "",
            BorrowedDate = r.BorrowedDate,
            DueDate = r.DueDate,
            ReturnedDate = r.ReturnedDate
        };
    }

    public async Task DeleteAsync(int id)
    {
        var record = await _repo.GetByIdAsync(id);
        if (record != null)
        {
            await _repo.DeleteAsync(record);
        }
    }

}
