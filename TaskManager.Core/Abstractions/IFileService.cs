using Microsoft.AspNetCore.Http;

namespace TaskManager.Core.Abstractions;

public interface IFileService
{
    Task SaveFile(IFormFile formFile);
    void SetPathUpload(string uploadPath);
}