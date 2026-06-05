using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.DTOs.Auth;

public class ConfirmEmailDto
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}
