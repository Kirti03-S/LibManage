using LibManage.DTOs.Review;
using LibManage.Models;
using LibManage.Repositories.Interfaces;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _repo;

    public ReviewService(IReviewRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<ReviewDto>> GetAllAsync()
    {
        var reviews = await _repo.GetAllAsync();

        return reviews.Select(r => new ReviewDto
        {
            Id = r.Id,
            BookId = r.BookId,
            BookTitle = r.Book?.Title ?? "",
            MemberId = r.MemberId,
            MemberName = r.Member?.Name ?? "",
            Rating = r.Rating,
            Comment = r.Comment,
            CreatedDate = r.CreatedDate
        }).ToList();
    }

    public async Task AddAsync(ReviewDto dto)
    {
        var review = new Review
        {
            BookId = dto.BookId,
            MemberId = dto.MemberId,
            Rating = dto.Rating,
            Comment = dto.Comment,
            CreatedDate = DateTime.UtcNow
        };

        await _repo.AddAsync(review);
    }
}

