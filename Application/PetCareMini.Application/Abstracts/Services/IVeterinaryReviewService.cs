using PetCareMini.Application.DTOs.VeterinaryReview;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.Abstracts.Services;

public interface IVeterinaryReviewService
{
    Task<VeterinaryReviewGetDto> CreateAsync(int userId, VeterinaryReviewCreateDto dto);
    Task<IEnumerable<VeterinaryReviewGetDto>> GetAllAsync(int page, int pageSize);
    Task<IEnumerable<VeterinaryReviewGetDto>> GetByVeterinarianAsync(int vetId, string lang = "az");
    Task<IEnumerable<VeterinaryReviewGetDto>> GetFeaturedAsync();
    Task<IEnumerable<VeterinaryReviewGetDto>> GetAllApprovedAsync();
    Task ApproveAsync(int reviewId);
    Task SetFeaturedAsync(int reviewId, bool isFeatured);
    Task DeleteAsync(int reviewId);
}