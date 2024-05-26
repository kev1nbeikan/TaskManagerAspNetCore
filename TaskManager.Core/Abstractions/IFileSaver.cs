using Microsoft.AspNetCore.Http;

namespace TaskManager.Core.Abstractions;

public interface IFileSaver
{
    Task Save(string fullPath, IFormFile formFile);}