namespace para.Services;

public interface IImageService
{
    Task<string> SaveAsync(Stream stream, string fileName);
}
