using LibManage.DTOs.Review;
using LibManage.Models;
using LibManage.Repositories.Interfaces;
using LibManage.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IBookService _bookService;

    public ReviewService(IReviewRepository reviewRepository, IMemberRepository memberRepository, IBookService bookService)
    {
        _reviewRepository = reviewRepository;
        _memberRepository = memberRepository; 
        _bookService = bookService;

    }

    public async Task<List<ReviewDto>> GetAllAsync()
    {
        var reviews = await _reviewRepository.GetAllAsync();

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

    public async Task AddAsync(CreateReviewDto dto, string username)
    {
        var member = await _memberRepository.GetByUsernameAsync(username);
        if (member == null)
            throw new Exception("Member not found for the current user.");

        var review = new Review
        {
            BookId = dto.BookId,
            MemberId = member.Id,
            Rating = dto.Rating,
            Comment = dto.Comment,
            CreatedDate = DateTime.UtcNow
        };

        await _reviewRepository.AddAsync(review);
    }
   

}
