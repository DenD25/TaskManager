using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Contracts.Service
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
