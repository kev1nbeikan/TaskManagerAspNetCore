using UserEntity = TaskManager.DataAccess.Enities.UserEntity;

namespace TaskManager.DataAccess.Extentions;

public static class UserEntityMappingExtensions
{
    public static Users.Core.User ToCoreUser(this UserEntity userEntity)
    {
        ArgumentNullException.ThrowIfNull(userEntity);

        return Users.Core.User.Create(
            userEntity.Id,
            userEntity.UserName,
            userEntity.Email,
            userEntity.PasswordHash
        ).user!;
    }
}