using PetCareMini.Domain.Common;

namespace PetCareMini.Domain.Entities;

public class Faq : BaseEntity
{
    public string QuestionAz { get; set; } = string.Empty;
    public string QuestionEn { get; set; } = string.Empty;

    public string AnswerAz { get; set; } = string.Empty;
    public string AnswerEn { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}