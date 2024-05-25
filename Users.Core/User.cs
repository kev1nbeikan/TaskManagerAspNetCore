using Users.Core.Abstractions;

namespace Users.Core;

public class User
{
    public static int MAX_USERNA_LENGTH = 255;

    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }


    private User(Guid userId, string userName, string email, string passwordHash)
    {
        UserId = userId;
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
    }


    public static (User? user, string error) Create(Guid userId, string userName, string email, string passwordHash)
    {
        var result = (user: default(User), error: string.Empty);

        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(passwordHash))
        {
            result.error = "Empty fields not allowed";
        }

        result.user = new User(userId, userName, email, passwordHash);

        return result;
    }
}