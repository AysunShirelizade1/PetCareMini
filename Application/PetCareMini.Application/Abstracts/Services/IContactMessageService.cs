using PetCareMini.Application.DTOs.ContactMessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.Abstracts.Services;

public interface IContactMessageService
{
    // User
    Task SendMessageAsync(int userId, ContactMessageCreateDto dto);
    Task<List<ContactMessageGetDto>> GetMyMessagesAsync(int userId);

    // Admin
    Task<List<ContactMessageGetDto>> GetAllAsync();
    Task<List<ContactMessageGetDto>> GetUnreadAsync();
    Task<List<ContactMessageGetDto>> GetArchivedAsync();
    Task MarkAsReadAsync(int id);
    Task ReplyAsync(int id, ContactMessageReplyDto dto);
    Task ArchiveAsync(int id);
    Task DeleteAsync(int id);
}
