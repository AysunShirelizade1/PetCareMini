using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Faq;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Services;

public class FaqService : IFaqService
{
    private readonly IFaqRepository _faqRepository;

    public FaqService(IFaqRepository faqRepository)
    {
        _faqRepository = faqRepository;
    }

    public async Task<List<FaqGetDto>> GetAllAsync(string lang)
    {
        var faqs = await _faqRepository.GetAllAsync();

        return faqs.Select(x => MapToDto(x, lang)).ToList();
    }

    public async Task<FaqGetDto?> GetByIdAsync(int id, string lang)
    {
        var faq = await _faqRepository.GetByIdAsync(id);

        if (faq is null)
            return null;

        return MapToDto(faq, lang);
    }

    public async Task CreateAsync(FaqCreateDto dto)
    {
        var faq = new Faq
        {
            QuestionAz = dto.QuestionAz,
            QuestionEn = dto.QuestionEn,
            AnswerAz = dto.AnswerAz,
            AnswerEn = dto.AnswerEn,
            IsActive = true
        };

        await _faqRepository.AddAsync(faq);
        await _faqRepository.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(int id, FaqUpdateDto dto)
    {
        var faq = await _faqRepository.GetByIdAsync(id);

        if (faq is null)
            return false;

        faq.QuestionAz = dto.QuestionAz;
        faq.QuestionEn = dto.QuestionEn;
        faq.AnswerAz = dto.AnswerAz;
        faq.AnswerEn = dto.AnswerEn;
        faq.IsActive = dto.IsActive;

        _faqRepository.Update(faq);
        await _faqRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var faq = await _faqRepository.GetByIdAsync(id);

        if (faq is null)
            return false;

        _faqRepository.Delete(faq);
        await _faqRepository.SaveChangesAsync();

        return true;
    }

    private FaqGetDto MapToDto(Faq faq, string lang)
    {
        var isEn = lang.ToLower() == "en";

        return new FaqGetDto
        {
            Id = faq.Id,
            Question = isEn ? faq.QuestionEn : faq.QuestionAz,
            Answer = isEn ? faq.AnswerEn : faq.AnswerAz
        };
    }
}