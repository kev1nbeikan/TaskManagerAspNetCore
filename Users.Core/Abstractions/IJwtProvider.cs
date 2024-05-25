namespace User.Infastructure;

public interface IJwtProvider
{
    string GenerateToken(Users.Core.User user);
}