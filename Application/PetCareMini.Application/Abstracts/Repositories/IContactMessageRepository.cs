using PetCareMini.Domain.Entities;
namespace PetCareMini.Application.Abstracts.Repositories;

public interface IContactMessageRepository
{
    Task<List<ContactMessage>> GetAllAsync();
    Task<ContactMessage?> GetByIdAsync(int id);
    void Update(ContactMessage contactMessage);
    void Delete(ContactMessage contactMessage);
    Task AddAsync(ContactMessage contactMessage);
    Task SaveChangesAsync();
}
