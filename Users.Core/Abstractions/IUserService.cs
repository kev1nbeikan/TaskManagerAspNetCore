namespace User.Application.Service;

public interface IUserService
{
    Task Register(string userName, string email, string passwordHash);
    Task<string> Login(string email, string password);
}