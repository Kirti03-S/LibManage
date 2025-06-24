using LibManage.Models;

public interface ILendingRecordRepository
{
    Task<List<LendingRecord>> GetAllAsync();
    Task<LendingRecord?> GetByIdAsync(int id);
    Task AddAsync(LendingRecord record);
    Task UpdateAsync(LendingRecord record);
    Task DeleteAsync(LendingRecord record);

}

