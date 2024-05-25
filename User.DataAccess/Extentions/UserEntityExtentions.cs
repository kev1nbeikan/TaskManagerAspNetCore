using User.DataAccess.Entities;

namespace User.DataAccess.Extentions;

public static class UserEntityMappingExtensions
{
    public static Users.Core.User ToCoreUser(this UserEntity userEntity)
    {
        if (userEntity == null)
        {
            throw new ArgumentNullException(nameof(userEntity));
        }


        return Users.Core.User.Create(
            userEntity.Id,
            userEntity.UserName,
            userEntity.Email,
            userEntity.PasswordHash
        ).user!;
    }
}