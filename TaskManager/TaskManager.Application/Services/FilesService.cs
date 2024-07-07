using Microsoft.AspNetCore.Http;
using TaskManager.Core.Abstractions;
using static Microsoft.AspNetCore.Http.IFormFile;

namespace TaskManager.Application.Services;

public class FilesService : IFileService
{
    private readonly IFileSaver _fileSaver;


    private string _uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";


    public FilesService(IFileSaver fileSaver)
    {
        _fileSaver = fileSaver;
    }

    public async Task SaveFile(IFormFile formFile)
    {
        string fullPath = $"{_uploadPath}/{formFile.FileName}";

        await _fileSaver.Save(fullPath, formFile);
    }

    public void SetPathUpload(string uploadPath)
    {
        _uploadPath = uploadPath;
        Directory.CreateDirectory(uploadPath);
    }
}