using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.Shared.Settings;

namespace PetCareMini.Infrastructure.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinarySettings> options)
    {
        var settings = options.Value;
        var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
        _cloudinary = new Cloudinary(account);
        _cloudinary.Api.Secure = true;
    }

    public async Task<(string ImageUrl, string PublicId)> UploadImageAsync(IFormFile file, string folderName)
    {
        if (file == null || file.Length == 0)
            return (null!, null!);

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var extension = Path.GetExtension(file.FileName).ToLower();

        await using var stream = file.OpenReadStream();

        if (allowedExtensions.Contains(extension))
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = folderName,
                Transformation = new Transformation().Width(500).Height(500).Crop("fill")
            };

            var result = await _cloudinary.UploadAsync(uploadParams);
            if (result.StatusCode == HttpStatusCode.OK)
                return (result.SecureUrl.ToString(), result.PublicId);
        }

        return (null!, null!);
    }

    public async Task<bool> DeleteImageAsync(string publicId)
    {
        if (string.IsNullOrWhiteSpace(publicId)) return false;

        var deletionParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deletionParams);
        return result.Result == "ok" || result.Result == "not found";
    }
}