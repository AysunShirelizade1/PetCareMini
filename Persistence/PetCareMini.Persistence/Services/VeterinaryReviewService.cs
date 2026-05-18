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

    public async Task CreateAsync(int userId, VeterinaryReviewCreateDto dto)
    {
        if (dto.Rating < 1 || dto.Rating > 5)
            throw new Exception("Rating must be between 1 and 5");

        var review = new VeterinaryReview
        {
            UserId = userId,
            VeterinarianId = dto.VeterinarianId,

            ServiceId = dto.ServiceId,
            AppointmentId = dto.AppointmentId,

            Rating = dto.Rating,
            Comment = dto.Comment,

            // frontend görsün deyə
            IsApproved = true,
            IsFeatured = false
        };

        await _repo.AddAsync(review);
        await _repo.SaveAsync();
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
            ?? throw new Exception("Review not found");

        review.IsApproved = true;

        await _repo.SaveAsync();
    }

    public async Task SetFeaturedAsync(int reviewId, bool isFeatured)
    {
        var review = await _repo.GetByIdAsync(reviewId)
            ?? throw new Exception("Review not found");

        review.IsFeatured = isFeatured;

        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int reviewId)
    {
        var review = await _repo.GetByIdAsync(reviewId)
            ?? throw new Exception("Review not found");

        await _repo.DeleteAsync(review);

        await _repo.SaveAsync();
    }

    private static VeterinaryReviewGetDto Map(VeterinaryReview r, string lang = "az")
    {
        return new VeterinaryReviewGetDto
        {
            Id = r.Id,
            UserFullName = r.User.FullName,
            VeterinarianName = r.Veterinarian.FullName,

            ServiceName = lang == "az"
                ? r.Service?.NameAz
                : r.Service?.NameEn,

            Rating = r.Rating,
            Comment = r.Comment,
            IsFeatured = r.IsFeatured,
            CreatedAt = r.CreatedAt
        };
    }
}