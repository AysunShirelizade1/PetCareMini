using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Contact;
using PetCareMini.Persistence.Contexts;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Persistence.Services;

public class ContactInfoService : IContactInfoService
{
    private readonly AppDbContext _context;

    public ContactInfoService(AppDbContext context)
        => _context = context;

    public async Task<ContactInfoDto> GetAsync()
    {
        var info = await _context.ContactInfos.FirstOrDefaultAsync()
            ?? throw new KeyNotFoundException("Contact info tapılmadı.");

        return new ContactInfoDto
        {
            PhoneNumber = info.PhoneNumber,
            Email = info.Email,
            Address = info.Address,
            WorkingHours = info.WorkingHours,
            FacebookUrl = info.FacebookUrl,
            InstagramUrl = info.InstagramUrl,
            LinkedInUrl = info.LinkedInUrl,
            TiktokUrl = info.TiktokUrl,
            TwitterUrl = info.TwitterUrl,
            WhatsappUrl = info.WhatsappUrl,
            YoutubeUrl = info.YoutubeUrl
        };
    }

    public async Task UpdateAsync(ContactInfoDto dto)
    {
        var info = await _context.ContactInfos.FirstOrDefaultAsync()
            ?? throw new KeyNotFoundException("Contact info tapılmadı.");

        info.PhoneNumber = dto.PhoneNumber;
        info.Email = dto.Email;
        info.Address = dto.Address;
        info.WorkingHours = dto.WorkingHours;
        info.FacebookUrl = dto.FacebookUrl;
        info.InstagramUrl = dto.InstagramUrl;
        info.LinkedInUrl = dto.LinkedInUrl;
        info.TiktokUrl = dto.TiktokUrl;
        info.TwitterUrl = dto.TwitterUrl;
        info.WhatsappUrl = dto.WhatsappUrl;
        info.YoutubeUrl = dto.YoutubeUrl;

        await _context.SaveChangesAsync();
    }
}
