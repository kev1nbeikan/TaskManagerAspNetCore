namespace Users.Core.Abstractions;

public interface IUserRepository
{
    Task Add(User user);
    Task<User?> GetByEmail(string email);
    Task<User?> GetById(Guid id);
}