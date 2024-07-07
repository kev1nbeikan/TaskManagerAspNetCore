using Microsoft.EntityFrameworkCore;
using TaskManager.DataAccess.Enities;
using TaskManager.DataAccess.Extentions;
using Users.Core.Abstractions;

namespace TaskManager.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TaskManagerDbContext _dbContext;

    public UserRepository(TaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Task Add(Users.Core.User user)
    {
        var userEntity = new UserEntity
        {
            Id = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            PasswordHash = user.PasswordHash
        };
        _dbContext.Users.Add(userEntity);
        return _dbContext.SaveChangesAsync();
    }

    public async Task<Users.Core.User?> GetByEmail(string email)
    {
        var userEntity = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);

        return userEntity?.ToCoreUser();
    }

    public async Task<Users.Core.User?> GetById(Guid id)
    {
        var userEntity = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        return userEntity?.ToCoreUser();
    }
}