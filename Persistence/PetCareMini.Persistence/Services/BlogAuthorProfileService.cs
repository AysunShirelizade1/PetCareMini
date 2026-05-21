using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Blog;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Services;

public class BlogAuthorProfileService : IBlogAuthorProfileService
{
    private readonly IBlogAuthorProfileRepository _repo;

    public BlogAuthorProfileService(IBlogAuthorProfileRepository repo)
    {
        _repo = repo;
    }

    public async Task<BlogAuthorProfileDto?> GetByUserIdAsync(int userId)
    {
        var profile = await _repo.GetByUserIdAsync(userId);
        if (profile is null) return null;

        return new BlogAuthorProfileDto
        {
            Bio = profile.Bio,
            ProfileImageUrl = profile.ProfileImageUrl,
            WebsiteUrl = profile.WebsiteUrl,
            InstagramUrl = profile.InstagramUrl,
            LinkedInUrl = profile.LinkedInUrl
        };
    }

    public async Task CreateOrUpdateAsync(int userId, BlogAuthorProfileDto dto)
    {
        var profile = await _repo.GetByUserIdAsync(userId);

        if (profile is null)
        {
            await _repo.AddAsync(new BlogAuthorProfile
            {
                UserId = userId,
                Bio = dto.Bio,
                ProfileImageUrl = dto.ProfileImageUrl,
                WebsiteUrl = dto.WebsiteUrl,
                InstagramUrl = dto.InstagramUrl,
                LinkedInUrl = dto.LinkedInUrl
            });
        }
        else
        {
            profile.Bio = dto.Bio;
            profile.ProfileImageUrl = dto.ProfileImageUrl;
            profile.WebsiteUrl = dto.WebsiteUrl;
            profile.InstagramUrl = dto.InstagramUrl;
            profile.LinkedInUrl = dto.LinkedInUrl;
            _repo.Update(profile);
        }

        await _repo.SaveChangesAsync();
    }
}