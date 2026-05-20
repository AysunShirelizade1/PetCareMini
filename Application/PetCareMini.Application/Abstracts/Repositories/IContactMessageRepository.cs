using PetCareMini.Domain.Entities;
namespace PetCareMini.Application.Abstracts.Repositories;

public interface IContactMessageRepository
{
    Task<ContactMessage> CreateAsync(ContactMessage message);
    Task<ContactMessage?> GetByIdAsync(int id);
    Task<List<ContactMessage>> GetAllAsync();
    Task<List<ContactMessage>> GetByUserIdAsync(int userId);
    Task<List<ContactMessage>> GetUnreadAsync();
    Task<List<ContactMessage>> GetArchivedAsync();
    Task UpdateAsync(ContactMessage message);
    Task DeleteAsync(ContactMessage message);
}
