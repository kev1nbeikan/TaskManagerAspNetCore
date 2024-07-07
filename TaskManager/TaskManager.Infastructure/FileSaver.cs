using Microsoft.AspNetCore.Http;
using TaskManager.Core.Abstractions;

namespace TaskManager.Infastructure;

public class FileSaver : IFileSaver
{
    public async Task Save(string fullPath, IFormFile formFile)
    {
        await using var fileStream = new FileStream(fullPath, FileMode.Create);
        await formFile.CopyToAsync(fileStream);
    }
}