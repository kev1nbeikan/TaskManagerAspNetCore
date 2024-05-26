namespace User.Application.Service;

public interface IUserService
{
    Task<(Users.Core.User? user, string? error)> Register(string userName, string email, string passwordHash);
    Task<(string? token, string? error)> Login(string email, string password);
    Task<Users.Core.User?> getUser(Guid id);
}