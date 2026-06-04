using PetCareMini.Application.DTOs.Contact;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.Abstracts.Services;

public interface IContactInfoService
{
    Task<ContactInfoDto> GetAsync();
    Task UpdateAsync(ContactInfoDto dto);
}