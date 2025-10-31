using Personnel.Application.Services.Interfaces;
using Personnel.Infrastructure.Minio;

namespace Personnel.Application.Services.Services;

public class ImageService : IImageService
{
    private readonly MinioStorageService _storage;

    public ImageService(MinioStorageService storage)
    {
        _storage = storage;
    }

    public async Task<string> UploadImageAsync(Stream stream, string fileName, string contentType)
    {
        // Формируем имя объекта в MinIO
        var imageName = $"{Guid.NewGuid()}-{fileName}";
        // Загружаем файл
        var filePath = await _storage.UploadAsync(imageName, stream, stream.Length, contentType);

        return filePath;
    }

    public async Task<(Stream, string)?> DownloadImageAsync(string fileName)
    {
        var stream = await _storage.DownloadAsync(fileName);
        return (stream, fileName);
    }

    public async Task DeleteImageAsync(string imageName)
    {
        await _storage.DeleteAsync(imageName);
    }
}
