using ApplicationCore.Configurations;
using ApplicationCore.Contracts.Service;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ApplicationCore.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly DataContext _context;
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinaryConfig> config, DataContext context)
        {
            var acc = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
            _context = context;
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                    Folder = "taskManager"
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var photo = await _context.Photos
                .Where(x => x.PublicId == publicId)
                .SingleOrDefaultAsync();

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}
