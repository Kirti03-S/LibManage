using LibManage.DTOs.LendingRecord;

public interface ILendingRecordService
{
    Task<List<LendingRecordDto>> GetAllAsync();
    Task<LendingRecordDto?> GetByIdAsync(int id);
    Task AddAsync(LendingRecordDto dto);
    Task DeleteAsync(int id);
    Task MarkAsReturnedAsync(int id);

    // New additions for Create View
    Task<CreateLendingRecordDto> GetCreateViewModelAsync();
    Task CreateAsync(CreateLendingRecordDto dto);

}
