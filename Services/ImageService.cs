using para.Models;

namespace para.Services;

public class ImageService(string webRoot) : IImageService
{
    public async Task<string> SaveAsync(Stream stream, string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        if (extension is not (".png" or ".jpg" or ".jpeg" or ".webp"))
            throw new StoreException(StoreError.Validation, "Выберите изображение PNG, JPG или WebP.");
        var name = $"{Guid.NewGuid():N}{extension}";
        var directory = Path.Combine(webRoot, "images");
        Directory.CreateDirectory(directory);
        await using var file = File.Create(Path.Combine(directory, name));
        await stream.CopyToAsync(file);
        return "/images/" + name;
    }
}
