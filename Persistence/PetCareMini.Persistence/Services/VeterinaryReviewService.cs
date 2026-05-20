using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.VeterinaryReview;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Services;

public class VeterinaryReviewService : IVeterinaryReviewService
{
    private readonly IVeterinaryReviewRepository _repo;

    public VeterinaryReviewService(IVeterinaryReviewRepository repo)
    {
        _repo = repo;
    }

    public async Task<VeterinaryReviewGetDto> CreateAsync(int userId, VeterinaryReviewCreateDto dto)
    {
        if (dto.Rating < 1 || dto.Rating > 5)
            throw new ArgumentException("Rating 1 ilə 5 arasında olmalıdır.");

        // Eyni appointment üçün ikinci dəfə rəy yazmağın qarşısını al
        var alreadyReviewed = await _repo.ExistsByAppointmentAsync(dto.AppointmentId);
        if (alreadyReviewed)
            throw new InvalidOperationException("Bu görüş üçün artıq rəy yazılıb.");

        var review = new VeterinaryReview
        {
            UserId = userId,
            VeterinarianId = dto.VeterinarianId,
            ServiceId = dto.ServiceId,
            AppointmentId = dto.AppointmentId,
            Rating = dto.Rating,
            CommentAz = dto.CommentAz,
            CommentEn = dto.CommentEn,
            IsApproved = false, // Admin təsdiq etməlidir
            IsFeatured = false,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(review);
        await _repo.SaveAsync();

        return Map(review, "az");
    }
    public async Task<IEnumerable<VeterinaryReviewGetDto>> GetAllAsync(int page, int pageSize)
    {
        var reviews = await _repo.GetAllAsync(page, pageSize);
        return reviews.Select(r => Map(r, "az"));
    }
    public async Task<IEnumerable<VeterinaryReviewGetDto>> GetByVeterinarianAsync(int vetId, string lang = "az")
    {
        var reviews = await _repo.GetByVeterinarianAsync(vetId);
        return reviews.Select(r => Map(r, lang));
    }

    public async Task<IEnumerable<VeterinaryReviewGetDto>> GetFeaturedAsync()
    {
        var reviews = await _repo.GetFeaturedAsync();
        return reviews.Select(r => Map(r, "az"));
    }

    public async Task<IEnumerable<VeterinaryReviewGetDto>> GetAllApprovedAsync()
    {
        var reviews = await _repo.GetAllApprovedAsync();
        return reviews.Select(r => Map(r, "az"));
    }

    public async Task ApproveAsync(int reviewId)
    {
        var review = await _repo.GetByIdAsync(reviewId)
            ?? throw new KeyNotFoundException($"{reviewId} ID-li rəy tapılmadı.");

        review.IsApproved = true;
        await _repo.SaveAsync();
    }

    public async Task SetFeaturedAsync(int reviewId, bool isFeatured)
    {
        var review = await _repo.GetByIdAsync(reviewId)
            ?? throw new KeyNotFoundException($"{reviewId} ID-li rəy tapılmadı.");

        review.IsFeatured = isFeatured;
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int reviewId)
    {
        var review = await _repo.GetByIdAsync(reviewId)
            ?? throw new KeyNotFoundException($"{reviewId} ID-li rəy tapılmadı.");

        await _repo.DeleteAsync(review);
        await _repo.SaveAsync();
    }

    private static VeterinaryReviewGetDto Map(VeterinaryReview r, string lang = "az")
    {
        return new VeterinaryReviewGetDto
        {
            Id = r.Id,
            UserFullName = r.User?.FullName ?? string.Empty,
            VeterinarianName = r.Veterinarian?.FullName ?? string.Empty,
            ServiceName = lang == "az" ? r.Service?.NameAz : r.Service?.NameEn,
            Rating = r.Rating,
            CommentAz = r.CommentAz,
            CommentEn = r.CommentEn,
            IsFeatured = r.IsFeatured,
            CreatedAt = r.CreatedAt
        };
    }
}