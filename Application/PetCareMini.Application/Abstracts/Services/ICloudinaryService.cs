using Microsoft.AspNetCore.Http;

namespace PetCareMini.Application.Abstracts.Services;

public interface ICloudinaryService
{
    Task<(string ImageUrl, string PublicId)> UploadImageAsync(IFormFile file, string folderName);
    Task<bool> DeleteImageAsync(string publicId);
}