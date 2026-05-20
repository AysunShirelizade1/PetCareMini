using PetCareMini.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.Abstracts.Repositories;

public interface IVeterinaryReviewRepository
{
    Task<IEnumerable<VeterinaryReview>> GetByVeterinarianAsync(int vetId, string lang = "az");
    Task<bool> ExistsByAppointmentAsync(int? appointmentId);
    Task<IEnumerable<VeterinaryReview>> GetAllAsync(int page, int pageSize);
    Task<IEnumerable<VeterinaryReview>> GetFeaturedAsync();       // Homepage
    Task<IEnumerable<VeterinaryReview>> GetAllApprovedAsync();    // About Us
    Task AddAsync(VeterinaryReview review);

    Task<VeterinaryReview?> GetByIdAsync(int id);
    Task DeleteAsync(VeterinaryReview review);
    Task SaveAsync();
}