using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.ContactMessage;
using PetCareMini.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Persistence.Services;

public class ContactMessageService : IContactMessageService
{
    private readonly IContactMessageRepository _repo;

    public ContactMessageService(IContactMessageRepository repo)
    {
        _repo = repo;
    }

    public async Task SendMessageAsync(int userId, ContactMessageCreateDto dto)
    {
        var message = new ContactMessage
        {
            Subject = dto.Subject,
            Message = dto.Message,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };
        await _repo.CreateAsync(message);
    }

    public async Task<List<ContactMessageGetDto>> GetMyMessagesAsync(int userId)
    {
        var messages = await _repo.GetByUserIdAsync(userId);
        return messages.Select(MapToDto).ToList();
    }

    public async Task<List<ContactMessageGetDto>> GetAllAsync()
    {
        var messages = await _repo.GetAllAsync();
        return messages.Select(MapToDto).ToList();
    }

    public async Task<List<ContactMessageGetDto>> GetUnreadAsync()
    {
        var messages = await _repo.GetUnreadAsync();
        return messages.Select(MapToDto).ToList();
    }

    public async Task<List<ContactMessageGetDto>> GetArchivedAsync()
    {
        var messages = await _repo.GetArchivedAsync();
        return messages.Select(MapToDto).ToList();
    }

    public async Task MarkAsReadAsync(int id)
    {
        var message = await _repo.GetByIdAsync(id)
            ?? throw new Exception("Mesaj tapılmadı.");

        message.IsRead = true;
        message.ReadAt = DateTime.UtcNow;
        await _repo.UpdateAsync(message);
    }

    public async Task ReplyAsync(int id, ContactMessageReplyDto dto)
    {
        var message = await _repo.GetByIdAsync(id)
            ?? throw new Exception("Mesaj tapılmadı.");

        message.ReplyMessage = dto.ReplyMessage;
        message.RepliedAt = DateTime.UtcNow;
        message.IsRead = true;
        message.ReadAt ??= DateTime.UtcNow;
        await _repo.UpdateAsync(message);
    }

    public async Task ArchiveAsync(int id)
    {
        var message = await _repo.GetByIdAsync(id)
            ?? throw new Exception("Mesaj tapılmadı.");

        message.IsArchived = true;
        await _repo.UpdateAsync(message);
    }

    public async Task DeleteAsync(int id)
    {
        var message = await _repo.GetByIdAsync(id)
            ?? throw new Exception("Mesaj tapılmadı.");

        await _repo.DeleteAsync(message);
    }

    private static ContactMessageGetDto MapToDto(ContactMessage m) => new()
    {
        Id = m.Id,
        Subject = m.Subject,
        Message = m.Message,
        IsRead = m.IsRead,
        IsArchived = m.IsArchived,
        ReadAt = m.ReadAt,
        ReplyMessage = m.ReplyMessage,
        RepliedAt = m.RepliedAt,
        CreatedAt = m.CreatedAt,
        UserFullName = $"{m.User.FullName}",
        UserEmail = m.User.Email
    };
}