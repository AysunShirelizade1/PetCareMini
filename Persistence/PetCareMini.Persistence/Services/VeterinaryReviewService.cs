using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.VeterinaryReview;
using PetCareMini.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Persistence.Services;

public class VeterinaryReviewService : IVeterinaryReviewService
{
    private readonly IVeterinaryReviewRepository _repo;

    public VeterinaryReviewService(IVeterinaryReviewRepository repo)
        => _repo = repo;

    public async Task CreateAsync(int userId, VeterinaryReviewCreateDto dto)
    {
        var review = new VeterinaryReview
        {
            UserId = userId,
            VeterinarianId = dto.VeterinarianId,
            ServiceId = dto.ServiceId,
            AppointmentId = dto.AppointmentId,
            Rating = dto.Rating,
            Comment = dto.Comment,
            IsApproved = false,
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
        => (await _repo.GetFeaturedAsync()).Select(r => Map(r, "az"));

    public async Task<IEnumerable<VeterinaryReviewGetDto>> GetAllApprovedAsync()
        => (await _repo.GetAllApprovedAsync()).Select(r => Map(r, "az"));

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

    private static VeterinaryReviewGetDto Map(VeterinaryReview r, string lang = "az") => new()
    {
        Id = r.Id,
        UserFullName = r.User.FullName,
        VeterinarianName = r.Veterinarian.FullName,
        ServiceName = lang == "az" ? r.Service?.NameAz : r.Service?.NameEn,
        Rating = r.Rating,
        Comment = r.Comment,
        IsFeatured = r.IsFeatured,
        CreatedAt = r.CreatedAt
    };
}