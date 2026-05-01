using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Application.DTOs.Faq;

public class FaqUpdateDto
{
    public string QuestionAz { get; set; } = string.Empty;
    public string QuestionEn { get; set; } = string.Empty;

    public string AnswerAz { get; set; } = string.Empty;
    public string AnswerEn { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}