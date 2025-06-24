using LibManage.DTOs.Review;

public interface IReviewService
{
    Task<List<ReviewDto>> GetAllAsync();
    Task AddAsync(ReviewDto dto);
}
