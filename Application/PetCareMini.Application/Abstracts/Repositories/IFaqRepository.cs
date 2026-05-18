using PetCareMini.Domain.Entities;

namespace PetCareMini.Application.Abstracts.Repositories;

public interface IFaqRepository
{
    Task<List<Faq>> GetAllAsync();

    Task<Faq?> GetByIdAsync(int id);

    Task AddAsync(Faq faq);

    void Update(Faq faq);

    void Delete(Faq faq);

    Task SaveChangesAsync();
}