namespace User.Application.Service;

public interface IUserService
{
    Task<Users.Core.User?> Register(string userName, string email, string passwordHash);
    Task<string> Login(string email, string password);
}