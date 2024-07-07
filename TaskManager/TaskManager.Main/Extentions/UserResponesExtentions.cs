using WebApplication3.Contracts;

namespace WebApplication3.Extentions;

public static class UserResponesExtentions
{
    public static UserRespone ToUserRespone(this Users.Core.User user)
    {
        return new UserRespone(user.Email, user.UserName);
    }
}